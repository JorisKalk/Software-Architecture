using Unity.VisualScripting;
using UnityEngine;

public class BossEnemyBehavior : EnemyBehaviour
{
    private LineRenderer laser;
    private BoxCollider2D playerCol;

    private BossStateMachine bossStateMachine;

    [Header("Bounce attack variables")]
    [SerializeField]
    private int initialBounceAttackChance;
    [SerializeField]
    private int maxBounces;
    [SerializeField]
    private LayerMask terrain;

    [Header("Pierce attack variables")]
    [SerializeField]
    private int initialPierceAttackChance;
    [SerializeField]
    private float maxPierceProjectileLifeTime;

    [Header("Attack projectiles")]
    [SerializeField]
    private RangedProjectileData bounceProjectileData;
    private int bounceDamage;
    private float bounceProjectileSpeed;
    private GameObject bounceAttackProjectile;

    [SerializeField]
    private RangedProjectileData pierceProjectileData;
    private int pierceDamage;
    private float pierceProjectileSpeed;
    private GameObject pierceAttackProjectile;

    [Header("Firing animation references")]
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

    private float timeToAttack;
    private Vector3 aimingDir;

    //state booleans
    private bool isIdle = false;
    private bool preparingBounceAttack = false;
    private bool preparingPierceAttack = false;

    protected override void OnEnemyCreated(Enemy enemy)
    {
        base.OnEnemyCreated(enemy);

        laser = GetComponent<LineRenderer>();
        playerCol = GameObject.Find("Player").GetComponent<BoxCollider2D>();

        if (bossStateMachine == null)
        {
            bossStateMachine = new BossStateMachine(this, attackCooldown, initialBounceAttackChance, initialPierceAttackChance);
        }

        SubscribeStateMachineEvents();
        InitializeProjectileStatistics();
        SetupLaser();
        SetOffsets();

        bossStateMachine.Enter();
    }

    private void SubscribeStateMachineEvents()
    {
        bossStateMachine.onStartIdling += Idle;
        bossStateMachine.onStartBounceAttack += BounceAttack;
        bossStateMachine.onStartPierceAttack += PierceAttack;
        bossStateMachine.onStartReloading += Reload;
    }

    private void InitializeProjectileStatistics()
    {
        bounceDamage = damageData.damage + bounceProjectileData.damageData.damage;
        bounceProjectileSpeed = bounceProjectileData.projectileSpeed;
        bounceAttackProjectile = bounceProjectileData.projectile;
        pierceDamage = damageData.damage + pierceProjectileData.damageData.damage;
        pierceProjectileSpeed = pierceProjectileData.projectileSpeed;
        pierceAttackProjectile = pierceProjectileData.projectile;
    }

