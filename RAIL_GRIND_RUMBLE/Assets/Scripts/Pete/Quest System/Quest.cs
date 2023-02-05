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
    public List<QuestReward> questRewards;

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

}
