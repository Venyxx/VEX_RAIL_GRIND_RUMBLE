using System;
using UnityEngine;

public class AutomaticDialogueTrigger : MonoBehaviour
{
    public DialogueTemplate dialogue;
    [SerializeField] private bool oneTimeUse;
    
    private bool used;

    public Vector3 returnPlayerDirection;
    

    [SerializeField] private bool freezePlayer;
    [SerializeField] private bool blockPlayer;


    private void Start()
    {
        if (blockPlayer)
            freezePlayer = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Collision Detected");
        if (other.CompareTag("Player") && (!oneTimeUse || !used) )
        {
            var dialogueManager = FindObjectOfType<DialogueManager>();
            if (blockPlayer)
            {
                returnPlayerDirection = FindObjectOfType<ThirdPersonMovement>().MoveDirection * -1;
                dialogueManager.StartPlayerBlockedDialogue(dialogue, returnPlayerDirection);

            }
            else if (freezePlayer)
            {
                dialogueManager.StartAutoFreezeDialogue(dialogue);
                
            }
            else
            {
                dialogueManager.StartAutoDialogue(dialogue);
            }
            used = true;
            
            //TODO: Progression Code Commented Out/Removed
            //ProgressionManager.Get().SetFirstAutoDialogueUsed();
        }
        
    }

     
}
