using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject SpawnEnemy(GameObject enemy)
    {
        enemy.transform.position = transform.position;
        return Instantiate(enemy);
    }
}
