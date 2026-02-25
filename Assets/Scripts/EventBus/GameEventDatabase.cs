using UnityEngine;

public class EnemyDieEventData : EventData
{
    public Enemy enemy;
    public GameObject enemyObject;
    public EnemyDieEventData(Enemy pEnemy, GameObject pEnemyObject)
    {
        name = "EnemyDieEvent";
        enemy = pEnemy;
        enemyObject = pEnemyObject;
    }

    public override string ToString()
    {
        if (enemyObject == null)
            return "Enemy object already destroyed";
        else
        {
            return "Event name: " + name + "\n" +
                "Enemy Dropped Money: " + enemy.MoneyDropped + "\n" +
                "Enemy Gave XP: " + enemy.XP;
        }
    }
}

public class QuestCompleteEventData : EventData
{
    public QuestData quest;
    public QuestCompleteEventData(QuestData pQuest)
    {
        name = "QuestCompleteEvent";
        quest = pQuest;
    }

    public override string ToString()
    {
        return "Event name: " + name + "\n" +
            "Quest named [" + quest.questName + "] was completed.\n" +
            "Rewards:\n" +
            "Gold: " + quest.gold + "\n" +
            "XP: " + quest.xp;
    }
}

public class PotionUsedEventData : EventData
{
    public string potionUsed;
    public int healAmount;
    public PotionUsedEventData(string pPotionUsed, int pHealAmount)
    {
        potionUsed = pPotionUsed;
        healAmount = pHealAmount;
    }

    public override string ToString()
    {
        return "Event name: " + name + "\n" +
            "Potion used: " + potionUsed + "\n" +
            "Heals for: " + healAmount;
    }
}
