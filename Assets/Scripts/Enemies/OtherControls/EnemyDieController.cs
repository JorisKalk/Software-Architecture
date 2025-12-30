using UnityEngine;

public class EnemyDieController : EnemyObserver
{
    private bool died = false;

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
                //implement DieEvent scriptable object here
                Destroy(enemyController.gameObject);
            }
        }
    }
}
