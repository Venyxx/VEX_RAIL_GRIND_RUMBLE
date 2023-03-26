using UnityEngine;

public class OutskirtsDiegoManager : NPCManager
{
    [SerializeField] private DialogueTemplate secondSetDialogue;
    [SerializeField] private DialogueTemplate thirdSetDialogue;
    [SerializeField] private GameObject dialogueGoon1;
    [SerializeField] private GameObject dialogueGoon2;

    private DialogueTrigger dialogueTrigger;
    private int state = 1;

    protected override void Start()
    {
        base.Start();
        dialogueTrigger = GetComponent<DialogueTrigger>();
    }

    public override void HandleProgress()
    {
        if (state == 1)
        {
            StartCoroutine(DialogueManager.DialogueWipe());
            Invoke(nameof(FirstStageProgress), 0.5f);
            
        }

        if (state == 2)
        {
            StartCoroutine(DialogueManager.DialogueWipe());
            Invoke(nameof(SecondStageProgress), 0.5f);
            
        }

        if (state == 3)
        {
            StartCoroutine(DialogueManager.DialogueWipe());
            Invoke(nameof(ThirdStageProgress), 0.5f);
        }

        state++;
    }

    private void FirstStageProgress()
    {
        transform.position = new Vector3(86.4700012f, 37.3800011f, 722.169983f);
        ProgressionManager.Get().SetQuestInfoText("Meet Diego and see what he's got");
        dialogueTrigger.dialogue = secondSetDialogue;
        secondSetDialogue.dialogueTrigger = dialogueTrigger;
    }

    private void SecondStageProgress()
    {
        dialogueGoon1.SetActive(true);
        dialogueGoon2.SetActive(true);
        dialogueTrigger.dialogue = thirdSetDialogue;
        thirdSetDialogue.dialogueTrigger = dialogueTrigger;
        //ACTIVATE GRAPPLING HERE!!! 
        FindObjectOfType<DialogueManager>().StartAutoFreezeDialogue(thirdSetDialogue);
    }

    private void ThirdStageProgress()
    {
        dialogueGoon1.SetActive(false);
        dialogueGoon2.SetActive(false);
        ProgressionManager.Get().SetQuestInfoText("Fight off the goons!");
        transform.GetChild(0).gameObject.SetActive(false);
        Invoke(nameof(Deactivate), 1f);
    }

    private void Deactivate()
    {
        ProgressionManager.Get().mainQuest3.BeginCombat();
        //GetComponent<BoxCollider>().enabled = false;
        gameObject.SetActive(false);
    }
}
