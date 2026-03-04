using UnityEngine;
using System;

/// <summary>
/// Receives events that give the player more money and changes the money value accordingly.
/// </summary>
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
