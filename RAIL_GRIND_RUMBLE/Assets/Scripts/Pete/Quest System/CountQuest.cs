using System;
using UnityEngine;

public class CountQuest : Quest
{
    private int currentCount;
    [SerializeField] private CountQuestType questType;
    [SerializeField] private int completionCount;
    private QuestTracker questTracker;
    [Tooltip("ONLY USABLE FOR COIN QUESTS")]
    public bool subtractCoinsOnCompletion;

    private void Start()
    {
        questTracker = FindObjectOfType<QuestTracker>();
    }

    public CountQuestType GetCountQuestType()
    {
        return questType;
    }

    public void IncrementCount()
    {
        currentCount++;
        questTracker.QuestInfoText.text = $"Progress: {currentCount} / {completionCount}";
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
