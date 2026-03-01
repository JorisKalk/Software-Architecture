using UnityEngine;

public class EndStageGate : MonoBehaviour
{
    public void OnEnemyKilled(EventData eventData)
    {
        EnemyDieEventData enemyDieEvent = (EnemyDieEventData)eventData;
        if (enemyDieEvent.enemy.EnemyType == "Boss")
        {
            Destroy(gameObject);
        }
    }
}
