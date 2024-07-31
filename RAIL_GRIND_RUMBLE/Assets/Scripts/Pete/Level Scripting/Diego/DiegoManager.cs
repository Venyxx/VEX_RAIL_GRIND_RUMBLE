using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class DiegoManager : NPCManager
{

     //RAUL FACIAL ANIM TEST//////////////////////////////////////////////////////////////////////////
    public Animator DiegoAnimator;
    public DialogueManager script2;
    ////////////////////////////////////////////////////////////////////////////////////////////

    [SerializeField] private DialogueTemplate firstMissionDialogue;
    private bool mission1Ready = true;

    protected override void Start()
    {
        base.Start();
        //HandleProgress();
        
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

    //TODO: Progression Code Commented Out/Removed
    public override void HandleProgress()
    {
        //ProgressionManager.Get().PrologueTalkedToJose();
        //ProgressionManager.Get().PrologueTalkedToJulie();
        if (mission1Ready)
        {
            ProgressionManager.Get().progressStage = 1;
            GameObject doorTrigger = GameObject.Find("DoorTrigger");
            LoadNewScene sceneLoader = doorTrigger.GetComponent<LoadNewScene>();
            AutomaticDialogueTrigger automaticDialogueTrigger = doorTrigger.GetComponent<AutomaticDialogueTrigger>();

            sceneLoader.enabled = true;
            automaticDialogueTrigger.enabled = false;

        }
    }

    public void ActivateMission1Dialogue()
    {
        Debug.Log("mission 1 is ready to start!");
        DialogueTrigger diegoTrigger = GetComponent<DialogueTrigger>();
        diegoTrigger.dialogue = firstMissionDialogue;
        firstMissionDialogue.dialogueTrigger = diegoTrigger;
        mission1Ready = true;
    }
}
