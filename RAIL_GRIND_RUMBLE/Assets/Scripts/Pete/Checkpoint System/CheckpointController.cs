using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointController : MonoBehaviour
{
    public static int LastScene { get; private set; }
    public static Vector3 lastCheckPointPosition;
    private GameObject ari;
    private bool performedProgressionAction;
    [SerializeField] private bool updatesPlayerSpawn = true;
    [SerializeField] private int cutsceneToPlay;
    [SerializeField] private GameObject[] objectsToActivate;
    [SerializeField] private GameObject[] objectsToDeactivate;
    [SerializeField] private string newQuestInfoText;
    [SerializeField] private bool updatesQuestInfoText;
    [SerializeField] private bool disableWaypoints;
    [SerializeField] private bool enableWaypoints;
    [SerializeField] private bool killAllDrones;
    [SerializeField] private bool teleportsPlayer;
    [SerializeField] private Transform placeToTeleport;
    [SerializeField] private bool setsWayPointIndex;
    [SerializeField] private int nextWayPointIndex;
    [SerializeField] private bool incrementsWaypoint;
    TotalWaypointController totalRef; 



    private void Awake()
    {
        ari = GameObject.Find("playerPrefab");
        totalRef = FindObjectOfType<TotalWaypointController>();
        if (SceneManager.GetActiveScene().name == "InnerRingLevel")
        {
            totalRef = GameObject.Find("WayPointPrefabs").transform.Find("MainQuest4 Waypoints")
                .GetComponent<TotalWaypointController>();
        }

        if (!lastCheckPointPosition.Equals(new Vector3(0, 0, 0)))
        {
            Debug.Log("Spawning Ari at a Checkpoint Position");
            ari.transform.position = lastCheckPointPosition;
        }
        else
        {
            Debug.Log("NOT spawning Ari at a Checkpoint Position");
        }

        //Time.timeScale = 1;
        LastScene = SceneManager.GetActiveScene().buildIndex;
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable;
        if (other.CompareTag("Player") || other.CompareTag("PlayerObject"))
        {
            
            if (updatesPlayerSpawn)
            {
                lastCheckPointPosition = this.gameObject.transform.position;
            }

            if (!performedProgressionAction)
            {
                performedProgressionAction = true;
                if (objectsToActivate != null && objectsToActivate.Length > 0)
                {
                    foreach (GameObject obj in objectsToActivate)
                    {
                        if(obj != null)
                            obj.SetActive(true);
                    }
                }
            
                if (objectsToDeactivate != null && objectsToDeactivate.Length > 0)
                {
                    foreach (GameObject obj in objectsToDeactivate)
                    {
                        if(obj != null)
                            obj.SetActive(false);
                    }
                }
                if (other.TryGetComponent<IDamageable>(out damageable))
                {
                    damageable.GainHealth(100);
                }
                if (updatesQuestInfoText)
                {
                    ProgressionManager.Get().QuestInfoText.text = newQuestInfoText;
                }
            
                if (totalRef == null) return;

                if (disableWaypoints)
                {
                    totalRef.gameObject.SetActive(false);
                }

                if (enableWaypoints)
                {
                    totalRef.gameObject.SetActive(true);
                }
                
                if (setsWayPointIndex)
                {
                    totalRef.currentIndex = nextWayPointIndex;
                }
                else if(incrementsWaypoint)
                {
                    totalRef.currentIndex++;
                }

                if (teleportsPlayer)
                {
                    StartCoroutine(DialogueManager.DialogueWipe());
                    GameObject playerPrefab = other.gameObject;
                    playerPrefab.transform.position = placeToTeleport.position;
                    playerPrefab.GetComponent<ThirdPersonMovement>().currentSpeed = 0;
                }
                
                if (killAllDrones)
                {
                    var drones = FindObjectsOfType<Drone>();
                    foreach (var drone in drones)
                    {
                        Destroy(drone.gameObject);
                    }
                }
                
                if (cutsceneToPlay != 0)
                {
                    ProgressionManager.Get().PlayCutscene(cutsceneToPlay);
                }
                 //MOVE ME TO TOP
            }
        }
    }
    
    

}
