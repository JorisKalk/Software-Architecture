using UnityEngine;
using System.Collections.Generic;

public class EnemySpawnHandling : MonoBehaviour
{
    [Header("Spawn Amounts")]
    [SerializeField]
    private int minimumSpawns;
    [SerializeField]
    private int maximumSpawns;

    [Header("Lists")]
    [SerializeField]
    private GameObject[] spawnableEnemies;
    [SerializeField]
    private List<GameObject> enemySpawners = new List<GameObject>();

    void Start()
    {
        for (int i = enemySpawners.Count - 1; i >= 0; --i)
        {
            if (enemySpawners[i] == null)
            {
                enemySpawners.RemoveAt(i);
                Debug.Log("removed item");
            }
        }

        if (maximumSpawns > enemySpawners.Count) maximumSpawns = enemySpawners.Count;
        int spawnAmount = Random.Range(minimumSpawns, maximumSpawns + 1);
        Debug.Log("Spawning " + spawnAmount + " Enemies");

        for (int i = 0; i < spawnAmount; i++)
        {
            int spawnIndex = Random.Range(0, spawnableEnemies.Length);
            int pickedSpawner = Random.Range(0, enemySpawners.Count);
            enemySpawners[pickedSpawner].GetComponent<EnemySpawner>().SpawnEnemy(spawnableEnemies[spawnIndex]);
            Destroy(enemySpawners[pickedSpawner].gameObject);
            enemySpawners.RemoveAt(pickedSpawner);
        }
    }
}
