using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //[SerializeField]
    //private GameObject[] spawnableEnemies;

    //void Start()
    //{
    //    int spawnChance = Random.Range(1, 101);

    //    if (spawnChance > 50)
    //    {
    //        spawnableEnemies[0].transform.position = transform.position;
    //        Instantiate(spawnableEnemies[0]);
    //    }
    //    Destroy(this);
    //}

    public void SpawnEnemy(GameObject enemy)
    {
        enemy.transform.position = transform.position;
        Instantiate(enemy);
        //Destroy(gameObject);
    }
}
