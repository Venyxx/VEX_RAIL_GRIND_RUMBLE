using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.Rendering.Universal;

public class DialogueManager : MonoBehaviour
{ 
    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI talkingToName;
    public GameObject dialogueBox;
    public GameObject questWindow;
    private Queue<string> paragraphDisplayed;
    private bool isBoxActive = false;
    [SerializeField] private float textSpeed = 0.1f;
    [SerializeField] private float npcRotationSpeed = 5;

    private ThirdPersonMovement thirdPersonControllerREF;

    [SerializeField] private GameObject talkPrompt;
    private GameObject npcModel;
    private GameObject ariRig;
    private bool rotatingNPC;
    private bool rotatingBack;
    
    private string lastString;
    
    // Start is called before the first frame update
    private void Start()
    {
        paragraphDisplayed = new Queue<string>();
        textComponent.text = string.Empty;
        talkingToName.text = string.Empty;
        
        thirdPersonControllerREF = FindObjectOfType<ThirdPersonMovement>();
        ariRig = GetAriRig();
        if (questWindow == null)
        {
            questWindow = GameObject.Find("QuestWindow");

        }
        questWindow.SetActive(false);
        dialogueBox.SetActive(false);
    }
    
    private void Update()
    {
        if (rotatingNPC && npcModel != null)
        {
            RotateNPC();
        }
    }
    
    //returns AriRig so we don't have to SerializeField or use tags; more performant than GameObject.Find()
    private GameObject GetAriRig()
    {
        foreach (Transform childTransform in thirdPersonControllerREF.transform)
        {
            if (childTransform.name.Equals("AriRig"))
            {
                return childTransform.gameObject;
            }
        }
        throw new Exception("Ari Model not named 'AriRig'; could not find model. Change model name to 'AriRig'");
    }

    //rotating ariadna causes some bugginess when you finish walking. ask vanessa for help
    private void RotateNPC()
    {
        Vector3 lookDirectionNPC = thirdPersonControllerREF.gameObject.transform.position - npcModel.transform.position;
        Vector3 lookDirectionAri = npcModel.transform.position - thirdPersonControllerREF.gameObject.transform.position;
        lookDirectionNPC.y = 0;
        lookDirectionAri.y = 0;
        lookDirectionNPC.Normalize();
        lookDirectionAri.Normalize();
        
        npcModel.transform.rotation = Quaternion.Slerp(npcModel.transform.rotation, Quaternion.LookRotation(lookDirectionNPC), npcRotationSpeed * Time.deltaTime);
        ariRig.transform.rotation = Quaternion.Slerp(ariRig.transform.rotation, Quaternion.LookRotation(lookDirectionAri), npcRotationSpeed * Time.deltaTime); //comment out this line to stop ari from rotating
    }
    
    
    public void StartDialogue(DialogueTemplate dialogue)
    {
        if (dialogue == null || dialogue.dialogueTrigger == null|| !thirdPersonControllerREF.isWalking) return;
        
        dialogueBox.SetActive(true);
        talkPrompt.SetActive(false);
        isBoxActive = true;
        talkingToName.text = dialogue.name;
        npcModel = dialogue.dialogueTrigger.npcModel;
        rotatingNPC = true;
        
        paragraphDisplayed.Clear();

        foreach (string paragraph in dialogue.paragraphs)
        {
            paragraphDisplayed.Enqueue(paragraph);
        }

        DisplayNextParagraph();
    }
    
    //inputactions method
    public void DialogueInput(InputAction.CallbackContext context)
    {
        if (!context.started || PauseMenu.isPaused || InfoScreen.isOpen) return;

        if (!isBoxActive && context.started)
        {
            StartDialogue(thirdPersonControllerREF.nearestDialogueTemplate);
        }
        else if (context.started && isBoxActive)
        {
            DisplayNextParagraph();
        }
    }
    
    private void DisplayNextParagraph ()
    {
        if (paragraphDisplayed.Count == 0)
        {
            EndDialogue(lastString);
            return;
        }
        string paragraph = paragraphDisplayed.Dequeue();
        lastString = paragraph;
        StopAllCoroutines();
        StartCoroutine(TypeParagraph(paragraph));
    }

    private IEnumerator TypeParagraph(string paragraph)
    {
        textComponent.text = "";
        foreach (char c in paragraph)
        {
            
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    private void EndDialogue(string text)
    {
        dialogueBox.SetActive(false);
        isBoxActive = false;
        thirdPersonControllerREF.nearestDialogueTemplate = null;
        rotatingNPC = false;
        try
        {
            var questGiver = npcModel.transform.parent.GetComponentInChildren<QuestGiver>();
            Quest quest = questGiver.GetQuest();
            if (!questGiver.acceptedOrDeniedAlready && !quest.isComplete && !quest.isActive)
            {
                questGiver.OpenQuestWindow();
                questGiver.acceptedOrDeniedAlready = true;
            }
            else
            {
                questGiver.acceptedOrDeniedAlready = false;
            }
            
            Debug.Log($"Attempting to Activate RivalQuest {text}, {quest.QuestAcceptedText}");
            if (quest is RivalQuest rivalQuest && quest.QuestAcceptedText.Equals(text))
            {
                rivalQuest.Activate();
            }
            
            //Debug.Log($"Quest is marked complete? {questGiver.GetQuest().isComplete}");
            //Debug.Log($"QuestRewards marked as Given? {questGiver.GetQuest().RewardsGiven}");
            if (quest.isComplete && !quest.RewardsGiven)
            {
                quest.RewardPlayer();
                quest.RewardsGiven = true;
                FindObjectOfType<QuestTracker>().QuestInfoText.text = "";
            }
        }
        catch (NullReferenceException e)
        {
            Debug.Log("There is no QuestGiver attached to this Dialogue");
        }

        
    }
}
