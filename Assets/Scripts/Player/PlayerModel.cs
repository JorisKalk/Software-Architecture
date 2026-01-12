using UnityEngine;
using System;
using System.ComponentModel;
using UnityEngine.UI;

public class PlayerModel : MonoBehaviour
{
    [Header("Player Attributes")]
    [SerializeField]
    private PlayerData baseStats;
    [SerializeField]
    private DamageData baseDamage;
    [SerializeField]
    private int xpNeededForLevelUp;

    [Header("Player Equipment")]
    [SerializeField]
    private ArmorData armor;
    [SerializeField]
    private RangedWeaponData rangedWeapon;
    [SerializeField]
    private MeleeWeaponData meleeWeapon;


}
