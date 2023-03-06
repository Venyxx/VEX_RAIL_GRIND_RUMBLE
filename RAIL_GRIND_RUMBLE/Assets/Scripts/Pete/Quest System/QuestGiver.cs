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
            //GetComponent<DialogueTrigger>().dialogue.paragraphs.spokenDialogue = questToGive.QuestCompletedText;
            //GetComponent<DialogueTrigger>().dialogue.paragraphs.speakers = questToGive.QuestCompletedSpeakers;
            GetComponent<DialogueTrigger>().dialogue = questToGive.questCompletedText;
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
        Debug.Log("Quest accepted text: " + questToGive.questAcceptedText.paragraphs[0].englishDialogue);
        FindObjectOfType<DialogueManager>().StartNPCDialogue(questToGive.questAcceptedText);
    }

    public void DenyQuest()
    {
        pauseMenu.ResumeGame();
        FindObjectOfType<DialogueManager>().StartNPCDialogue(questToGive.questDeniedText);

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

        