using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using System;

/// <summary>
/// Class that handles the displaying of quest information on the GUI.
/// </summary>
public class QuestMenu : MonoBehaviour
{
    [HideInInspector]
    public List<AbstractQuest> quests = new List<AbstractQuest>();

    [SerializeField]
    private QuestDetailPanel detailPanel;
    [SerializeField]
    private List<Button> infoPanels = new List<Button>();

    private bool initialized = false;

    private void Start()
    {
        if (detailPanel == null)
        {
            throw new Exception("Quest detail panel not attached to menu component");
        }
        else
        {
            foreach (Button panel in infoPanels)
            {
                panel.GetComponent<QuestButtonScript>().detailPanel = detailPanel;
            }
        }
        UpdateText();
        initialized = true;
    }

    public void UpdateText()
    {
        if (infoPanels.Count != 0)
        {
            for (int i = 0;  i < quests.Count; i++)
            {
                Debug.Log(i.ToString());
                Debug.Log("questslength" + quests.Count.ToString());
                QuestData quest = quests[i].GiveQuestData();
                infoPanels[i].GetComponent<QuestButtonScript>().UpdateText(quest);
                detailPanel.CheckForTextUpdate(quest);
            }
        }
    }

}
