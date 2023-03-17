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
    public MainQuest2 mainQuest2;
    
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
        else if (quest is MainQuest2)
        {
            mainQuest2 = (MainQuest2) quest;
            CurrentCountQuestType = CountQuestType.Graffiti;
            QuestInfoText.text = $"Leave your house and take the bus to the Inner Ring!";
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
        Debug.Log(CompletedQuests);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LoadObjects();
        if (!IsFinished(mainQuest1))
        {
            //SetBusStopsActive(false);
            //LoadMainQuest1();
        }
        if (!IsFinished(mainQuest2))
        {
            LoadMainQuest2();
        }
        HandleDiegoAutoDialogue();
    }

    public void SetBusStopsActive(bool active)
    {
        GameObject[] busStops = GameObject.FindGameObjectsWithTag("BusStop");
        foreach (var busStop in busStops)
        {
            busStop.SetActive(active);
        }
    }

    public void SetFirstAutoDialogueUsed()
    {
        firstAutoDialogueUsed = true;
    }

    private void HandleDiegoAutoDialogue()
    {
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

    private void LoadMainQuest1()
    {
        if (mainQuest1 != null && SceneManager.GetActiveScene().name == "Outskirts" && !mainQuest1.isComplete && !IsFinished(mainQuest1))
        {
            GameObject totalWayPointParent = GameObject.Find("WayPointPrefabs");
            GameObject mainQuest1WayPoints = totalWayPointParent.transform.GetChild(0).gameObject;
            mainQuest1WayPoints.SetActive(true);
            TotalWaypointController totalREF = mainQuest1WayPoints.GetComponent<TotalWaypointController>();
            GameObject mainQuestParent = GameObject.Find("MainQuest1 Objects");
            mainQuest1.LoadMainQuest1(totalREF, mainQuestParent, QuestInfoText);
        }
    }

    private void LoadMainQuest2()
    {
        if (mainQuest2 != null && !mainQuest2.isComplete && !IsFinished(mainQuest2))
        {
            if (SceneManager.GetActiveScene().name == "Outskirts")
            {
                GameObject totalWayPointParent = GameObject.Find("WayPointPrefabs");
                GameObject mainQuest2WayPoints = totalWayPointParent.transform.GetChild(1).gameObject;
                mainQuest2WayPoints.SetActive(true);
                TotalWaypointController totalREF = mainQuest2WayPoints.GetComponent<TotalWaypointController>();
                foreach (var waypoint in totalREF.waypoints)
                {
                    Debug.Log(waypoint.name);
                }
                //GameObject mainQuestParent = GameObject.Find("MainQuest1 Objects");
                mainQuest2.LoadMainQuest2Outskirts(totalREF, QuestInfoText);
            }
            else if (SceneManager.GetActiveScene().name == "InnerRingLevel")
            {
                mainQuest2.LoadMainQuest2InnerRing(null, QuestInfoText);
            }
        }
        
    }

    private void LoadObjects()
    {
        try { QuestInfoText = GameObject.Find("QuestInfo").transform.Find("QuestInfoText").gameObject.GetComponent<TextMeshProUGUI>(); }
        catch (Exception e) { Debug.LogError("Failed to assign QuestInfoText"); }
        try { Player = FindObjectOfType<ThirdPersonMovement>(); }
        catch (Exception e) { Debug.LogError("Failed to assign ThirdPersonMovement"); }
        try { DialogueManager = FindObjectOfType<DialogueManager>(); }
        catch (Exception e) { Debug.LogError("Failed to assign DialogueManager"); }
    }

    public static bool IsFinished(Quest quest)
    {
        return CompletedQuests.Contains(quest);
    }
}
