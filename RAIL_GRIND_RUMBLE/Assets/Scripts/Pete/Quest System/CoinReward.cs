using Unity.VisualScripting;
using UnityEngine;

public class CoinReward : QuestReward
{
    
    public int coinAmount;

    public override void RewardPlayer()
    {
        Debug.Log("CoinReward Called");
        FindObjectOfType<ThirdPersonMovement>().AddCoin(coinAmount);
    }

    public override string ToString()
    {
        return $"Coins: " + coinAmount;
    }
}
