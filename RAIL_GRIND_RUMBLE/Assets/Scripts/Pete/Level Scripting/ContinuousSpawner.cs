using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class ContinuousSpawner : MonoBehaviour
{
    [SerializeField] [Tooltip("If not quantity based, then time based")] 
    private bool quantityBased;
    
    [SerializeField] [Tooltip("Only matters if quantity based")] 
    private int spawnQuantity;
    
    [SerializeField] [Tooltip("The amount of time between spawns, after initial spawning is done")] 
    private float timeBetweenSpawns;

    [SerializeField]
    [Tooltip("How long the encounter should last; only matters if time based (quantity based unchecked)")]
    private float encounterDuration;

    [FormerlySerializedAs("spawnPoints")] 
    [SerializeField] private Transform[] initialSpawnPoints;

    [SerializeField] private Transform[] subsequentSpawnPoints;

    [SerializeField] private GameObject goonPrefab;

    //[Tooltip("You can leave this blank and no dialogue will play")]
    //[SerializeField] private DialogueTemplate endingDialogue;

    private int quantitySpawned;
    private float timer;
    private bool progressHandled = false;

    private void Start()
    {
        Debug.Log("SPAWNER ACTIVATE");
        foreach (var spawnPoint in initialSpawnPoints)
        {
            Instantiate(goonPrefab, spawnPoint.position, Quaternion.identity);
            if (quantityBased)
            {
                quantitySpawned++;
            }
        }

        StartCoroutine(SpawnEnemies());
    }

    private void Update()
    {
        if (!quantityBased && timer < encounterDuration)
        {
            timer += 1 * Time.deltaTime;
        }

        if (FinishedSpawning() && !progressHandled)
        {
            progressHandled = true;
            HandleProgress();
        }
    }

    private IEnumerator SpawnEnemies()
    {
        if (quantityBased)
        {
            while (quantitySpawned < spawnQuantity)
            {
                SpawnHelper();
                yield return new WaitForSeconds(timeBetweenSpawns);
            }
        }
        else
        {
            while (timer < encounterDuration)
            {
                SpawnHelper();
                yield return new WaitForSeconds(timeBetweenSpawns);
            }
        }
    }

    private void SpawnHelper()
    {
        foreach (var spawnPoint in subsequentSpawnPoints)
        {
            Instantiate(goonPrefab, spawnPoint.position, Quaternion.identity);
            if (quantityBased)
            {
                quantitySpawned++;
            }
        }
    }

    private bool FinishedSpawning()
    {
        return (!quantityBased && timer >= encounterDuration) || (quantityBased && quantitySpawned >= spawnQuantity);
    }

    protected virtual void HandleProgress() {}
}
