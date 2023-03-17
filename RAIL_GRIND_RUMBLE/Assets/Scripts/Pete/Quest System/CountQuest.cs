using System;
using UnityEngine;

public class CountQuest : Quest
{
    protected int currentCount;
    [SerializeField] protected CountQuestType questType;
    [SerializeField] protected int completionCount;
    //protected ProgressionManager questTracker;
    [Tooltip("ONLY USABLE FOR COIN QUESTS")]
    public bool subtractCoinsOnCompletion;
    

    public CountQuestType GetCountQuestType()
    {
        return questType;
    }

    public virtual void IncrementCount()
    {
        switch (questType)
        {
            case CountQuestType.Coins:
                IncrementCount("Coins Collected");
                break;
            case CountQuestType.Enemies:
                IncrementCount("Enemies Defeated");
                break;
            case CountQuestType.Graffiti:
                IncrementCount("Graffiti Sprayed");
                break;
            default:
                IncrementCount("Progress");
                break;
        }
    }

    public virtual void IncrementCount(string thingToCount)
    {
        //Debug.Log("Parameterized IncrementCount");
        currentCount++;
        ProgressionManager.Get().QuestInfoText.text = $"{thingToCount}: {currentCount} / {completionCount}";
        //Debug.Log(currentCount);
        if (currentCount >= completionCount)
        {
            //Debug.Log("Count Quest Completed");
            ProgressionManager.Get().CompleteQuest();
        }
    }

    public void ResetCount()
    {
        currentCount = 0;
    }

    public int GetCompletionCount()
    {
        return completionCount;
    }
}


//COINS AND ENEMIES ARE IMPLEMENTED, THE REST ARE NOT 
[Serializable]
public enum CountQuestType
{
    //GrindTime,
    Graffiti,
    //SpeedMeter,
    Coins,
    Enemies,
    None
};
