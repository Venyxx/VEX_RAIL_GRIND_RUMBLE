using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using Cinemachine;

public class DialogueManager : MonoBehaviour
{ 

    //RAUL FACIAL ANIM//////////////////////////////////////////////////////////////////////////
    public bool isTalking = false;
    public bool doneTalking = false;
    // [SerializeField]
    // private CinemachineVirtualCamera BasicCam;
    // private CinemachineVirtualCamera NPCTalkingCam;


    ////////////////////////////////////////////////////////////////////////////////////////////


    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI talkingToName;
    public GameObject dialogueBox;
    public GameObject questWindow;
    public bool freezePlayer;
    private Queue<string> paragraphDisplayed;
    private Queue<string> nameDisplayed;
    private Queue<AudioClip> voiceClips;
    private bool isBoxActive = false;
    private float textSpeed = 0.01f;
    [SerializeField] private float npcRotationSpeed = 5;

    private ThirdPersonMovement thirdPersonControllerREF;

    [SerializeField] private GameObject talkPrompt;
    private GameObject npc;
    private GameObject ariRig;
    private bool rotatingNPC;
    private bool rotatingBack;
    
    private string lastString;
    private AudioSource audioSource;

    public bool SpawnHealth { get; set; } = false;

    

    // Start is called before the first frame update
    private void Start()
    {
        paragraphDisplayed = new Queue<string>();
        nameDisplayed = new Queue<string>();
        voiceClips = new Queue<AudioClip>();
        textComponent.text = string.Empty;
        talkingToName.text = string.Empty;
        audioSource = GetComponent<AudioSource>();
        
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
        if (rotatingNPC && npc != null)
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
        Vector3 lookDirectionNPC = thirdPersonControllerREF.gameObject.transform.position - npc.transform.position;
        Vector3 lookDirectionAri = npc.transform.position - thirdPersonControllerREF.gameObject.transform.position;
        lookDirectionNPC.y = 0;
        lookDirectionAri.y = 0;
        lookDirectionNPC.Normalize();
        lookDirectionAri.Normalize();
        
        npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation, Quaternion.LookRotation(lookDirectionNPC), npcRotationSpeed * Time.deltaTime);
        ariRig.transform.rotation = Quaternion.Slerp(ariRig.transform.rotation, Quaternion.LookRotation(lookDirectionAri), npcRotationSpeed * Time.deltaTime); //comment out this line to stop ari from rotating
    }
    
    
    public void StartNPCDialogue(DialogueTemplate dialogue)
    {
       


        /*Debug.Log("Attempting to call StartNPCDialogue");
        Debug.Log($"Is dialogue null? {dialogue == null}");
        Debug.Log($"Is dialogue.dialogueTrigger null? {dialogue.dialogueTrigger == null}");
        Debug.Log($"Is ari walking? {thirdPersonControllerREF.isWalking}");*/
        if (dialogue == null || !thirdPersonControllerREF.isWalking) return;
        //Debug.Log("StartNPCDialogue called successfully");
        freezePlayer = true;
        textSpeed = 0.01f;
        PlayDialogue(dialogue);
        
    }

    public void StartAutoFreezeDialogue(DialogueTemplate dialogue)
    {
        if (dialogue != null)
        {
            freezePlayer = true;
            textSpeed = 0.01f;
            PlayDialogue(dialogue);
        }
    }

    public void StartAutoDialogue(DialogueTemplate dialogue)
    {
        if (dialogue != null)
        {
            textSpeed = 0.03f;
            PlayDialogue(dialogue);
        }
    }

    private void PlayDialogue(DialogueTemplate dialogue)
    {
         //RAUL FACIAL ANIM//////////////////////////////////////////////////////////////////////////
        isTalking = true;
        //Debug.Log("IsTALKING");
        // SwitchPriority();

        ////////////////////////////////////////////////////////////////////////////////////////////
        
        dialogueBox.SetActive(true);
        talkPrompt.SetActive(false);
        isBoxActive = true;
        

        if (dialogue.dialogueTrigger != null)
        {
            npc = dialogue.dialogueTrigger.gameObject;
            rotatingNPC = true;
        }
        paragraphDisplayed.Clear();

        /*foreach (string paragraph in dialogue.paragraphs.spokenDialogue)
        {
            paragraphDisplayed.Enqueue(paragraph);
        }*/

        //Debug.Log("Length of paragraphs: " + dialogue.paragraphs.Length);
        foreach (var dialogueParagraph in dialogue.paragraphs)
        {
            //Debug.Log(dialogueParagraph.englishDialogue);
            paragraphDisplayed.Enqueue(dialogueParagraph.englishDialogue);
            nameDisplayed.Enqueue(dialogueParagraph.speakerName);
            voiceClips.Enqueue(dialogueParagraph.englishVoiceLine);
        }

        

        /*foreach (string name in dialogue.paragraphs.speakers)
        {
            nameDisplayed.Enqueue(name);
        }*/
        
        DisplayNextParagraph();
    }

    //inputactions method
    public void DialogueInput(InputAction.CallbackContext context)
    {
        //Debug.Log($"Pause Menu is paused? {PauseMenu.isPaused}");
        //Debug.Log($"InfoScreen is open? {InfoScreen.isOpen}");
        if (!context.started || PauseMenu.isPaused || InfoScreen.isOpen) return;

        if (!isBoxActive && context.started && thirdPersonControllerREF.nearestDialogueTemplate != null)
        {
            StartNPCDialogue(thirdPersonControllerREF.nearestDialogueTemplate);
        }
        else if (context.started && isBoxActive && freezePlayer)
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

        talkingToName.text = nameDisplayed.Dequeue();
        string paragraph = paragraphDisplayed.Dequeue();
        AudioClip clip = voiceClips.Dequeue();
        if (clip != null)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(clip);
        }
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

        if (!freezePlayer)
        {
            Invoke("DisplayNextParagraph", 2f);
        }
    }

    private void EndDialogue(string text)
    {

         //RAUL FACIAL ANIM//////////////////////////////////////////////////////////////////////////
        isTalking = false;
        doneTalking = true;
        //Debug.Log("DONESPEAKING");
        // SwitchPriority();

        ////////////////////////////////////////////////////////////////////////////////////////////
        
        
        dialogueBox.SetActive(false);
        isBoxActive = false;
        freezePlayer = false;
        audioSource.Stop();
        
        //Debug.Log($"ThirdPersonControllerREF is NULL {thirdPersonControllerREF == null}");
        //Debug.Log($"nearestDialogueTemplate is NULL {thirdPersonControllerREF.nearestDialogueTemplate == null}");
        //Debug.Log($"dialogueTrigger is NULL {thirdPersonControllerREF.nearestDialogueTemplate.dialogueTrigger == null}");

        DialogueTrigger trigger = null;
        if (thirdPersonControllerREF.nearestDialogueTemplate != null)
        {
            trigger = thirdPersonControllerREF.nearestDialogueTemplate.dialogueTrigger;
        }
        if (trigger != null && trigger is HealthSpawnDialogueTrigger hpSpawner)
        {
            hpSpawner.SpawnHealth();
        }

        NPCManager npcManager = null;
        if (thirdPersonControllerREF.nearestDialogueTemplate != null &&
            thirdPersonControllerREF.nearestDialogueTemplate.dialogueTrigger != null) 
        {
            npcManager = thirdPersonControllerREF.nearestDialogueTemplate.dialogueTrigger.gameObject.GetComponent<NPCManager>();
        }
        //Debug.Log($"NPC Manager is null: {npcManager == null}");
        
        HandleQuest(text);
        if (npcManager != null)
        {
            HandleProgression(npcManager);
        }
        else
        {
            //Debug.Log("NPC has no NPC Manager for Progression");
        }
        //thirdPersonControllerREF.nearestDialogueTemplate = null;
        rotatingNPC = false;
    }

    private void HandleQuest(string text)
    {
        try
        {
            QuestGiver[] questGivers = npc.GetComponents<QuestGiver>();
            QuestGiver questGiver = null;

            if (questGivers.Length == 1)
            {
                questGiver = npc.GetComponent<QuestGiver>();
            }
            else
            {
                foreach (var giver in questGivers)
                {
                    if (giver.enabled)
                    {
                        questGiver = giver;
                    }
                }
            }
            
            Quest quest = questGiver.GetQuest();
            /*Debug.Log(quest.GetName());
            Debug.Log($"!questGiver.acceptedOrDeniedAlready: {!questGiver.acceptedOrDeniedAlready}");
            Debug.Log($"!quest.isComplete: {!quest.isComplete}");
            Debug.Log($"!quest.isActive: {!quest.isActive}");*/
            if (!questGiver.acceptedOrDeniedAlready && !quest.isComplete && !quest.isActive)
            {
                //questGiver.OpenQuestWindow();
                questGiver.AcceptQuest();
                questGiver.acceptedOrDeniedAlready = true;
            }
            else
            {
                questGiver.acceptedOrDeniedAlready = false;
            }

            //Debug.Log($"Attempting to Activate RivalQuest {text}, {quest.QuestAcceptedText}");
            if (quest is LocuoQuest locuoQuest && quest.questAcceptedText.paragraphs[0].englishDialogue == text)
            {
                locuoQuest.Activate();
            }

            //Debug.Log($"Quest is marked complete? {questGiver.GetQuest().isComplete}");
            //Debug.Log($"QuestRewards marked as Given? {questGiver.GetQuest().RewardsGiven}");
            if (quest.isComplete && !quest.RewardsGiven)
            {
                quest.RewardPlayer();
                quest.RewardsGiven = true;
                ProgressionManager.Get().QuestInfoText.text = "";
            }
        }
        catch (NullReferenceException e)
        {
            //Debug.Log("There is no QuestGiver attached to this Dialogue");
        }
        catch (UnassignedReferenceException e)
        {
            //Debug.Log("There is no QuestGiver attached to this Dialogue");
        }

    }

    private void HandleProgression(NPCManager manager)
    {
        manager.HandleProgress();
    }

    public static IEnumerator DialogueWipe()
    {
        GameObject wipe = GameObject.Find("canvasPrefab").transform.Find("PauseWipeTransition").gameObject;
        wipe.SetActive(true);
        wipe.GetComponent<Animator>().CrossFade("PauseIntro", 0, 0);
        yield return new WaitForSeconds(1f);
        wipe.GetComponent<Animator>().CrossFade("PauseOutro", 0, 0);
        yield return new WaitForSeconds(0.2f);
        wipe.SetActive(false);
    }

}
