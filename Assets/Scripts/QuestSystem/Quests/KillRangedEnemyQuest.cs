using UnityEngine;

public class KillRangedEnemyQuest : AbstractQuest
{
    public override void GetEvent(EventData eventData)
    {
        EnemyDieEventData enemyDieEvent = (EnemyDieEventData)eventData;
        if (enemyDieEvent.enemy.EnemyType == "Ranged") UpdateProgress(1);
    }
}
