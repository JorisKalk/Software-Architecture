using TMPro;
using UnityEngine;

/// <summary>
/// Class that displays received QuestData in the GUI.
/// </summary>
public class QuestDetailPanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;

    private QuestData questData;

    private void Start()
    {
        text.text = "";
    }

    public void UpdateText(QuestData data)
    {
        questData = data;
        if (questData.progress >= questData.goal)
        {
            text.text = questData.questMessage + "\n" +
                "Complete!\n" +
                "Rewards:\n" +
                "Gold: " + data.gold.ToString() + "\n" +
                "XP: " + data.xp.ToString();
        }
        else
        {
            text.text = questData.questMessage + "\n" +
                "Progress: " + questData.progress.ToString() + " / " + questData.goal.ToString() + "\n" +
                "Rewards:\n" +
                "Gold: " + data.gold.ToString() + "\n" +
                "XP: " + data.xp.ToString();
        }
    }

    public void CheckForTextUpdate(QuestData data)
    {
        if(questData != null && questData.questName == data.questName)
        {
            UpdateText(data);
        }
    }
}
