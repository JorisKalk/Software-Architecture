using UnityEngine;
using System;
using UnityEditor.SceneManagement;
using System.ComponentModel;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class PlayerModel : MonoBehaviour
{
    private Animator anim;

    [Header("Player Attributes")]
    [SerializeField]
    private PlayerData baseStats;
    private Player player;
    [SerializeField]
    private DamageData baseDamage;

    [Header("Level Up")]
    [SerializeField]
    private int bonusMaxHP;
    [SerializeField]
    private int bonusDamage;

    [Header("Player Equipment")]
    [SerializeField]
    private ArmorData armor;
    [SerializeField]
    private RangedWeaponData rangedWeapon;
    //[SerializeField]
    //private MeleeWeaponData meleeWeapon;

    [Header("Controllers")]
    [SerializeField]
    private PlayerMovement movementController;
    [SerializeField]
    private PlayerAttackController attackController;

    [Header("GUI Int Values")]
    [SerializeField]
    private IntValue level;
    [SerializeField]
    private IntValue currentHP;
    [SerializeField]
    private IntValue maxHP;
    [SerializeField]
    private IntValue currentXP;
    [SerializeField]
    private IntValue maxXP;

    [Header("Temp Values")]
    [SerializeField]
    private int tempPotionHealing = 25;
    private int potions = 0;

    public event Action<Player> PlayerCreated;
    public event Action<Player, DamageData> OnHit;
    //TODO: make sure that you change int to healingdata everywhere if you ever implement healingdata
    public event Action<Player, int> OnHeal;
    public event Action<Player> OnMaxHpChanged;

    private void Start()
    {
        anim = GetComponent<Animator>();

        player = baseStats.CreatePlayer();
        PlayerCreated?.Invoke(player);
        SetGuiValues();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TryHeal();
        }
    }

    private void OnEnable()
    {
        movementController.SetMovementValues(baseStats);
        attackController.SetAttackData(rangedWeapon, rangedWeapon.projectileData);
    }

    private void TryHeal()
    {
        if (potions > 0 && player.currentHP < player.MaxHP)
        {
            potions--;
            player.currentHP += tempPotionHealing;
            if (player.currentHP > player.MaxHP)
            {
                player.currentHP = player.MaxHP;
            }

            currentHP.value = player.currentHP;

            OnHeal?.Invoke(player, tempPotionHealing);
        }
        else
        {
            Debug.Log("couldn't heal");
        }
    }

    public void GetHit(DamageData damageData)
    {
        player.currentHP -= damageData.damage;
        if (player.currentHP < 0)
        {
            player.currentHP = 0;
            movementController.enabled = false;
        }

        currentHP.value = player.currentHP;

        OnHit?.Invoke(player, damageData);
    }

    public void OnEnemyDied(EventData eventData)
    {
        EnemyDieEventData enemyDieEvent = (EnemyDieEventData)eventData;
        GainXP(enemyDieEvent.enemy.XP);

        potions += enemyDieEvent.enemy.PotionsDropped;
    }

    public void OnQuestCompleted(EventData eventData)
    {
        QuestCompleteEventData questCompleteEvent = (QuestCompleteEventData)eventData;

        GainXP(questCompleteEvent.quest.xp);
    }

    private void GainXP(int xpGained)
    {
        int newLevels = player.UpdateLevel(xpGained);
        if (newLevels > 0)
        {
            LevelUp(newLevels);
        }
        currentXP.value = (int)player.currentXP;
    }

    private void LevelUp(int amountOfLevels)
    {
        attackController.DamageUp(bonusDamage * amountOfLevels);
        player.MaxHpUp(bonusMaxHP * amountOfLevels);

        OnMaxHpChanged?.Invoke(player);

        SetGuiValues();
    }

    private void SetGuiValues()
    {
        currentHP.value = player.currentHP;
        maxHP.value = player.MaxHP;
        currentXP.value = (int)player.currentXP;
        maxXP.value = (int)player.XpToNextLevel;
        level.value = player.Level;
    }

    public void EndAnimation()
    {
        anim.SetTrigger("End");
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
