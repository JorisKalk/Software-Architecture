using UnityEngine;

public class EnemyDieController : EnemyObserver
{
    private bool died = false;

    [SerializeField]
    private GameEvent enemyDieEvent;

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
                Destroy(enemyController.gameObject);
            }
        }
    }
}
