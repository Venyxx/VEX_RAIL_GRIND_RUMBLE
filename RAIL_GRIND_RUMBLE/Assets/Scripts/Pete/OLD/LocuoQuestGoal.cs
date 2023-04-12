using UnityEngine;

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
        }
    }
}
