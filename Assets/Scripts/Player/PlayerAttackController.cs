using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Transform tf;

    private RangedWeaponData weaponData;
    private RangedProjectileData projectileData;

    private float shotCooldown;
    private int damage;
    private int playerDamageBonus;
    private float projectileSpeed;
    private GameObject attackProjectile;

    [SerializeField]
    private GameObject muzzleFlashAnimation;
    [SerializeField]
    private GameObject projectileOriginOffsetReference;
    [SerializeField]
    private GameObject flashOffsetReference;
    private Vector3 projectileOriginOffset;
    private Vector3 muzzleFlashOffset;

    private float currentShotCooldown = 0f;
    private bool canShoot = true;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        tf = GetComponent<Transform>();
        projectileOriginOffset = projectileOriginOffsetReference.transform.localPosition;
        Destroy(projectileOriginOffsetReference.gameObject);
        muzzleFlashOffset = flashOffsetReference.transform.localPosition;
        Destroy(flashOffsetReference.gameObject);
    }

    public void SetAttackData(RangedWeaponData pWeaponData, RangedProjectileData pProjectileData)
    {
        weaponData = pWeaponData;
        projectileData = pProjectileData;
        shotCooldown = weaponData.shotCooldown;
        damage = weaponData.damageBonus + projectileData.damageData.damage + playerDamageBonus;
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
        GameObject muzzleFlash = Instantiate(muzzleFlashAnimation);

        int lookDir = 0;
        if (sprite.flipX) 
        {
            lookDir = -1;
            firedProjectile.GetComponent<SpriteRenderer>().flipX = true;
            muzzleFlash.GetComponent<SpriteRenderer>().flipX = true;
        }
        else lookDir = 1;

        Vector3 offset = projectileOriginOffset;
        offset.x *= lookDir;
        firedProjectile.GetComponent<Transform>().position = tf.position + offset;
        firedProjectile.GetComponent<Rigidbody2D>().linearVelocityX = projectileSpeed * lookDir;
        firedProjectile.GetComponent<PlayerProjectile>().damageData = new DamageData(damage, projectileData.damageData.damageType);

        Vector3 flashOffset = muzzleFlashOffset;
        flashOffset.x *= lookDir;
        muzzleFlash.GetComponent<Transform>().position = tf.position + flashOffset;
    }

    public void DamageUp(int extraDamage)
    {
        playerDamageBonus += extraDamage;
        damage = weaponData.damageBonus + projectileData.damageData.damage + playerDamageBonus;
    }
}
