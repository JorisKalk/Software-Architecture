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
    [SerializeField]
    private int xpNeededForLevelUp;

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

    public event Action<Player> PlayerCreated;
    public event Action<Player, DamageData> OnHit;
    //maybe use this one, but implement healing first.
    //public event Action<HealingData> OnHeal;

    private float currentXP = 0;

    private void Start()
    {
        player = baseStats.CreatePlayer();
        PlayerCreated?.Invoke(player);
    }

    private void OnEnable()
    {
        movementController.SetMovementValues(baseStats);
        attackController.SetAttackData(rangedWeapon, rangedWeapon.projectileData);
    }



    public void OnEnemyDied(EventData eventData)
    {
        EnemyDieEventData enemyDieEvent = (EnemyDieEventData)eventData;
        UpdateXP(enemyDieEvent.enemy.XP);
    }

    private void UpdateXP(int pValue)
    {
        currentXP += pValue;
    }
}
