using UnityEngine;
using System.Collections.Generic;

public class QuestHandler : MonoBehaviour
{
    [SerializeField]
    private List<QuestData> quests = new List<QuestData>();
    [SerializeField]
    private GameEvent questCompleteEvent;

    private List<QuestData> completedQuests = new List<QuestData>();

    void Start()
    {
        int something = quests.Count;
    }

    public QuestData FindData(string name)
    {
        foreach (QuestData data in quests)
        {
            if (data.questName == name)
            {
                QuestData result = data;
                quests.Remove(data);
                return result;
            }
        }
        return null;
    }

    public void CompleteQuest(QuestData data)
    {
        completedQuests.Add(data);
        questCompleteEvent.Publish(new QuestCompleteEventData(data), this.gameObject);
    }

    public List<QuestData> CompletedQuests()
    {
        return completedQuests;
    }
}
