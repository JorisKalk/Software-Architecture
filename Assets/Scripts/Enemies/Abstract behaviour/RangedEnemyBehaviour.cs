using UnityEngine;

/// <summary>
/// Class that extends EnemyBehaviour to handle the behaviour of ranged enemies. It also handles animations.
/// </summary>
public class RangedEnemyBehaviour : EnemyBehaviour
{
    private LineRenderer laser;
    private BoxCollider2D playerCol;

    [SerializeField]
    private LayerMask terrain;

    [SerializeField]
    private RangedProjectileData projectileData;
    private int damage;
    private float projectileSpeed;
    private GameObject attackProjectile;

    [SerializeField]
    private GameObject muzzleFlashAnimation;
    [SerializeField]
    private GameObject projectileOriginOffsetReference;
    [SerializeField]
    private GameObject flashOffsetReference;
    [SerializeField]
    private GameObject laserOriginOffsetReference;
    private Vector3 projectileOriginOffset;
    private Vector3 muzzleFlashOffset;
    private Vector3 laserOriginOffset;

    private Vector3 directionalLaserOffset;

    private Vector3 aimTarget;

    private float attackTimer;

    private void Start()
    {
        laser = GetComponent<LineRenderer>();
        playerCol = GameObject.Find("Player").GetComponent<BoxCollider2D>();
        
        SetOffsets();
        SetupLaser();
    }

    private void SetOffsets()
    {
        projectileOriginOffset = projectileOriginOffsetReference.transform.localPosition;
        Destroy(projectileOriginOffsetReference.gameObject);
        muzzleFlashOffset = flashOffsetReference.transform.localPosition;
        Destroy(flashOffsetReference.gameObject);
        laserOriginOffset = laserOriginOffsetReference.transform.localPosition;
        Destroy(laserOriginOffsetReference.gameObject);
    }

    private void SetupLaser()
    {
        laser.positionCount = 2;
        laser.startColor = Color.red;
        laser.endColor = Color.red;
        laser.startWidth = 0.1f;
        laser.endWidth = 0.1f;
        laser.enabled = false;
    }

    protected override void OnEnemyCreated(Enemy enemy)
    {
        base.OnEnemyCreated(enemy);
        damage = damageData.damage + projectileData.damageData.damage;
        projectileSpeed = projectileData.projectileSpeed;
        attackProjectile = projectileData.projectile;
    }

    protected override void MoveBehaviour() { }

    protected override void AttackBehaviour()
    {
        if (!isAttacking)
        {
            LookAtPlayer();
            if (cooldownTimer > 0)
            {
                cooldownTimer -= Time.deltaTime;
            }
            else if (PlayerTargetFound())
            {
                attackOnCooldown = false;
                cooldownTimer = attackCooldown;
                PrepareAttack();
            }
        }
        else
        {
            if (attackTimer > 0)
            {
                attackTimer -= Time.deltaTime;
            }
            else if (!attackOnCooldown)
            {
                FireProjectile(aimTarget);
                attackOnCooldown = true;
                laser.enabled = false;
            }
        }
    }

    private void PrepareAttack()
    {
        isAttacking = true;
        aimTarget = PlayerDir();
        laser.enabled = true;
        laser.SetPosition(0, col.bounds.center + directionalLaserOffset);
        laser.SetPosition(1, LaserTargetPosition());
        attackTimer = enemy.AttackDelay;
    }

    private void LookAtPlayer()
    {
        if (playerCol.bounds.center.x < tf.position.x)
        {
            sprite.flipX = true;
        }
        else sprite.flipX = false;
    }

    private Vector3 LaserTargetPosition()
    {
        RaycastHit2D hit = Physics2D.Raycast(col.bounds.center + directionalLaserOffset, PlayerDir(), Mathf.Infinity, terrain);
        return hit.point;
    }

    private void FireProjectile(Vector3 aimDir)
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
        aimDir *= lookDir;

        Vector3 offset = projectileOriginOffset;
        offset.x *= lookDir;
        firedProjectile.GetComponent<Transform>().position = tf.position + offset;
        firedProjectile.GetComponent<Rigidbody2D>().linearVelocity = projectileSpeed * lookDir * aimDir;
        firedProjectile.GetComponent<EnemyProjectile>().damageData = new DamageData(damage, projectileData.damageData.damageType);

        Vector3 flashOffset = muzzleFlashOffset;
        flashOffset.x *= lookDir;
        muzzleFlash.GetComponent<Transform>().position = tf.position + flashOffset;

        anim.SetTrigger("Fire");
    }

    private Vector3 PlayerDir()
    {
        Vector3 playerDir = playerCol.bounds.center - (col.bounds.center + directionalLaserOffset);
        playerDir.Normalize();
        return playerDir;
    }

    private bool PlayerTargetFound()
    {
        if (sprite.flipX)
        {
            directionalLaserOffset = laserOriginOffset;
            directionalLaserOffset.x *= -1;
        }
        else
        {
            directionalLaserOffset = laserOriginOffset;
        }

        return Physics2D.Raycast(col.bounds.center + directionalLaserOffset, PlayerDir(), enemy.AttackRange, playerLayer);
    }

    public void StartReload()
    {
        anim.SetTrigger("Reload");
        isAttacking = false;
    }

    public void ReloadDone()
    {
        anim.SetTrigger("Reloaded");
    }
}
