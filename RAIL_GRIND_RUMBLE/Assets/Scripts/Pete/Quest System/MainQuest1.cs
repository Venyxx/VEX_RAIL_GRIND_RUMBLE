using Unity.VisualScripting;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainQuest1 : CountQuest
{
    private bool destinationReached;
    private GameObject mainQuestParent;
    private TextMeshProUGUI questInfoText { get; set; }
    private bool endDialoguePlayed = false;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        if (ProgressionManager.Get().mainQuest1 == null)
        {
            ProgressionManager.Get().mainQuest1 = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        base.Start();
    }
    
    public override void IncrementCount()
    {
        if (destinationReached)
        {
            base.IncrementCount("Goons Defeated");
            if (isComplete)
            {
                string[] dialogue = {"Great work as always! Now grab the parts and come on back."};
                string[] speakers = {"Diego"};
                DialogueTemplate template = new DialogueTemplate(speakers, dialogue);
                FindObjectOfType<DialogueManager>().StartAutoDialogue(template);
            }
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().name == "Outskirts")
        {
            mainQuestParent = GameObject.Find("MainQuest1 Objects");
            questInfoText = GameObject.Find("QuestInfo").transform.Find("QuestInfoText").gameObject.GetComponent<TextMeshProUGUI>();
            if (isActive)
            {
                foreach (Transform child in mainQuestParent.transform)
                {
                    questInfoText.text = "Chase that van!";
                    child.gameObject.SetActive(true);
                }
            }
        }
    }

    public void VanDestinationReached()
    {
        destinationReached = true;
        questInfoText.text = $"Goons Defeated: {currentCount} / {completionCount}";
    }
}
