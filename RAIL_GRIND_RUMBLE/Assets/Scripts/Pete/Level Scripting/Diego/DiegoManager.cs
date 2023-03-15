using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Pete.Level_Scripting;
using UnityEngine;

public class DiegoManager : NPCManager
{
    private bool mainQuest1Finished = false;
    [SerializeField] private DialogueTemplate mainQuest2Dialogue;
    

    void Start()
    {
        List<Quest> completedQuests = ProgressionManager.CompletedQuests;
        foreach (Quest quest in completedQuests)
        {
            if(quest is MainQuest1)
            {
                mainQuest1Finished = true;
            }
        }

        if (mainQuest1Finished)
        {
            GetComponent<MainQuest1Giver>().enabled = false;
            GetComponent<DialogueTrigger>().dialogue = mainQuest2Dialogue;
        }
    }
}
