using TMPro;
using UnityEngine;

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
