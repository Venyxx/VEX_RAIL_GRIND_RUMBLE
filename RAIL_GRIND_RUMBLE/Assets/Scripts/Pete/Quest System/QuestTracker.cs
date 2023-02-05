using System.Collections.Generic;
using UnityEngine;

public class QuestTracker : MonoBehaviour
{
    public Quest CurrentQuest { get; set; }
    private static List<Quest> completedQuests;
    public CountQuestType CurrentCountQuestType { get; private set; } = CountQuestType.None;

    void Start()
    {
        completedQuests = new List<Quest>();
    }

    public void AcceptQuest(Quest quest)
    {
        CurrentQuest = quest;
        CurrentQuest.isActive = true;
        if (quest is CountQuest)
        {
            CountQuest countQuest = (CountQuest) quest;
            CurrentCountQuestType = countQuest.GetCountQuestType();
        }
    }

    public void CompleteQuest()
    {
        foreach (QuestReward reward in CurrentQuest.questRewards)
        {
            reward.RewardPlayer();
        }

        CurrentQuest.isActive = false;
        CurrentQuest.isComplete = true;
        completedQuests.Add(CurrentQuest);
        CurrentCountQuestType = CountQuestType.None;
        Debug.Log("Quest Completed");
    }
    
}
