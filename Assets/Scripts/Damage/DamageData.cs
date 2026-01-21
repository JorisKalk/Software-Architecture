using UnityEngine;
using System;

[Serializable]
public class DamageData
{
    public int damage;
    public DamageType damageType;

    public DamageData(int pDamage, DamageType pDamageType)
    {
        damage = pDamage;
        damageType = pDamageType;
    }
}
