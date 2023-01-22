using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueTemplate dialogue;
    [SerializeField] private GameObject talkPrompt;
    private ThirdPersonMovement thirdPersonControllerREF;
    
    
    void Start()
    {
        Debug.Log(gameObject.name);
        talkPrompt.SetActive(false);
        thirdPersonControllerREF = FindObjectOfType<ThirdPersonMovement>();
    }
    
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "PlayerObject")
        {
            talkPrompt.SetActive(true);
            thirdPersonControllerREF.nearestDialogueTemplate = dialogue;
        }
    }
    
    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "PlayerObject")
        {
            talkPrompt.SetActive(false);
            thirdPersonControllerREF.nearestDialogueTemplate = null;
        }
    }
}
