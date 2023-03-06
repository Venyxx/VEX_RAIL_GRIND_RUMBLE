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
        /*dialogueTrigger = GetComponent<DialogueTrigger>();
        questAcceptedText.dialogueTrigger = dialogueTrigger;
        questCompletedText.dialogueTrigger = dialogueTrigger;
        questDeniedText.dialogueTrigger = dialogueTrigger;*/
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
