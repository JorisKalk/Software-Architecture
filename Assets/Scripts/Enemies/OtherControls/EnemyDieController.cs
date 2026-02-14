using UnityEngine;

public class EnemyDieController : EnemyObserver
{
    private Animator anim;

    private bool died = false;

    [SerializeField]
    private GameEvent enemyDieEvent;

    protected void Start()
    {
        anim = GetComponent<Animator>();
    }

    protected override void OnEnemyCreated(Enemy enemy)
    {
        
    }

    protected override void OnEnemyHit(Enemy enemy, DamageData damageData)
    {
        if (!died)
        {
            if (enemy.currentHP == 0)
            {
                died = true;
                enemyDieEvent.Publish(new EnemyDieEventData(enemy, enemyController.gameObject), enemyController.gameObject);
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                GetComponent<Collider2D>().enabled = false;
                anim.SetTrigger("Die");
            }
        }
    }
}
