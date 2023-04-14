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
    public MainQuest3 mainQuest3;
    public LocuoQuest locuoRace1;
    public LocuoQuest locuoRace2;
    public LocuoQuest locuoRace3;
    
    public Quest currentQuest;
    
    public TextMeshProUGUI QuestInfoText { get; private set; }
    public static List<Quest> CompletedQuests { get; private set; }
    public CountQuestType CurrentCountQuestType { get; private set; } = CountQuestType.None;
    public ThirdPersonMovement Player { get; private set; }
    public DialogueManager DialogueManager { get; private set; }
    public string questInfo;
    private bool firstAutoDialogueUsed;
    public bool firstLoad = true;

    public bool grappleUnlocked;
    public bool prologueComplete;
    private bool firstRaceCompleted;

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
            CompletedQuests = new List<Quest>();
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    
    void Start()
    {
        try
        {
            QuestInfoText = GameObject.Find("QuestInfo").transform.Find("QuestInfoText").gameObject
                .GetComponent<TextMeshProUGUI>();
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to assign QuestInfoText");
        }
        
        PlayFirstCutscene();

        if (firstLoad && (SceneManager.GetActiveScene().name == "Inner Ring Level" ||
            SceneManager.GetActiveScene().name == "Servos HQ"))
        {
            grappleUnlocked = true;
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
        else if (quest is MainQuest3)
        {
            mainQuest3 = (MainQuest3) quest;
            QuestInfoText.text = $"Leave your house and meet Diego at the Orphanage!";
            
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

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LoadObjects();
        string sceneName = SceneManager.GetActiveScene().name;
        
        if (!IsFinished(mainQuest1))
        {
            //Debug.Log("MAINQUEST1 IS NOT FINISHED");
            SetBusStopsActive(false);
            if (mainQuest1 != null && currentQuest == mainQuest1)
            {
                LoadMainQuest1();
            }
        }
        else
        {
            //Debug.Log("MAINQUEST1 IS FINISHED");
            SetBusStopsActive(true);
        }

        if (mainQuest2 != null && !IsFinished(mainQuest2) && currentQuest == mainQuest2)
        {
            LoadMainQuest2();
        }

        if (mainQuest3 != null && !IsFinished(mainQuest3) && currentQuest == mainQuest3)
        {
            LoadMainQuest3();
        }

        if (mainQuest3 != null && IsFinished(mainQuest3) && !mainQuest3.RewardsGiven && sceneName ==  "Ari's House")
        {
            PlayCutscene(4);
            QuestInfoText.text = "Ask for intel about Diego";
            prologueComplete = true;
            mainQuest3.RewardPlayer();
        }

        if (sceneName == "Outskirts" && prologueComplete)
        {
            Transform locuo = GameObject.Find("Locuo").transform;
            foreach (Transform obj in locuo)
            {
                obj.gameObject.SetActive(true);
            }
            GameObject.Find("WayPointPrefabs").transform.Find("Main Waypoints").gameObject.SetActive(true);
            LocuoQuest race = FindObjectOfType<LocuoQuestGiver>().GetLocuoQuest();
            if (race != null)
            {
                locuoRace1 = race;
            }
        }

        
        /*if (currentQuest != mainQuest3 && SceneManager.GetActiveScene().name == "Outskirts")
        {
            GameObject.Find("OpenGateQuest3").SetActive(true);
        }*/

        if (SceneManager.GetActiveScene().name == "Servos HQ")
        {
            Debug.Log("LOADED SERVOS HQ");
            PlayCutscene(6);
        }
        else
        {
            Debug.Log("DID NOT LOAD SERVOS HQ");
        }
        
        /*if (currentQuest == null && GameObject.Find("WayPointPrefabs") && SceneManager.GetActiveScene().name == "Outskirts")
        {
            GameObject waypoints = GameObject.Find("WayPointPrefabs");
            waypoints.SetActive(false);
        }*/

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
                GameObject totalWayPointParent = GameObject.Find("WayPointPrefabs");
                GameObject mainQuest2WayPoints = totalWayPointParent.transform.GetChild(0).gameObject;
                mainQuest2WayPoints.SetActive(true);
                TotalWaypointController totalREF = mainQuest2WayPoints.GetComponent<TotalWaypointController>();
                GameObject mainQuest2Parent = GameObject.Find("MainQuest2 Objects");
                mainQuest2.LoadMainQuest2InnerRing(totalREF, QuestInfoText, mainQuest2Parent);
            }
        }
    }

    private void LoadMainQuest3()
    {
        if (mainQuest3 != null && SceneManager.GetActiveScene().name == "Outskirts" && !mainQuest3.isComplete && !IsFinished(mainQuest3))
        {
            GameObject totalWayPointParent = GameObject.Find("WayPointPrefabs");
            GameObject mainQuest3WayPoints = totalWayPointParent.transform.GetChild(2).gameObject;
            mainQuest3WayPoints.SetActive(true);
            TotalWaypointController totalREF = mainQuest3WayPoints.GetComponent<TotalWaypointController>();
            GameObject mainQuestParent = GameObject.Find("MainQuest3 Objects");
            mainQuest3.LoadMainQuest3(totalREF, mainQuestParent, QuestInfoText);
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

    public void SetQuestInfoText(string text)
    {
        QuestInfoText.text = text;
    }

    public void PlayFirstCutscene()
    {
        if (SceneManager.GetActiveScene().name == "Ari's House" && !firstAutoDialogueUsed)
        {
           
            PlayCutscene(1);
        }
    }

    public void PlayCutscene(int clip)
    {
        FindObjectOfType<CutscenePlayer>().PlayCutscene(clip);
    }
}
