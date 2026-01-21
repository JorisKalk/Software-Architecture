using UnityEngine;

[CreateAssetMenu(fileName = "RangedProjectileData", menuName = "Scriptable Objects/RangedProjectileData")]
public class RangedProjectileData : ScriptableObject
{
    public float projectileSpeed;
    public GameObject projectile;
    public DamageData damageData;
}
