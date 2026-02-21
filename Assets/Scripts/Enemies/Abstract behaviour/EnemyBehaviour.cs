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

    protected void OnEnable()
    {
        tf = transform;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();

        enemyController.onEnemyCreated += OnEnemyCreated;
    }

    protected void OnDisable()
    {
        enemyController.onEnemyCreated -= OnEnemyCreated;
    }

    protected void Update()
    {
        MoveBehaviour();
        AttackBehaviour();
    }

    protected void OnEnemyCreated(Enemy enemy)
    {
        this.enemy = enemy;
        damageData = enemyController.dealtDamageData;
        damageData.damage += enemy.DamageDealt;
    }

    protected abstract void MoveBehaviour();

    protected abstract void AttackBehaviour();
}
