using UnityEngine;

public abstract class AbstractQuest : MonoBehaviour
{
    [SerializeField]
    protected QuestHandler handler;

    [SerializeField]
    protected string targetQuestName;

    protected QuestData questData;

    void Start()
    {
        questData = handler.FindData(targetQuestName);
        if (questData == null)
        {
            Debug.Log("Couldn't find quest with name: " + targetQuestName);
            Destroy(this);
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
        Debug.Log(questData.questMessage + "\n" +
            "Progress: " + questData.progress + " / " + questData.goal + "\n" +
            "Rewards:\n" +
            "Gold: " + questData.gold + "\n" +
            "XP: " + questData.xp);
    }

    public abstract void GetEvent(EventData eventData);
}
