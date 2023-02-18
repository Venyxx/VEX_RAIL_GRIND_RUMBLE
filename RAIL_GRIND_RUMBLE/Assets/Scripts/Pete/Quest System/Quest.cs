using System.Collections.Generic;
using System.Text;
using StarterAssets;
using UnityEngine;

[System.Serializable]
public class Quest : MonoBehaviour
{
    public QuestReward[] questRewards;
    public bool isActive;
    public bool isComplete;
    [SerializeField] private string questName;
    [SerializeField] private string description;
    public bool RewardsGiven { get; set; } = false;
    
    
    [TextArea(3,10)] [SerializeField] private string questAcceptedText;
    [TextArea(3,10)] [SerializeField] private string questDeniedText;
    [TextArea(3,10)] [SerializeField] private string[] questCompletedText;
    
    public string QuestAcceptedText => questAcceptedText;
    public string QuestDeniedText => questDeniedText;
    public string[] QuestCompletedText => questCompletedText;

    public string GetName()
    {
        return questName;
    }

    public string GetDesc()
    {
        return description;
    }

    public string GetRewards()
    {
        int num = 1;
        StringBuilder sb = new StringBuilder();
        foreach (var reward in questRewards)
        {
            
            sb.Append($"{num}. {reward.ToString()}\n");
        }
        return sb.ToString();
    }

    void Start()
    {
        
    }

    public void RewardPlayer()
    {
        foreach (var reward in questRewards)
        {
            reward.RewardPlayer();
        }
        
        QuestTracker tracker = FindObjectOfType<QuestTracker>();
        if (tracker.CurrentQuest is CountQuest)
        {
            CountQuest countQuest = (CountQuest)tracker.CurrentQuest;
            if (countQuest.subtractCoinsOnCompletion && countQuest.GetCountQuestType() is CountQuestType.Coins)
            {
                ThirdPersonMovement player = FindObjectOfType<ThirdPersonMovement>();
                player.AddCoin(-countQuest.GetCompletionCount());
            }
        }
        
    }

}
