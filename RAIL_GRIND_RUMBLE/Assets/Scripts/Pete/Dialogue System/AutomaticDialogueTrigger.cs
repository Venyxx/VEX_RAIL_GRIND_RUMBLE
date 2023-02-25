using System;
using UnityEngine;

public class AutomaticDialogueTrigger : MonoBehaviour
{
    public DialogueTemplate dialogue;
    [SerializeField] private bool oneTimeUse;
    private bool used;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision Detected");
        if (other.CompareTag("Player") && (!oneTimeUse || !used))
        {
            Debug.Log("Starting Diego Dialogue");
            FindObjectOfType<DialogueManager>().StartAutoDialogue(dialogue);
            used = true;
        }
    }
}