    private void SetupLaser()
    {
        laser.positionCount = 2;
        laser.startColor = Color.red;
        laser.endColor = Color.red;
        laser.startWidth = 0.5f;
        laser.endWidth = 0.5f;
        laser.enabled = false;
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

    protected override void MoveBehaviour()
    {
        
    }

    protected override void AttackBehaviour()
    {
        bossStateMachine.Step();
        
        if (isIdle)
        {

        }
        else if (preparingBounceAttack && AttackTimer())
        {
            preparingBounceAttack = false;
            anim.SetTrigger("Fire");
            FireBounceBullet();
            laser.enabled = false;
        }
        else if (preparingPierceAttack && AttackTimer())
        {
            preparingPierceAttack = false;
            anim.SetTrigger("Fire");
            FirePierceBullet();
            laser.enabled = false;
        }
    }

    private void Idle()
    {
        //isIdle = true;
        //needs to make sure the timer properly starts
        //needs to make the animation go back to the default gun aiming sprite.
        //make sure that the state machine and its steps properly run the step frame every frame!!!!!!!
    }

    private void BounceAttack()
    {
        isIdle = false;
        preparingBounceAttack = true;
        timeToAttack = enemy.AttackDelay;
        laser.enabled = true;
        laser.positionCount = maxBounces + 1;
        aimingDir = PlayerDir();
        Vector3 currentPos = col.bounds.center + laserOriginOffset;
        Vector3 currentDir = aimingDir;
        laser.SetPosition(0, currentPos);
        for (int i = 1; i <= maxBounces; i++)
        {
            RaycastHit2D hit = LaserTargetPosition(currentPos, currentDir);
            currentPos = hit.point;
            currentDir = Vector3.Reflect(currentDir, hit.normal);
            currentDir = currentDir.normalized;
            laser.SetPosition(i, currentPos);
            currentPos += currentDir * 0.1f;
        }

        //dont forget to add bounces left to the projectile after instantiating.
        //dont forget to add projectilespeed variable to this projectile, not only set velocity because it needs to repeat it on bounces.
        //needs to trigger the attacking animation
        //need a method that the attacking animation can trigger that makes the state machine move on from the pierce attack state.
        //(maybe same method as pierce attack?)
        //do this by changing the bool in the state.
    }

    private void PierceAttack()
    {
        isIdle = false;
        preparingPierceAttack = true;
        timeToAttack = enemy.AttackDelay;
        laser.enabled = true;
        laser.positionCount = 2;
        aimingDir = PlayerDir();
        Vector3 startPos = col.bounds.center + laserOriginOffset;
        laser.SetPosition(0, startPos);
        laser.SetPosition(1, startPos + (aimingDir * 400));
        //dont forget to add lifetime to the projectile after instantiating.
        //needs to trigger the attacking animation
        //need a method that the attacking animation can trigger that makes the state machine move on from the pierce attack state.
        //(maybe same method as bounce attack?)
        //do this by changing the bool in the state.
    }

    private void Reload()
    {
        anim.SetTrigger("StartReload");
        //reload animation has to be triggered
        //need a method that the reload animation can trigger that makes the state machine move on from the reload state.
        //do this by changing the bool in the state.
    }

    private void FireBounceBullet()
    {
        GameObject firedProjectile = Instantiate(bounceAttackProjectile);
        GameObject muzzleFlash = Instantiate(muzzleFlashAnimation);
        firedProjectile.gameObject.GetComponent<SpriteRenderer>().flipX = true;
        muzzleFlash.gameObject.GetComponent<SpriteRenderer>().flipX = true;

        Vector3 offset = projectileOriginOffset;
        firedProjectile.GetComponent<Transform>().position = tf.position + offset;
        firedProjectile.GetComponent<Rigidbody2D>().linearVelocity = bounceProjectileSpeed * aimingDir;
        firedProjectile.GetComponent<BossBounceProjectile>().damageData = new DamageData(bounceDamage, bounceProjectileData.damageData.damageType);
        firedProjectile.GetComponent<BossBounceProjectile>().bouncesLeft = maxBounces;
        firedProjectile.GetComponent<BossBounceProjectile>().projectileSpeed = bounceProjectileSpeed;

        Vector3 flashOffset = muzzleFlashOffset;
        muzzleFlash.GetComponent<Transform>().position = tf.position + flashOffset;

        anim.SetTrigger("Fire");
    }

    private void FirePierceBullet()
    {
        GameObject firedProjectile = Instantiate(pierceAttackProjectile);
        GameObject muzzleFlash = Instantiate(muzzleFlashAnimation);
        firedProjectile.gameObject.GetComponent<SpriteRenderer>().flipX = true;
        muzzleFlash.gameObject.GetComponent<SpriteRenderer>().flipX = true;

        Vector3 offset = projectileOriginOffset;
        firedProjectile.GetComponent<Transform>().position = tf.position + offset;
        firedProjectile.GetComponent<Rigidbody2D>().linearVelocity = pierceProjectileSpeed * aimingDir;
        firedProjectile.GetComponent<BossPierceProjectile>().damageData = new DamageData(pierceDamage, pierceProjectileData.damageData.damageType);
        firedProjectile.GetComponent<BossPierceProjectile>().lifeTime = maxPierceProjectileLifeTime;

        Vector3 flashOffset = muzzleFlashOffset;
        muzzleFlash.GetComponent<Transform>().position = tf.position + flashOffset;

        anim.SetTrigger("Fire");
    }

    private RaycastHit2D LaserTargetPosition(Vector3 startPos, Vector3 laserDir)
    {
        return Physics2D.Raycast(startPos, laserDir, Mathf.Infinity, terrain);
    }

    private Vector3 PlayerDir()
    {
        Vector3 playerDir = playerCol.bounds.center - (col.bounds.center + laserOriginOffset);
        playerDir.Normalize();
        return playerDir;
    }

    private bool AttackTimer()
    {
        if (timeToAttack <= 0)
        {
            return true;
        }
        else
        {
            timeToAttack -= Time.deltaTime;
            return false;
        }
    }

    public void HasAttacked()
    {
        bossStateMachine.HasAttacked();
    }

    public void Reloaded()
    {
        bossStateMachine.Reloaded();
        anim.SetTrigger("Reloaded");
    }
}
