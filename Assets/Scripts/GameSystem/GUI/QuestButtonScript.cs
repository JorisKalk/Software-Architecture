using TMPro;
using UnityEngine;

/// <summary>
/// This script displays a short version of the quest info on a button and makes the quest menu display
/// more info when the button is clicked.
/// </summary>
public class QuestButtonScript : MonoBehaviour
{
    [HideInInspector]
    public QuestDetailPanel detailPanel;

    [SerializeField]
    private TextMeshProUGUI textComponent;

    private QuestData questData;

    public void UpdateText(QuestData data)
    {
        questData = data;
        if (data.progress >= data.goal)
        {
            textComponent.text = questData.questMessage + "\n" + 
                "Complete";
        }
        else
        {
            textComponent.text = questData.questMessage + "\n" +
                "Progress: " + questData.progress.ToString() + "/" + questData.goal.ToString();
        }
    }

    public void OnButtonPressed()
    {
        if (detailPanel != null)
        {
            detailPanel.UpdateText(questData);
        }
    }
}
