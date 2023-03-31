using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DiegoManager : NPCManager
{

     //RAUL FACIAL ANIM TEST//////////////////////////////////////////////////////////////////////////
    public Animator DiegoAnimator;
    public DialogueManager script2;
    ////////////////////////////////////////////////////////////////////////////////////////////


    private bool mainQuest1Finished = false;

    private bool mainQuest2Finished = false;
    [SerializeField] private DialogueTemplate mainQuest2Dialogue;
    [SerializeField] private DialogueTemplate mainQuest3Dialogue;

    

    protected override void Start()
    {
        base.Start();
        HandleProgress();
        
    }

//RAUL FACIAL ANIM TEST//////////////////////////////////////////////////////////////////////////////////////
    void Update()
    {
        if (script2.isTalking == true)
        {
            DiegoAnimator.SetBool("isTalking", true);
        }

         if (script2.isTalking == false)
        {
            DiegoAnimator.SetBool("isTalking", false);
        }  
    }
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public override void HandleProgress()
    {
        List<Quest> completedQuests = ProgressionManager.CompletedQuests;
        foreach (Quest quest in completedQuests)
        {
            Debug.Log($"THE FOLLOWING QUEST IS COMPLETED: {quest.GetName()}");
            if(quest is MainQuest1)
            {
                mainQuest1Finished = true;
            }

            if (quest is MainQuest2)
            {
                mainQuest2Finished = true;
            }

            if (quest is MainQuest3)
            {
                gameObject.SetActive(false);
            }
        }

        if (mainQuest1Finished && !mainQuest2Finished)
        {
            GetComponent<MainQuest1Giver>().enabled = false;
            GetComponent<DialogueTrigger>().dialogue = mainQuest2Dialogue;
            GetComponent<DialogueTrigger>().dialogue.dialogueTrigger = GetComponent<DialogueTrigger>();
            GetComponent<MainQuest2Giver>().enabled = true;
        }

        if (mainQuest2Finished)
        {
            GetComponent<MainQuest1Giver>().enabled = false;
            GetComponent<MainQuest2Giver>().enabled = false;
            GetComponent<DialogueTrigger>().dialogue = mainQuest3Dialogue;
            GetComponent<DialogueTrigger>().dialogue.dialogueTrigger = GetComponent<DialogueTrigger>();
            GetComponent<MainQuest3Giver>().enabled = true;
        }
    }
}
