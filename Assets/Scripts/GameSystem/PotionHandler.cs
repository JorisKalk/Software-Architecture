using System;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

/// <summary>
/// Class that handles the amount of potions that the player has and publishes an event when a potion gets used.
/// </summary>
public class PotionHandler : MonoBehaviour
{
    [SerializeField]
    private List<PotionPanelData> potionPanels = new List<PotionPanelData>();
    [SerializeField]
    private GameEvent potionUsedEvent;

    private void Awake()
    {
        foreach (PotionPanelData potionPanelData in potionPanels)
        {
            potionPanelData.potionPanelController.ReceiveParent(this);
        }
    }

    public void OnEnemyDied(EventData eventData)
    {
        EnemyDieEventData enemyDieEvent = (EnemyDieEventData)eventData;
        Enemy enemy = enemyDieEvent.enemy;
        Debug.Log(enemy.PotionTypeDropped);
        if (enemy.PotionsDropped > 0)
        {
            foreach (PotionPanelData potionPanel in potionPanels)
            {
                if (enemy.PotionTypeDropped == potionPanel.potionName)
                {
                    potionPanel.amountInInventory += enemy.PotionsDropped;
                    if (potionPanel.potionPanelController.isActiveAndEnabled)
                    {
                        potionPanel.potionPanelController.UpdateValues(potionPanel.potionName,
                            potionPanel.amountInInventory, potionPanel.healAmount);
                    }
                }
            }
        }
    }

    public void RequestValues(PotionInventoryPanel requester)
    {
        foreach (PotionPanelData potionPanel in potionPanels)
        {
            if (potionPanel.potionPanelController == requester)
            {
                requester.UpdateValues(potionPanel.potionName,
                    potionPanel.amountInInventory, potionPanel.healAmount);
            }
        }
    }

    public void PotionUsed(PotionInventoryPanel caller)
    {
        foreach (PotionPanelData potionPanel in potionPanels)
        {
            if (potionPanel.potionPanelController == caller)
            {
                potionUsedEvent.Publish(new PotionUsedEventData(potionPanel.potionName, potionPanel.healAmount),
                    caller.gameObject);
                potionPanel.amountInInventory--;
                caller.UpdateValues(potionPanel.potionName,
                    potionPanel.amountInInventory, potionPanel.healAmount);
            }
        }
    }
}

[Serializable]
public class PotionPanelData
{
    public PotionInventoryPanel potionPanelController;
    public string potionName;
    public int healAmount;
    public int amountInInventory = 0;
}
