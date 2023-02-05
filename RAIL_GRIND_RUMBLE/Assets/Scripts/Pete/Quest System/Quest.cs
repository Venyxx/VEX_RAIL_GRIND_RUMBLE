using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[System.Serializable]
public class Quest : MonoBehaviour
{
    public bool isActive;
    public bool isComplete;
    [SerializeField] private string name;
    [SerializeField] private string description;
    public bool RewardsGiven { get; set; } = false;
    public List<QuestReward> questRewards;
    
    [TextArea(3,10)] [SerializeField] private string questAcceptedText;
    [TextArea(3,10)] [SerializeField] private string questDeniedText;
    [TextArea(3,10)] [SerializeField] private string[] questCompletedText;
    
    public string QuestAcceptedText => questAcceptedText;
    public string QuestDeniedText => questDeniedText;
    public string[] QuestCompletedText => questCompletedText;

    public string GetName()
    {
        return name;
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
        questRewards = new List<QuestReward>();
    }

    public void RewardPlayer()
    {
        foreach (var reward in questRewards)
        {
            reward.RewardPlayer();
        }

        RewardsGiven = true;
    }

}
