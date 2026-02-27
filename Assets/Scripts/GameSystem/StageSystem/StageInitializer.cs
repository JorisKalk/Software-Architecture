using UnityEngine;
using System;
using System.Collections.Generic;

public class StageInitializer : MonoBehaviour
{
    [SerializeField]
    private EnemySpawnHandling spawnHandler;
    [SerializeField]
    private GameObject playerStartPosition;

    private List<GameObject> enemyList = new List<GameObject>();

    public void StartStage(GameObject player)
    {
        if (spawnHandler == null)
        {
            throw new Exception(name + " does not have a SpawnHandler attached!");
        }
        player.transform.position = playerStartPosition.transform.position;
        enemyList = spawnHandler.StartStage();
    }

    public void EndStage()
    {
        for (int i = enemyList.Count - 1; i >= 0; i--)
        {
            if (enemyList[i] != null)
            {
                Destroy(enemyList[i].gameObject);
                enemyList.RemoveAt(i);
            }
            else
            {
                enemyList.RemoveAt(i);
            }
        }
    }
}
