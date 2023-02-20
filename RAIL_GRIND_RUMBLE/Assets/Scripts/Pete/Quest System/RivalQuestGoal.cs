using System;
using UnityEngine;

public class RivalQuestGoal : MonoBehaviour
{
    private RivalQuest myQuest;

    public void SetMyQuest(RivalQuest quest)
    {
        myQuest = quest;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("PlayerObject")) return;
        myQuest.PlayerWin();
    }
}
