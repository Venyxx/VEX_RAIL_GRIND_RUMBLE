using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DiegoManager : NPCManager
{
    private bool mainQuest1Finished = false;

    private bool mainQuest2Finished = false;
    //[SerializeField] private DialogueTemplate mainQuest2Dialogue;
    

    public override void HandleProgress()
    {
        List<Quest> completedQuests = ProgressionManager.CompletedQuests;
        foreach (Quest quest in completedQuests)
        {
            if(quest is MainQuest1)
            {
                mainQuest1Finished = true;
            }

            if (quest is MainQuest2)
            {
                mainQuest2Finished = true;
            }
        }

        if (mainQuest1Finished)
        {
            GetComponent<MainQuest1Giver>().enabled = false;
            //GetComponent<DialogueTrigger>().dialogue = mainQuest2Dialogue;
            //GetComponent<DialogueTrigger>().dialogue.dialogueTrigger = GetComponent<DialogueTrigger>();
            GetComponent<MainQuest2Giver>().enabled = true;
        }

        if (mainQuest2Finished)
        {
            GetComponent<MainQuest2Giver>().enabled = false;
            //GetComponent<DialogueTrigger>().dialogue = mainQuest2Dialogue;
            //GetComponent<DialogueTrigger>().dialogue.dialogueTrigger = GetComponent<DialogueTrigger>();
            GetComponent<MainQuest3Giver>().enabled = true;
        }
    }
}
