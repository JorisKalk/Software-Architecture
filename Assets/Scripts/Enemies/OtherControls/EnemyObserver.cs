using UnityEngine;

public abstract class EnemyObserver : MonoBehaviour
{
    [SerializeField]
    protected EnemyController enemyController;

    protected void OnEnable()
    {
        enemyController.onEnemyCreated += OnEnemyCreated;
        enemyController.OnHit += OnEnemyHit;
    }

    protected void OnDisable()
    {
        enemyController.onEnemyCreated -= OnEnemyCreated;
        enemyController.OnHit -= OnEnemyHit;
    }

    protected abstract void OnEnemyCreated(Enemy enemy);

    protected abstract void OnEnemyHit(Enemy enemy, DamageData damageData);
}
