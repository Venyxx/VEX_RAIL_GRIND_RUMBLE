using System.Collections.Generic;
using System.Text;
using StarterAssets;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public QuestReward[] questRewards;
    public bool isActive;
    public bool isComplete;
    [SerializeField] private string questName;
    [SerializeField] private string description;
    public bool RewardsGiven { get; set; } = false;
    //private DialogueTrigger dialogueTrigger;
    public DialogueTemplate questAcceptedText;
    public DialogueTemplate questDeniedText;
    public DialogueTemplate questCompletedText;

    /*[TextArea(3,10)] [SerializeField] protected string questAcceptedText;
    [TextArea(3,10)] [SerializeField] protected string questDeniedText;
    [TextArea(3,10)] [SerializeField] protected string[] questCompletedText;
    [SerializeField] protected string[] questCompletedSpeakers;
    
    public string QuestAcceptedText => questAcceptedText;
    public string QuestDeniedText => questDeniedText;
    public string[] QuestCompletedText => questCompletedText;
    public string[] QuestCompletedSpeakers => questCompletedSpeakers;*/

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
        StringBuilder sb = new StringBuilder();
        foreach (var reward in questRewards)
        {
            
            sb.Append($"{reward.ToString()}\n");
        }
        return sb.ToString();
    }
    

    public void RewardPlayer()
    {
        foreach (var reward in questRewards)
        {
            reward.RewardPlayer();
        }
        
        ProgressionManager tracker = ProgressionManager.Get();
        if (tracker.currentQuest is CountQuest)
        {
            CountQuest countQuest = (CountQuest)tracker.currentQuest;
            if (countQuest.subtractCoinsOnCompletion && countQuest.GetCountQuestType() is CountQuestType.Coins)
            {
                ThirdPersonMovement player = ProgressionManager.Get().Player;
                player.AddCoin(-countQuest.GetCompletionCount());
            }
        }
        
    }

}
