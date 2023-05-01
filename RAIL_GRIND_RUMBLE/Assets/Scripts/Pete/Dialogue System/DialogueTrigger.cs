using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueTemplate dialogue;
    protected static GameObject talkPrompt;
    protected ThirdPersonMovement thirdPersonControllerREF;
    public GameObject npcModel { get; private set; }
    private string homeSceneName = "";

    void Start()
    {
        if (homeSceneName == "")
        {
            homeSceneName = SceneManager.GetActiveScene().name;
        }

        //Debug.Log(gameObject.name);
        dialogue.dialogueTrigger = this;
        TalkPromptCheck();
        thirdPersonControllerREF = FindObjectOfType<ThirdPersonMovement>();
        foreach (Transform child in transform)
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
        if ((collision.gameObject.CompareTag("PlayerObject") ||
            collision.gameObject.CompareTag("Player")) && SceneManager.GetActiveScene().name == homeSceneName)
        {
            talkPrompt.SetActive(true);
            thirdPersonControllerREF.nearestDialogueTemplate = dialogue;
        }
        
    }

    private void OnTriggerStay(Collider collision)
    {
        if ((collision.gameObject.CompareTag("PlayerObject") ||
            collision.gameObject.CompareTag("Player")) && SceneManager.GetActiveScene().name == homeSceneName)
        {
            if (thirdPersonControllerREF.isWalking)
            {
                talkPrompt.GetComponent<TextMeshProUGUI>().SetText("Click or Press A to Talk");
            }
            else
            {
                talkPrompt.GetComponent<TextMeshProUGUI>().SetText("Enter Walk Mode to Talk");
            } 
        }

        
    }

    void OnTriggerExit(Collider collision)
    {

        if ((collision.gameObject.CompareTag("PlayerObject") ||
            collision.gameObject.CompareTag("Player")) && SceneManager.GetActiveScene().name == homeSceneName)
        {
            talkPrompt.SetActive(false);
            thirdPersonControllerREF.nearestDialogueTemplate = null;
        }
    }
    
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        dialogue.dialogueTrigger = this;
        TalkPromptCheck();
        thirdPersonControllerREF = FindObjectOfType<ThirdPersonMovement>();
        if (npcModel == null)
        {
            return;
        }

        if (homeSceneName != SceneManager.GetActiveScene().name)
        {
            npcModel.SetActive(false);
        }

        else
        {
            npcModel.SetActive(true);
        }
    }

    void TalkPromptCheck()
    {
        if (talkPrompt == null)
        {
            talkPrompt = GameObject.Find("TalkPrompt");

        }
        if (talkPrompt != null && talkPrompt.activeInHierarchy)
        {
            talkPrompt.SetActive(false);
        }
    }

    void Update()
    {
        if (dialogue.dialogueTrigger == null)
        {
            dialogue.dialogueTrigger = this;
        }
    }
}

