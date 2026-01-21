using UnityEngine;
using System;

[CreateAssetMenu(fileName = "RangedWeaponData", menuName = "Scriptable Objects/Equipment/RangedWeaponData")]
public class RangedWeaponData : ScriptableObject
{
    public float shotCooldown;
    public int damageBonus;
    public RangedProjectileData projectileData;
}
