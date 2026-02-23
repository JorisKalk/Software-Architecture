using UnityEngine;
using System.Collections.Generic;

public class QuestHandler : MonoBehaviour
{
    [SerializeField]
    public List<QuestData> quests = new List<QuestData>();
    [SerializeField]
    private GameEvent questCompleteEvent;

    void Start()
    {
        int something = quests.Count;
    }

    void Update()
    {
        
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
        questCompleteEvent.Publish(new QuestCompleteEventData(data), this.gameObject);
    }
}
