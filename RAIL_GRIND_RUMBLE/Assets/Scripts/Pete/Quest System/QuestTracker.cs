using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestTracker : MonoBehaviour
{
    public Quest CurrentQuest { get; set; }
    public TextMeshProUGUI QuestInfoText { get; private set; }
    private static List<Quest> completedQuests;
    public CountQuestType CurrentCountQuestType { get; private set; } = CountQuestType.None;
    public string questInfo;

    void Start()
    {
        completedQuests = new List<Quest>();
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

        if (CurrentQuest == null && GameObject.Find("WayPointPrefab") && SceneManager.GetActiveScene().name == "Outskirts")
        {
            GameObject waypoints = GameObject.Find("WayPointPrefab");
            waypoints.SetActive(false);
        }

        //QuestInfoText.text = questInfo;
    }


    public void AcceptQuest(Quest quest)
    {
        foreach (var otherQuest in FindObjectsOfType<Quest>())
        {
            otherQuest.isActive = false;
        }
        CurrentQuest = quest;
        CurrentQuest.isActive = true;
        if (quest is MainQuest1)
        {
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
        CurrentQuest.isActive = false;
        CurrentQuest.isComplete = true;
        completedQuests.Add(CurrentQuest);
        CurrentCountQuestType = CountQuestType.None;
        Debug.Log("Quest Completed");
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
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
    }
}
