using UnityEngine;

public abstract class EnemyBehaviour : MonoBehaviour
{
    protected Transform tf;
    protected Animator anim;
    protected Rigidbody2D rb;
    protected BoxCollider2D col;
    protected SpriteRenderer sprite;

    protected Transform playerTransform;

    [SerializeField]
    protected EnemyController enemyController;
    protected Enemy enemy;
    protected DamageData damageData;

    [SerializeField]
    protected float attackCooldown;
    protected float cooldownTimer;
    protected bool attackOnCooldown = false;

    protected bool isAttacking = false;

    protected void OnEnable()
    {
        tf = transform;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();

        enemyController.onEnemyCreated += OnEnemyCreated;
        enemyController.onEnemyCreated += ExtraOnEnemyCreated;
    }

    protected void OnDisable()
    {
        enemyController.onEnemyCreated -= OnEnemyCreated;
        enemyController.onEnemyCreated -= ExtraOnEnemyCreated;
    }

    protected void Update()
    {
        if (!enemyController.died)
        {
            MoveBehaviour();
            AttackBehaviour();
        }
    }

    protected void OnEnemyCreated(Enemy enemy)
    {
        this.enemy = enemy;
        damageData = enemyController.dealtDamageData;
        damageData.damage += enemy.DamageDealt;
    }

    protected abstract void ExtraOnEnemyCreated(Enemy enemy);

    protected abstract void MoveBehaviour();

    protected abstract void AttackBehaviour();
}
