using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    [SerializeField] private Quest questToGive;

    private PauseMenu pauseMenu;
    
    private TextMeshProUGUI questTitleText;
    private TextMeshProUGUI questDescrText;
    private TextMeshProUGUI questRewardText;

    public bool acceptedOrDeniedAlready = false;


    void Start()
    {
        questToGive = GetComponent<Quest>();
        pauseMenu = FindObjectOfType<PauseMenu>();
        questTitleText = GameObject.Find("QuestNameField").GetComponent<TextMeshProUGUI>();
        questDescrText = GameObject.Find("DescriptionField").GetComponent<TextMeshProUGUI>();
        questRewardText = GameObject.Find("RewardsText").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (questToGive.isComplete && !questToGive.RewardsGiven)
        {
            GetComponent<DialogueTrigger>().dialogue.paragraphs = questToGive.QuestCompletedText;
        }
    }

    public void OpenQuestWindow()
    {
        pauseMenu.ActivateQuestWindow();
        pauseMenu.acceptQuestButton.GetComponent<Button>().onClick.AddListener(this.AcceptQuest);
        pauseMenu.denyQuestButton.GetComponent<Button>().onClick.AddListener(this.DenyQuest);
        questTitleText.text = questToGive.GetName();
        questDescrText.text = questToGive.GetDesc();
        questRewardText.text = questToGive.GetRewards();
    }

    public void AcceptQuest()
    {
        pauseMenu.ResumeGame();
        FindObjectOfType<QuestTracker>().AcceptQuest(questToGive);
        DialogueTemplate temp = new DialogueTemplate();
        temp.dialogueTrigger = GetComponent<DialogueTrigger>();
        temp.name = temp.dialogueTrigger.dialogue.name;
        temp.paragraphs = new[] { questToGive.QuestAcceptedText };
        FindObjectOfType<DialogueManager>().StartDialogue(temp);
    }

    public void DenyQuest()
    {
        pauseMenu.ResumeGame();
        DialogueTemplate temp = new DialogueTemplate();
        temp.dialogueTrigger = GetComponent<DialogueTrigger>();
        temp.name = temp.dialogueTrigger.dialogue.name;
        temp.paragraphs = new[] { questToGive.QuestDeniedText };
        FindObjectOfType<DialogueManager>().StartDialogue(temp);
    }

    public Quest GetQuest()
    {
        return questToGive;
    }
}

        