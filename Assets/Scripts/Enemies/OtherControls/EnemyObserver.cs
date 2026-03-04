using UnityEngine;

/// <summary>
/// Base class for observers that subscribe to the events that get called by the EnemyController.
/// </summary>
public abstract class EnemyObserver : MonoBehaviour
{
    [SerializeField]
    protected EnemyController enemyController;

    protected void OnEnable()
    {
        enemyController.OnEnemyCreated += OnEnemyCreated;
        enemyController.OnHit += OnEnemyHit;
    }

    protected void OnDisable()
    {
        enemyController.OnEnemyCreated -= OnEnemyCreated;
        enemyController.OnHit -= OnEnemyHit;
    }

    protected abstract void OnEnemyCreated(Enemy enemy);

    protected abstract void OnEnemyHit(Enemy enemy, DamageData damageData);
}
