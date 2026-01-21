using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    private SpriteRenderer sprite;

    private RangedWeaponData weaponData;
    private RangedProjectileData projectileData;

    private int damage;
    private GameObject attackProjectile;

    [SerializeField]
    private Vector3 projectileOriginOffset = new Vector3(0f, 0f, 0f);

    private bool canShoot = true;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    public void SetAttackData(RangedWeaponData pWeaponData, RangedProjectileData pProjectileData)
    {
        weaponData = pWeaponData;
        projectileData = pProjectileData;
        damage = weaponData.damageBonus + projectileData.damageData.damage;
        attackProjectile = projectileData.projectile;
    }

    void Update()
    {
        
    }
}
