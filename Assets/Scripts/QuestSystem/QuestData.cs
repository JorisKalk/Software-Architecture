using System;
using UnityEngine;

[Serializable]
public class QuestData
{
    public string questName;
    public int goal;
    public int progress = 0;
    public string questMessage;

    [Header("Rewards")]
    public int gold;
    public int xp;

    public QuestData(string pQuestName, int pGoal, string pQuestMessage, int pGold, int pXP)
    {
        questName = pQuestName;
        goal = pGoal;
        questMessage = pQuestMessage;
        gold = pGold;
        xp = pXP;
    }
}
