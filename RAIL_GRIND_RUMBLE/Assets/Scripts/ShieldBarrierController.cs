using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ShieldBarrierController : MonoBehaviour
{
    private GameObject[] barriers;
    private GameObject[] generators;
    private GameObject wayPointPrefab;
    private TextMeshProUGUI questInfoText;
    private bool endRoutinePerformed;
    [SerializeField] private bool forcedDestroy;
    [SerializeField] private string updatedQuestInfoText;
    [SerializeField] private bool incrementWaypoint;

    private void Start()
    {
        Transform barrierParent = transform.GetChild(1);
        Transform generatorParent = transform.GetChild(0);
        barriers = new GameObject[barrierParent.childCount];
        generators = new GameObject[generatorParent.childCount];
        AddChildrenToArray(barrierParent, barriers);
        AddChildrenToArray(generatorParent, generators);
        wayPointPrefab = GameObject.Find("WayPointPrefabs").transform.Find("MainQuest4 Waypoints").gameObject;
        questInfoText = GameObject.Find("QuestInfo").transform.Find("QuestInfoText").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (AllGeneratorsDestroyed() && !endRoutinePerformed)
        {
            foreach (GameObject barrier in barriers)
            {
                Destroy(barrier);
            }
            wayPointPrefab.SetActive(true);
            if (incrementWaypoint)
            {
                wayPointPrefab.GetComponent<TotalWaypointController>().currentIndex++;
            }
            questInfoText.text = updatedQuestInfoText;
            endRoutinePerformed = true;
        }
    }

    private static void AddChildrenToArray(Transform parent, GameObject[] array)
    {
        int i = 0;
        foreach (Transform child in parent)
        {
            array[i++] = child.gameObject;
        }
    }

    private bool AllGeneratorsDestroyed()
    {
        if (forcedDestroy)
        {
            return true;
        }

        int destroyCount = 0;
        foreach (GameObject generator in generators)
        {
            ShieldGeneratorController shieldGenerator = generator.GetComponent<ShieldGeneratorController>();
            if (shieldGenerator.IsDestroyed())
            {
                destroyCount++;
            }
        }
        return destroyCount >= generators.Length;
    }
}
