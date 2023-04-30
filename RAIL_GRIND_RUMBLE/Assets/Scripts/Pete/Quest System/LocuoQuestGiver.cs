using System.Runtime.CompilerServices;
using PathCreation;
using UnityEngine;
using UnityEngine.InputSystem.DualShock;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LocuoQuestGiver : QuestGiver
{
    [SerializeField] private LocuoQuest locuoQuestToGive;
    [SerializeField] private float baseMoveSpeed = 15;
    [SerializeField] private float startDelay = 2f;
    [SerializeField] private Transform raceOverWaitSpot;
    [SerializeField] private bool inInnerRing;
    
    public PathCreator pathCreator { get; set; }
    public EndOfPathInstruction endOfPathInstruction;
    public bool activated = false;
    public float distanceTraveled;
    private float currentMoveSpeed;
    private DialogueTrigger _dialogueTrigger;
    private bool _raceOver;
    private bool _playerWin;
    private bool _teleported = false;
    [SerializeField] private GameObject skateParkEnemies;

    private GameObject lobbyExit1;
    private GameObject lobbyExit2;

    //music switch
    [SerializeField] private GameObject dynamicMusic;
    [SerializeField] private GameObject RaceMusic;

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

        if (SceneManager.GetActiveScene().name == "Servos HQ")
        {
            lobbyExit1 = GameObject.Find("LobbyExit1");
            lobbyExit2 = GameObject.Find("LobbyExit2");
            lobbyExit1.SetActive(false);
            lobbyExit2.SetActive(false);
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
                    Quaternion pathCreatorRotation = pathCreator.path.GetRotationAtDistance(distanceTraveled, endOfPathInstruction);
                    float rotationY = pathCreatorRotation.y;
                    transform.rotation = new Quaternion(transform.rotation.x, rotationY, transform.rotation.z,
                        transform.rotation.w);
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
        if (mainWaypoints != null && SceneManager.GetActiveScene().name == "Outskirts")
        {
            mainWaypoints.GetComponent<TotalWaypointController>().currentIndex = 1;
            ProgressionManager.Get().QuestInfoText.text = "Race Locuo to the find Diego!";
        }

        if (SceneManager.GetActiveScene().name == "InnerRingLevel")
        {
            FindObjectOfType<TotalWaypointController>().currentIndex = 2;
            ProgressionManager.Get().QuestInfoText.text = "Race Locuo to the last shield generator!";
            GameObject killboxes = GameObject.Find("Killboxes");
            foreach (Transform child in killboxes.transform)
            {
                child.gameObject.SetActive(true);
            }
        }

        if (SceneManager.GetActiveScene().name == "Servos HQ" && !_teleported)
        {
            StartCoroutine(DialogueManager.DialogueWipe());
            FindObjectOfType<ThirdPersonMovement>().gameObject.transform.position =
                new Vector3(1889.93994f, 85.4100037f, 3483.19995f);
            FindObjectOfType<ThirdPersonMovement>().currentSpeed = 0;
            _teleported = true;
        }

        _dialogueTrigger.enabled = false;
        Invoke(nameof(SetActivated), startDelay);
    }

    private void SetActivated()
    {
        activated = true;
        RaceMusic.SetActive(true);
        dynamicMusic.SetActive(false);
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
        FindObjectOfType<ThirdPersonMovement>().currentSpeed = 0;
        if (SceneManager.GetActiveScene().name != "Servos HQ")
        {
            FindObjectOfType<TotalWaypointController>().currentIndex = 7;
        }
        if (SceneManager.GetActiveScene().name == "InnerRingLevel")
        {
            ProgressionManager.Get().QuestInfoText.text = "Destroy the last generator!";
            GameObject killboxes = GameObject.Find("Killboxes");
            
            foreach (Transform child in killboxes.transform)
            {
                child.gameObject.SetActive(false);
            }
        }

        RaceMusic.SetActive(false);
        dynamicMusic.SetActive(true);

        
    }

    public void RewardPlayer()
    {
        if (SceneManager.GetActiveScene().name == "Outskirts")
        {
            GameObject.Find("Main Waypoints").GetComponent<TotalWaypointController>().currentIndex = 8;
            StartCoroutine(DialogueManager.DialogueWipe());
            skateParkEnemies.SetActive(true);
            StartCoroutine(DialogueManager.DialogueWipe());
            GetComponent<DialogueTrigger>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
            transform.Find("Locuo Model").gameObject.SetActive(false);
        }
        
        if (SceneManager.GetActiveScene().name == "Servos HQ")
        {
            lobbyExit1.SetActive(true);
            lobbyExit2.SetActive(true);
        }
        
    }

    public LocuoQuest GetLocuoQuest()
    {
        return locuoQuestToGive;
    }
}
