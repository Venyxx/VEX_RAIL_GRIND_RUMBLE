using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountQuest : Quest
{
    private int currentCount;
    [SerializeField] private CountQuestType questType;
    [SerializeField] private int completionCount;

    public CountQuestType GetCountQuestType()
    {
        return questType;
    }

    public void IncrementCount()
    {
        currentCount++;
        Debug.Log(currentCount);
        if (currentCount >= completionCount)
        {
            Debug.Log("Count Quest Completed");
            FindObjectOfType<QuestTracker>().CompleteQuest();
        }
    }

    public void ResetCount()
    {
        currentCount = 0;
    }
}



[Serializable]
public enum CountQuestType
{
    GrindTime,
    Graffiti,
    SpeedMeter,
    Coins,
    Enemies,
    None
};
