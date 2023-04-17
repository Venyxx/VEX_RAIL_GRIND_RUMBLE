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

    private Teleportation teleREF;
    private bool waypointAdvanced;


    private void Start()
    {
        ari = GameObject.Find("playerPrefab");
        if (!lastCheckPointPosition.Equals(new Vector3(0,0,0)))
            ari.transform.position = lastCheckPointPosition;
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
            
            if (cutsceneToPlay != 0)
            {
                ProgressionManager.Get().PlayCutscene(cutsceneToPlay);
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

                performedProgressionAction = true;
            }

            if (killAllDrones)
            {
                var drones = FindObjectsOfType<Drone>();
                foreach (var drone in drones)
                {
                    Destroy(drone.gameObject);
                }
            }




        }
    }
    
    

}
