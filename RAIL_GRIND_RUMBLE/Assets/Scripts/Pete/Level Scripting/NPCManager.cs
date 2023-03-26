using UnityEngine;

public abstract class NPCManager : MonoBehaviour
{

    protected DialogueTemplate dialogueTemplate;
    protected virtual void Start()
    {
        dialogueTemplate = GetComponent<DialogueTrigger>().dialogue;
    }
    
    public virtual void HandleProgress()
    {
        
    }
}