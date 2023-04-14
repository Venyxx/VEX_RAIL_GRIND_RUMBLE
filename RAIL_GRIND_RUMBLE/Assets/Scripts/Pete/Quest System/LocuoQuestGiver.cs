﻿using System.Runtime.CompilerServices;
using PathCreation;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocuoQuestGiver : QuestGiver
{
    [SerializeField] private LocuoQuest locuoQuestToGive;
    [SerializeField] private float baseMoveSpeed = 15;
    [SerializeField] private float startDelay = 2f;
    [SerializeField] private Transform raceOverWaitSpot;
    public PathCreator pathCreator { get; set; }
    public EndOfPathInstruction endOfPathInstruction;
    public bool activated = false;
    public float distanceTraveled;
    private float currentMoveSpeed;
    private DialogueTrigger _dialogueTrigger;
    private bool _raceOver;
    private bool _playerWin;
    [SerializeField] private GameObject skateParkEnemies;

    protected override void Start()
    {
        base.Start();
        locuoQuestToGive.questGiver = this;
        questToGive = locuoQuestToGive;
        Transform parent = transform.root;
        _dialogueTrigger = GetComponent<DialogueTrigger>();
        if (parent.Find("Road Creator"))
        {
            pathCreator = parent.Find("Road Creator").GetComponent<PathCreator>();
        }
        else
        {
            Debug.LogError("Could not find Road Creator in Parent");
        }

        currentMoveSpeed = baseMoveSpeed;
    }

    protected override void Update()
    {
        base.Update();

        if (activated)
        {
            if (!_raceOver)
            {
                if (pathCreator != null)
                {
                    distanceTraveled += currentMoveSpeed * Time.deltaTime;
                    transform.position = pathCreator.path.GetPointAtDistance(distanceTraveled, endOfPathInstruction);
                    transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTraveled, endOfPathInstruction);
                    _dialogueTrigger.enabled = false;
                }
                currentMoveSpeed = Mathf.Lerp(currentMoveSpeed, baseMoveSpeed, .02f);
            }
            else
            {
                activated = false;
            }
        }
        else if (_raceOver)
        {
            if (!_playerWin)
            {
                SceneManager.LoadScene("LoseScene");
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, raceOverWaitSpot.position, baseMoveSpeed * Time.deltaTime);
                if (!locuoQuestToGive.RewardsGiven)
                {
                    _dialogueTrigger.enabled = true;
                }
            }

 
        }

    }

    public void Activate()
    {
        ProgressionManager.Get().grappleUnlocked = true;
        GameObject mainWaypoints = GameObject.Find("Main Waypoints");
        if (mainWaypoints != null)
        {
            mainWaypoints.GetComponent<TotalWaypointController>().currentIndex = 1;
        }
        
        _dialogueTrigger.enabled = false;
        Invoke(nameof(SetActivated), startDelay);
    }

    private void SetActivated()
    {
        activated = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LocuoGoal"))
        {
            _raceOver = true;
        }
    }

    public void PlayerWin()
    {
        _playerWin = true;
        _raceOver = true;
        ProgressionManager.Get().CompleteQuest();
        StartCoroutine(DialogueManager.DialogueWipe());
        transform.position = raceOverWaitSpot.position;
        FindObjectOfType<TotalWaypointController>().currentIndex = 7;
        FindObjectOfType<ThirdPersonMovement>().currentSpeed = 0;
    }

    public void RewardPlayer()
    {
        GameObject.Find("Main Waypoints").GetComponent<TotalWaypointController>().currentIndex = 8;
        StartCoroutine(DialogueManager.DialogueWipe());
        skateParkEnemies.SetActive(true);
        StartCoroutine(DialogueManager.DialogueWipe());
        GetComponent<DialogueTrigger>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        transform.Find("Locuo Model").gameObject.SetActive(false);
    }

    public LocuoQuest GetLocuoQuest()
    {
        return locuoQuestToGive;
    }
}
