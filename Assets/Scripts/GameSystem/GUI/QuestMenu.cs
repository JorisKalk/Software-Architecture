using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using System;

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
        UpdateText();
        initialized = true;
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
    }

    private void OnEnable()
    {
        if (initialized)
        {
            UpdateText();
        }
    }

    public void UpdateText()
    {
        for (int i = infoPanels.Count - 1; i >= 0; i--)
        {
            
                QuestData quest = quests[i].GiveQuestData();
                infoPanels[i].GetComponent<QuestButtonScript>().UpdateText(quest);
                detailPanel.CheckForTextUpdate(quest);
            
        }
    }

}
