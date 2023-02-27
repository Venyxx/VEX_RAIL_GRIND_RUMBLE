using System;
using UnityEngine;

public class CountQuest : Quest
{
    protected int currentCount;
    [SerializeField] protected CountQuestType questType;
    [SerializeField] protected int completionCount;
    protected QuestTracker questTracker;
    [Tooltip("ONLY USABLE FOR COIN QUESTS")]
    public bool subtractCoinsOnCompletion;

    protected void Start()
    {
        questTracker = FindObjectOfType<QuestTracker>();
    }

    public CountQuestType GetCountQuestType()
    {
        return questType;
    }

    public virtual void IncrementCount()
    {
        //Debug.Log("IncrementCount('Progress')");
        IncrementCount("Progress");
    }

    public virtual void IncrementCount(string thingToCount)
    {
        //Debug.Log("Parameterized IncrementCount");
        currentCount++;
        questTracker.QuestInfoText.text = $"{thingToCount}: {currentCount} / {completionCount}";
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
    //Graffiti,
    //SpeedMeter,
    Coins,
    Enemies,
    None
};
