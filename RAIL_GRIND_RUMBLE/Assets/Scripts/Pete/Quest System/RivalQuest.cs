
using UnityEngine;

public class RivalQuest : Quest
{
    [SerializeField] private Transform goalPoint;
    [SerializeField] private float speed;
    private bool raceStarted = false;
    private bool raceOver = false;
    private bool playerWon = false;
    private GameObject parent;
    private GameObject player;

    private void Start()
    {
        parent = transform.parent.gameObject;
        player = FindObjectOfType<ThirdPersonMovement>().gameObject;
        RivalQuestGoal goal = goalPoint.gameObject.GetComponent<RivalQuestGoal>();
        goal.SetMyQuest(this);
    }

    public void Activate()
    {
        CheckpointController.lastCheckPointPosition = player.transform.position;
        raceStarted = true;
    }

    public void PlayerWin()
    {
        playerWon = true;
        FindObjectOfType<QuestTracker>().CompleteQuest();
    }

    private void Update()
    {
        if (raceStarted && !raceOver)
        {
            parent.transform.position = Vector3.MoveTowards(parent.transform.position, goalPoint.position, speed * Time.deltaTime);
            if (Vector3.Distance(player.transform.position, goalPoint.position) < 0.1)
            {
                playerWon = true;
            }

            if (Vector3.Distance(parent.transform.position, goalPoint.position) < 0.1)
            {
                raceOver = true;
                if (playerWon)
                {
                    Debug.Log("The race is over, and the player won!");
                }
                else
                {
                    QuestGiver questGiver = GetComponent<QuestGiver>();
                    questGiver.FailQuest();
                }
            }
        }

    }
}
