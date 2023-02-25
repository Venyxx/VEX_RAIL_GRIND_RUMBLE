using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    [SerializeField] private Quest questToGive;

    private PauseMenu pauseMenu;
    
    [SerializeField]private TextMeshProUGUI questTitleText;
    [SerializeField]private TextMeshProUGUI questDescrText;
    [SerializeField]private TextMeshProUGUI questRewardText;

    public bool acceptedOrDeniedAlready = false;


    void Start()
    {
        questToGive = GetComponent<Quest>();
        pauseMenu = FindObjectOfType<PauseMenu>();
        if(questTitleText == null)
            questTitleText = GameObject.Find("QuestNameField").GetComponent<TextMeshProUGUI>();
        if(questDescrText == null)
            questDescrText = GameObject.Find("DescriptionField").GetComponent<TextMeshProUGUI>();
        if(questRewardText == null)
            questRewardText = GameObject.Find("RewardsText").GetComponent<TextMeshProUGUI>();
        Debug.Log("Finished Start in QuestGiver");
    }

    // Update is called once per frame
    void Update()
    {
        if (questToGive.isComplete && !questToGive.RewardsGiven)
        {
            GetComponent<DialogueTrigger>().dialogue.paragraphs.spokenDialogue = questToGive.QuestCompletedText;
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
        temp.paragraphs = new DialogueParagraph();
        temp.dialogueTrigger = GetComponent<DialogueTrigger>();
        temp.paragraphs.speakers = temp.dialogueTrigger.dialogue.paragraphs.speakers;
        temp.paragraphs.spokenDialogue = new[] { questToGive.QuestAcceptedText };
        FindObjectOfType<DialogueManager>().StartNPCDialogue(temp);
    }

    public void DenyQuest()
    {
        pauseMenu.ResumeGame();
        DialogueTemplate temp = new DialogueTemplate();
        temp.paragraphs = new DialogueParagraph();
        temp.dialogueTrigger = GetComponent<DialogueTrigger>();
        temp.paragraphs.speakers = temp.dialogueTrigger.dialogue.paragraphs.speakers;
        temp.paragraphs.spokenDialogue = new[] { questToGive.QuestDeniedText };
        FindObjectOfType<DialogueManager>().StartNPCDialogue(temp);
    }

    public Quest GetQuest()
    {
        return questToGive;
    }

    public void FailQuest()
    {
        //questDescrText.text = "You Failed! Try Again?";
        SceneManager.LoadScene("LoseScene");
    }
}

        