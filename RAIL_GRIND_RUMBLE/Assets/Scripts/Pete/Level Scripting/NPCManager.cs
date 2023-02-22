using UnityEngine;

namespace Pete.Level_Scripting
{
    public abstract class NPCManager : MonoBehaviour
    {

        protected DialogueTemplate dialogueTemplate;
        protected void Start()
        {
            dialogueTemplate = GetComponent<DialogueTrigger>().dialogue;
        }
        
        public virtual void HandleProgress()
        {
            
        }
    }
}