using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestTracker : MonoBehaviour
{
    public Quest CurrentQuest { get; set; }
    public TextMeshProUGUI QuestInfoText { get; private set; }
    private static List<Quest> completedQuests;
    public CountQuestType CurrentCountQuestType { get; private set; } = CountQuestType.None;

    void Start()
    {
        completedQuests = new List<Quest>();
        try
        {
            QuestInfoText = GameObject.Find("QuestInfo").transform.Find("QuestInfoText").gameObject
                .GetComponent<TextMeshProUGUI>();
            QuestInfoText.text = "";
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to assign QuestInfoText");
        }
    }

    
    public void AcceptQuest(Quest quest)
    {
        foreach (var otherQuest in FindObjectsOfType<Quest>())
        {
            otherQuest.isActive = false;
        }
        CurrentQuest = quest;
        CurrentQuest.isActive = true;
        if (quest is CountQuest countQuest)
        {
            CurrentCountQuestType = countQuest.GetCountQuestType();
            QuestInfoText.text = $"Progress: {0} / {countQuest.GetCompletionCount()}";
        }
    }
    

    public void CompleteQuest()
    {
        CurrentQuest.isActive = false;
        CurrentQuest.isComplete = true;
        completedQuests.Add(CurrentQuest);
        CurrentCountQuestType = CountQuestType.None;
        Debug.Log("Quest Completed");
    }
    
}
