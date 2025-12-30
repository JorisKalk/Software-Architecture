using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public void SpawnEnemy(GameObject enemy)
    {
        enemy.transform.position = transform.position;
        Instantiate(enemy);
        //Destroy(gameObject);
    }
}
