using UnityEngine;
using UnityEngine.SceneManagement;

public class LocuoQuestGoal : MonoBehaviour
{
    private LocuoQuestGiver _questGiver;

    private void Start()
    {
        _questGiver = FindObjectOfType<LocuoQuestGiver>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!(other.gameObject.CompareTag("PlayerObject") || other.gameObject.CompareTag("Player"))) return;
        
        if (_questGiver.activated)
        {
            _questGiver.PlayerWin();
            if (SceneManager.GetActiveScene().name == "Servos HQ")
            {
                FindObjectOfType<ThirdPersonMovement>().gameObject.transform.position = new Vector3(667.37f, 31.94f, 1216.16f);
                FindObjectOfType<ThirdPersonMovement>().currentSpeed = 0;
            }
        }
    }
}
