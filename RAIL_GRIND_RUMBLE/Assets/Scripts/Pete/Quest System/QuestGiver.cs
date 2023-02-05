using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    [SerializeField] private Quest questToGive;
    //[SerializeField] private string acceptedDialogue;
    //[SerializeField] private string denyDialogue; 
    
    private PauseMenu pauseMenu;
    
    private TextMeshProUGUI questTitleText;
    private TextMeshProUGUI questDescrText;
    private TextMeshProUGUI questRewardText;


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
    }

    public void DenyQuest()
    {
        pauseMenu.ResumeGame();
    }
}
