using UnityEngine;
using System;

public class MoneyController : MonoBehaviour
{
    [SerializeField]
    private IntValue money;

    private void Start()
    {
        money.value = 0;
    }

    public void OnEnemyDied(EventData eventData)
    {
        EnemyDieEventData enemyDieEvent = (EnemyDieEventData)eventData;
        UpdateMoney(enemyDieEvent.enemy.MoneyDropped);
    }

    public void OnQuestCompleted(EventData eventData)
    {
        QuestCompleteEventData questCompleteEvent = (QuestCompleteEventData)eventData;
        UpdateMoney(questCompleteEvent.quest.gold);
    }

    private void UpdateMoney(int pValue)
    {
        money.value += pValue;
    }
}
