using UnityEngine;
using System;

[CreateAssetMenu(fileName = "DamageData", menuName = "Scriptable Objects/DamageData")]
public class DamageData : ScriptableObject
{
    public int damage;
    public DamageType damageType;
}
