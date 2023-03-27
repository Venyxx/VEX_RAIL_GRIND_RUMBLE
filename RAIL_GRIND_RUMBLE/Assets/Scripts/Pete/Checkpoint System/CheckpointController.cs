using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointController : MonoBehaviour
{
    public static int LastScene { get; private set; }
    public static Vector3 lastCheckPointPosition;
    private GameObject ari;
    [SerializeField] private int cutsceneToPlay;

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
        if (other.CompareTag("Player") || other.CompareTag("PlayerObject"))
        {
            if (cutsceneToPlay != 0)
            {
                ProgressionManager.Get().PlayCutscene(cutsceneToPlay);
            }
            
            lastCheckPointPosition = this.gameObject.transform.position;
            if (!waypointAdvanced)
            {
                TotalWaypointController totalRef = FindObjectOfType<TotalWaypointController>();
                if (totalRef == null) return;
                totalRef.currentIndex++;
                waypointAdvanced = true;
            }

            
        }
    }
    
    

}
