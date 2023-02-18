using System;
using UnityEngine;

public abstract class QuestReward : MonoBehaviour
{
    
    //THESE METHODS ARE NOT SUPPOSED TO BE IMPLEMENTED, IMPLEMENT THEM IN SUBCLASSES (LIKE CoinReward)
    public virtual void RewardPlayer()
    {
        //throw new NotImplementedException();
    }

    public virtual string ToString()
    {
        //throw new NotImplementedException();
        return "";
    }
}
