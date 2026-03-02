using UnityEngine;
using System;
using UnityEditor;

public abstract class AbstractQuest : MonoBehaviour
{
    [SerializeField]
    protected QuestHandler handler;

    [SerializeField]
    protected string targetQuestName;

    [SerializeField]
    protected GameObject questDetailsMenu;

    protected QuestData questData;

    protected QuestMenu infoMenu;

    protected void Awake()
    {
        questData = handler.FindData(targetQuestName);
        if (questData == null)
        {
            Debug.Log("Couldn't find quest with name: " + targetQuestName);
            Destroy(this);
        }

        infoMenu = questDetailsMenu.GetComponent<QuestMenu>();
        if (infoMenu == null)
        {
            throw new Exception("Couldn't find quest list");
        }
        else
        {
            infoMenu.quests.Add(this);
            Debug.Log("quest added");
        }

            UpdateQuestText();
    }

    protected void UpdateProgress(int pProgress)
    {
        questData.progress += pProgress;
        if (questData.progress >= questData.goal)
        {
            handler.CompleteQuest(questData);
            Destroy(this);
        }
        UpdateQuestText();
    }

    protected void UpdateQuestText()
    {
        infoMenu.UpdateText();
        Debug.Log(questData.questMessage + "\n" +
            "Progress: " + questData.progress + " / " + questData.goal + "\n" +
            "Rewards:\n" +
            "Gold: " + questData.gold + "\n" +
            "XP: " + questData.xp);
    }

    public QuestData GiveQuestData()
    {
        return questData;
    }

    public abstract void GetEvent(EventData eventData);
}
