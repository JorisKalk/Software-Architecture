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

    private void UpdateMoney(int pValue)
    {
        money.value += pValue;
    }
}
