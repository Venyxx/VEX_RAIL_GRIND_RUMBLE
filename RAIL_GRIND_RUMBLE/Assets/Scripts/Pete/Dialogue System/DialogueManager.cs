using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.ProBuilder.MeshOperations;

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
    
    // Start is called before the first frame update
    private void Start()
    {
        paragraphDisplayed = new Queue<string>();
        textComponent.text = string.Empty;
        talkingToName.text = string.Empty;
        dialogueBox.SetActive(false);
        thirdPersonControllerREF = FindObjectOfType<ThirdPersonMovement>();
        ariRig = GetAriRig();
        questWindow = GameObject.Find("QuestWindow");
        questWindow.SetActive(false);
        Debug.Log(questWindow == null);
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
        if (!context.started) return;

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
            EndDialogue();
            return;
        }

        string paragraph = paragraphDisplayed.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeParagraph(paragraph));
    }

    private IEnumerator TypeParagraph(string paragraph)
    {
        textComponent.text = "";
        foreach (char c in paragraph.ToCharArray())
        {
            
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    private void EndDialogue()
    {
        dialogueBox.SetActive(false);
        isBoxActive = false;
        thirdPersonControllerREF.nearestDialogueTemplate = null;
        rotatingNPC = false;
        try
        {
            var questGiver = npcModel.transform.parent.GetComponentInChildren<QuestGiver>();
            if (!questGiver.acceptedOrDeniedAlready && !questGiver.GetQuest().isComplete && !questGiver.GetQuest().isActive)
            {
                questGiver.OpenQuestWindow();
                questGiver.acceptedOrDeniedAlready = true;
            }
            else
            {
                questGiver.acceptedOrDeniedAlready = false;
            }
        }
        catch (NullReferenceException e)
        {
            
        }

        
    }
}
