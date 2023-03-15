using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressionManager : MonoBehaviour
{

    private static ProgressionManager instance;
    public int coinCount;
    public MainQuest1 mainQuest1;


    public Quest currentQuest;
    
    public TextMeshProUGUI QuestInfoText { get; private set; }
    public static List<Quest> CompletedQuests { get; private set; }
    public CountQuestType CurrentCountQuestType { get; private set; } = CountQuestType.None;
    public ThirdPersonMovement Player { get; private set; }
    public DialogueManager DialogueManager { get; private set; }
    public string questInfo;
    private bool firstAutoDialogueUsed;

    public static ProgressionManager Get()
    {
        if (instance == null)
        {
            var gameObject = new GameObject("Progression Manager");
            gameObject.AddComponent<ProgressionManager>();
        }

        return instance;
    }
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    
    void Start()
    {
        CompletedQuests = new List<Quest>();
        try
        {
            QuestInfoText = GameObject.Find("QuestInfo").transform.Find("QuestInfoText").gameObject
                .GetComponent<TextMeshProUGUI>();
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to assign QuestInfoText");
        }
        
    }

    private void Update()
    {
        if (PauseMenu.isPaused || InfoScreen.isOpen)
        {
            QuestInfoText.gameObject.SetActive(false);
        }
        else
        {
            QuestInfoText.gameObject.SetActive(true);
        }

        if (currentQuest == null && GameObject.Find("WayPointPrefab") && SceneManager.GetActiveScene().name == "Outskirts")
        {
            GameObject waypoints = GameObject.Find("WayPointPrefab");
            waypoints.SetActive(false);
        }
        
        

        //QuestInfoText.text = questInfo;
    }
    
    

    
    public void AcceptQuest(Quest quest)
    {
        foreach (var otherQuestGiver in FindObjectsOfType<QuestGiver>())
        {
            Quest otherQuest = otherQuestGiver.GetQuest();
            otherQuest.isActive = false;
        }
        currentQuest = quest;
        currentQuest.isActive = true;
        if (quest is MainQuest1)
        {
            mainQuest1 = (MainQuest1) quest;
            CurrentCountQuestType = CountQuestType.Enemies;
            QuestInfoText.text = $"Leave your house and chase that van!";
        }
        else if (quest is CountQuest countQuest)
        {
            CurrentCountQuestType = countQuest.GetCountQuestType();
            QuestInfoText.text = $"Progress: {0} / {countQuest.GetCompletionCount()}";
        }
    }
    

    public void CompleteQuest()
    {
        currentQuest.isActive = false;
        currentQuest.isComplete = true;
        CompletedQuests.Add(currentQuest);
        CurrentCountQuestType = CountQuestType.None;
        Debug.Log("Quest Completed");
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        try { QuestInfoText = GameObject.Find("QuestInfo").transform.Find("QuestInfoText").gameObject.GetComponent<TextMeshProUGUI>(); }
        catch (Exception e) { Debug.LogError("Failed to assign QuestInfoText"); }
        try { Player = FindObjectOfType<ThirdPersonMovement>(); }
        catch (Exception e) { Debug.LogError("Failed to assign ThirdPersonMovement"); }
        try { DialogueManager = FindObjectOfType<DialogueManager>(); }
        catch (Exception e) { Debug.LogError("Failed to assign DialogueManager"); }

        if (mainQuest1 != null && SceneManager.GetActiveScene().name == "Outskirts")
        {
            TotalWaypointController totalREF = GameObject.Find("WayPointPrefab").GetComponent<TotalWaypointController>();
            GameObject mainQuestParent = GameObject.Find("MainQuest1 Objects");
            mainQuest1.LoadMainQuest1(totalREF, mainQuestParent, QuestInfoText);
        }

        if (SceneManager.GetActiveScene().name == "Ari's House")
        {
            GameObject diego = GameObject.Find("Diego");
            AutomaticDialogueTrigger autoTrigger = diego.transform.Find("OpeningAutoDialogue").gameObject.GetComponent<AutomaticDialogueTrigger>();
            if (firstAutoDialogueUsed)
            {
                autoTrigger.gameObject.SetActive(false);
            }
        }
    }

    public void SetFirstAutoDialogueUsed()
    {
        firstAutoDialogueUsed = true;
    }
}
