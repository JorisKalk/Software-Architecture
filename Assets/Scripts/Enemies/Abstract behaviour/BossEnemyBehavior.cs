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

    protected override void MoveBehaviour()
    {
        
    }

    protected override void AttackBehaviour()
    {
        
    }

    private void Idle()
    {
        //needs to make sure the timer properly starts
        //needs to make the animation go back to the default gun aiming sprite.
        //make sure that the state machine and its steps properly run the step frame every frame!!!!!!!
    }

    private void BounceAttack()
    {
        //dont forget to add bounces left to the projectile after instantiating.
        //needs to trigger the attacking animation
        //need a method that the attacking animation can trigger that makes the state machine move on from the pierce attack state.
        //(maybe same method as pierce attack?)
        //do this by changing the bool in the state.
    }

    private void PierceAttack()
    {
        //dont forget to add lifetime to the projectile after instantiating.
        //needs to trigger the attacking animation
        //need a method that the attacking animation can trigger that makes the state machine move on from the pierce attack state.
        //(maybe same method as bounce attack?)
        //do this by changing the bool in the state.
    }

    private void Reload()
    {
        //reload animation has to be triggered
        //need a method that the reload animation can trigger that makes the state machine move on from the reload state.
        //do this by changing the bool in the state.
    }
}
