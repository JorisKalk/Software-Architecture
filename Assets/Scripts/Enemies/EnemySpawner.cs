using UnityEngine;

/// <summary>
/// Can be used by EnemySpawnHandling to spawn an enemy on its position.
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    public GameObject SpawnEnemy(GameObject enemy)
    {
        enemy.transform.position = transform.position;
        return Instantiate(enemy);
    }
}
