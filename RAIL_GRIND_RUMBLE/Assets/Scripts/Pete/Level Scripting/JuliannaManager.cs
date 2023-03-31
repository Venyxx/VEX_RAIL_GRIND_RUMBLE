
using UnityEngine;

public class JuliannaManager: NPCManager
{
    public DialogueTemplate secondSetDialogue;


    //RAUL FACIAL ANIM TEST//////////////////////////////////////////////////////////////////////////
    public Animator JulieAnimator;
    public DialogueManager script;
    ////////////////////////////////////////////////////////////////////////////////////////////

    void Start()
    {
        ProgressionManager pm = ProgressionManager.Get();
        if (pm.prologueComplete)
        {
            GetComponent<DialogueTrigger>().dialogue = secondSetDialogue;
            secondSetDialogue.dialogueTrigger = GetComponent<DialogueTrigger>();
        }
    }

//RAUL FACIAL ANIM TEST//////////////////////////////////////////////////////////////////////////////////////
    void Update()
    {
        if (script.isTalking == true)
        {
            JulieAnimator.SetBool("isTalking", true);
        }

         if (script.isTalking == false)
        {
            JulieAnimator.SetBool("isTalking", false);
        }  
    }
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public override void HandleProgress()
    {
        ProgressionManager pm = ProgressionManager.Get();
        if (!pm.prologueComplete) return;

        pm.QuestInfoText.text = "Head to the skate park!";
    }
}
