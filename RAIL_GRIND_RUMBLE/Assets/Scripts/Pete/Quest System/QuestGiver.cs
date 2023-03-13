using System;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    [Tooltip("LEAVE BLANK IF CHILD CLASS")]
    [SerializeField] protected Quest questToGive;
    
    protected PauseMenu pauseMenu;
    
    [SerializeField] protected TextMeshProUGUI questTitleText;
    [SerializeField] protected TextMeshProUGUI questDescrText;
    [SerializeField] protected TextMeshProUGUI questRewardText;

    public bool acceptedOrDeniedAlready = false;
    
    
    protected void Start()
    {
        pauseMenu = FindObjectOfType<PauseMenu>();
        
        if(questTitleText == null)
            questTitleText = GameObject.Find("QuestNameField").GetComponent<TextMeshProUGUI>();
        if(questDescrText == null)
            questDescrText = GameObject.Find("DescriptionField").GetComponent<TextMeshProUGUI>();
        if(questRewardText == null)
            questRewardText = GameObject.Find("RewardsText").GetComponent<TextMeshProUGUI>();
        Debug.Log("Finished Start in QuestGiver");

        string myQuestName = questToGive.GetName();
        string progressionManagerQuestName = ProgressionManager.Get().currentQuest.GetName();
        if (myQuestName == progressionManagerQuestName)
        {
            questToGive = ProgressionManager.Get().currentQuest;
        }
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
        ProgressionManager.Get().AcceptQuest(questToGive);
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

enum QuestType
{
    MainQuest1, 
    SideHustle, 
    None
}

        