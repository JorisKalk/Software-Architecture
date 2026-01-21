using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Transform tf;

    private RangedWeaponData weaponData;
    private RangedProjectileData projectileData;

    private float shotCooldown;
    private int damage;
    private float projectileSpeed;
    private GameObject attackProjectile;

    [SerializeField]
    private Vector3 projectileOriginOffset = new Vector3(0f, 0f, 0f);

    private float currentShotCooldown = 0f;
    private bool canShoot = true;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        tf = GetComponent<Transform>();
    }

    public void SetAttackData(RangedWeaponData pWeaponData, RangedProjectileData pProjectileData)
    {
        weaponData = pWeaponData;
        projectileData = pProjectileData;
        shotCooldown = weaponData.shotCooldown;
        damage = weaponData.damageBonus + projectileData.damageData.damage;
        projectileSpeed = projectileData.projectileSpeed;
        attackProjectile = projectileData.projectile;
    }

    void Update()
    {
        if (canShoot)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                FireProjectile();

                canShoot = false;
                currentShotCooldown = shotCooldown;
            }
        }
        else if (currentShotCooldown  > 0f)
        {
            currentShotCooldown -= Time.deltaTime;
            if (currentShotCooldown < 0f) canShoot = true;
        }
        
    }

    private void FireProjectile()
    {
        GameObject firedProjectile = Instantiate(attackProjectile);

        int lookDir = 0;
        if (sprite.flipX) 
        {
            lookDir = -1;
            firedProjectile.GetComponent<SpriteRenderer>().flipX = true;
        }
        else lookDir = 1;

        projectileOriginOffset = new Vector3(projectileOriginOffset.x * lookDir, projectileOriginOffset.y, projectileOriginOffset.z);
        firedProjectile.GetComponent<Transform>().position = tf.position + projectileOriginOffset;
        firedProjectile.GetComponent<Rigidbody2D>().linearVelocityX = projectileSpeed * lookDir;
        firedProjectile.GetComponent<PlayerProjectile>().damageData = new DamageData(damage, projectileData.damageData.damageType);
    }
}
