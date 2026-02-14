using UnityEngine;
using System;
using System.ComponentModel;
using UnityEngine.UI;
using Unity.VisualScripting;

public class PlayerModel : MonoBehaviour
{
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

    public event Action<Player> PlayerCreated;
    public event Action<Player, DamageData> OnHit;
    //TODO: make sure that you change int to healingdata everywhere if you ever implement healingdata
    public event Action<Player, int> OnHeal;
    public event Action<Player> OnMaxHpChanged;

    private void Start()
    {
        player = baseStats.CreatePlayer();
        PlayerCreated?.Invoke(player);
        SetGuiValues();
    }

    private void OnEnable()
    {
        movementController.SetMovementValues(baseStats);
        attackController.SetAttackData(rangedWeapon, rangedWeapon.projectileData);
    }

    public void GetHit(DamageData damageData)
    {
        player.currentHP -= damageData.damage;
        if (player.currentHP < 0)
        {
            player.currentHP = 0;
        }

        currentHP.value = player.currentHP;

        OnHit?.Invoke(player, damageData);
    }

    public void OnEnemyDied(EventData eventData)
    {
        EnemyDieEventData enemyDieEvent = (EnemyDieEventData)eventData;

        //LevelUp(player.UpdateLevel(enemyDieEvent.enemy.XP));
        int newLevels = player.UpdateLevel(enemyDieEvent.enemy.XP);
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
        level.value += amountOfLevels;
    }

    private void SetGuiValues()
    {
        currentHP.value = player.currentHP;
        maxHP.value = player.MaxHP;
        maxXP.value = (int)player.XpToNextLevel;
    }
}
