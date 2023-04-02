using System;
using UnityEngine;

public class AutomaticDialogueTrigger : MonoBehaviour
{
    public DialogueTemplate dialogue;
    [SerializeField] private bool oneTimeUse;
    private bool used;
    

    [SerializeField] private bool freezePlayer;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Collision Detected");
        if (other.CompareTag("Player") && (!oneTimeUse || !used) )
        {
            var dialogueManager = FindObjectOfType<DialogueManager>();
            if (freezePlayer)
            {
                dialogueManager.StartAutoFreezeDialogue(dialogue);
                
            }
            else
            {
                dialogueManager.StartAutoDialogue(dialogue);
            }
            used = true;
            ProgressionManager.Get().SetFirstAutoDialogueUsed();
        }
    }

     
}
