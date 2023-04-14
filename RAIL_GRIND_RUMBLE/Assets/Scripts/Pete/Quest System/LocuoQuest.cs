using System;
using UnityEngine;

[System.Serializable]
public class LocuoQuest : Quest
{
    public LocuoQuestGiver questGiver;
    public void Activate()
    {
        Debug.Log("Locuo Quest Activate()");
        questGiver.Activate();
    }

    public override void RewardPlayer()
    {
        base.RewardPlayer();
        Debug.Log("LOCUO REWARD PLAYER");
        questGiver.RewardPlayer();
    }
}
