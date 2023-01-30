using System;
using TMPro;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueTemplate dialogue;
    [SerializeField] private GameObject talkPrompt;
    private ThirdPersonMovement thirdPersonControllerREF;
    public GameObject npcModel { get; private set; }


    void Start()
    {
        //Debug.Log(gameObject.name);
        dialogue.dialogueTrigger = this;
        talkPrompt.SetActive(false);
        thirdPersonControllerREF = FindObjectOfType<ThirdPersonMovement>();
        foreach (Transform child in transform.parent)
        {
            if (child.gameObject.CompareTag("NPC Model"))
            {
                npcModel = child.gameObject;
            }
        }

        if (npcModel == null)
        {
            throw new Exception("NPC Model is missing 'NPC Model' Tag; NPC Model is Null");
        }
    }
    
    void OnTriggerEnter(Collider collision)
    {
        if (!collision.gameObject.CompareTag("PlayerObject")) return;
        talkPrompt.SetActive(true);
        thirdPersonControllerREF.nearestDialogueTemplate = dialogue;
        
    }

    private void OnTriggerStay(Collider collision)
    {
        if (!(collision.gameObject.CompareTag("PlayerObject") || thirdPersonControllerREF.dialogueBox.activeInHierarchy))
        {
            return;
        }

        if (thirdPersonControllerREF.isWalking)
        {
            talkPrompt.GetComponent<TextMeshProUGUI>().SetText("Click or Press A to Talk");
        }
        else
        {
            talkPrompt.GetComponent<TextMeshProUGUI>().SetText("Enter Walk Mode to Talk");
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (!collision.gameObject.CompareTag("PlayerObject")) return;
        talkPrompt.SetActive(false);
        thirdPersonControllerREF.nearestDialogueTemplate = null;
    }
}
