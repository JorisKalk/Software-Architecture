using UnityEngine;

public class CollectMediumPotionsQuest : AbstractQuest
{
    public override void GetEvent(EventData eventData)
    {
        EnemyDieEventData enemyDieEvent = (EnemyDieEventData)eventData;
        if (enemyDieEvent.enemy.PotionTypeDropped == "Medium") UpdateProgress(enemyDieEvent.enemy.PotionsDropped);
    }
}
