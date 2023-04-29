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
    [SerializeField] private string newQuestInfoText;
    [SerializeField] private bool updatesQuestInfoText;
    [SerializeField] private bool disableWaypoints;
    [SerializeField] private bool killAllDrones;
    [SerializeField] private bool teleportsPlayer;
    [SerializeField] private Transform placeToTeleport;

    private Teleportation teleREF;
    private bool waypointAdvanced;


    private void Start()
    {
        ari = GameObject.Find("playerPrefab");
        if (!lastCheckPointPosition.Equals(new Vector3(0, 0, 0)))
        {
            Debug.Log("Spawning Ari at a Checkpoint Position");
            ari.transform.position = lastCheckPointPosition;
        }
        else
        {
            Debug.Log("NOT spawning Ari at a Checkpoint Position");
        }

        Time.timeScale = 1;
        LastScene = SceneManager.GetActiveScene().buildIndex;

        if (GameObject.Find("TeleportManager"))
            teleREF = GameObject.Find("TeleportManager").GetComponent<Teleportation>();
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable;
        if (other.CompareTag("Player") || other.CompareTag("PlayerObject"))
        {

            foreach (GameObject obj in objectsToActivate)
            {
                obj.SetActive(true);
            }

            if (other.TryGetComponent<IDamageable>(out damageable))
            {
                damageable.GainHealth(100);
            }

            if (updatesPlayerSpawn)
            {
                lastCheckPointPosition = this.gameObject.transform.position;
            }

            if (!performedProgressionAction)
            {
                if (updatesQuestInfoText)
                {
                    ProgressionManager.Get().QuestInfoText.text = newQuestInfoText;
                }
            
                TotalWaypointController totalRef = FindObjectOfType<TotalWaypointController>();
                if (totalRef == null) return;

                if (disableWaypoints)
                {
                    totalRef.gameObject.SetActive(false);
                }

                if (!waypointAdvanced)
                {
                    totalRef.currentIndex++;
                    waypointAdvanced = true;
                }
                
                if (teleportsPlayer)
                {
                    StartCoroutine(DialogueManager.DialogueWipe());
                    GameObject playerPrefab = other.gameObject;
                    playerPrefab.transform.position = placeToTeleport.position;
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

                performedProgressionAction = true;
            }

            

            




        }
    }
    
    

}
