using PathCreation;
using UnityEngine;

public class LocuoQuestGiver : QuestGiver
{
    [SerializeField] private LocuoQuest locuoQuestToGive;
    [SerializeField] private float baseMoveSpeed = 15;
    [SerializeField] private float startDelay = 2f;
    public PathCreator pathCreator;
    public EndOfPathInstruction endOfPathInstruction;
    public bool activated = false;
    public float distanceTraveled;
    private float currentMoveSpeed;

    protected override void Start()
    {
        base.Start();
        locuoQuestToGive.questGiver = this;
        questToGive = locuoQuestToGive;
        Transform parent = transform.root;
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

        if (!activated) return;
        
        if (pathCreator != null)
        {
            distanceTraveled += currentMoveSpeed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distanceTraveled, endOfPathInstruction);
            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTraveled, endOfPathInstruction);
        }

        currentMoveSpeed = Mathf.Lerp(currentMoveSpeed, baseMoveSpeed, .02f);
    }

    public void Activate()
    {
        ProgressionManager.Get().grappleUnlocked = true;
        GameObject.Find("Main Waypoints").GetComponent<TotalWaypointController>().currentIndex++;
        Invoke(nameof(SetActivated), startDelay);
    }

    private void SetActivated()
    {
       
        activated = true;
    }
}
