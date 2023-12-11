using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FinalBossGroundSpawner : MonoBehaviour
{
    [SerializeField] private GameObject flatGround;
    [SerializeField] private GameObject wall;
    [SerializeField] private Vector3 startSpawnPositionFloor = new Vector3(857.81f, -18.95f, -236.26f);
    [SerializeField] private Vector3 startSpawnPositionRightWall = new Vector3(861.83f, -11.99f, -236.43f); 
    [SerializeField] private Vector3 startSpawnPositionLeftWall = new Vector3(845f, -11.99f, -236.43f);
    [SerializeField] private float spawnDelaySeconds;
    private Queue<GameObject> spawnedObjects;
    private float zLengthFloor;
    private float zLengthWall;

    private GameObject player;
    private GrappleHook playerGrapple;
    private bool paused = true;
    
    // Start is called before the first frame update
    void Start()
    {
        spawnedObjects = new Queue<GameObject>();
        zLengthFloor = flatGround.GetComponent<Renderer>().bounds.size.z;
        zLengthWall = wall.GetComponent<Renderer>().bounds.size.z;
        player = FindObjectOfType<ThirdPersonMovement>().gameObject;
        playerGrapple = player.GetComponent<GrappleHook>();
        
        SpawnFloor(0);
        
    }

    // Update is called once per frame
    void Update()
    {
        paused = playerGrapple.isGrappling;
    }

    public void Activate()
    {
        StartCoroutine(SpawnGround());
    }

    IEnumerator SpawnGround()
    {
        int i = 1;
        while (true)
        {

            

            if (i % 7 != 0 || i == 0)
            {
                SpawnFloor(i);
            }
            else
            {
                bool isLeft = Random.Range(0, 2) == 0;
                Debug.Log($"isLeft: {isLeft}");
                SpawnWalls(i, isLeft);
                i++;

            }
            i++;

            if (!paused)
            {
                if (spawnedObjects.Count > 20)
                {
                    Destroy(spawnedObjects.Dequeue());
                }
            }
            yield return new WaitForSeconds(spawnDelaySeconds);
            
        }
    }

    private void SpawnFloor(int i)
    {
        GameObject spawned = Instantiate(flatGround, startSpawnPositionFloor + new Vector3(0,0,zLengthFloor*i), Quaternion.identity);
        spawnedObjects.Enqueue(spawned);
    }

    private void SpawnWalls(int i, bool isLeft)
    {
        Vector3 startSpawnPosition; 
        if (isLeft)
        {
            startSpawnPosition = startSpawnPositionLeftWall;
        }
        else
        {
            startSpawnPosition = startSpawnPositionRightWall;
        }

        GameObject spawned = Instantiate(wall, startSpawnPosition + new Vector3(0, 0, (zLengthWall * i)/2.0f + zLengthFloor), Quaternion.identity);
        spawned.transform.Rotate(0,0,90);
        spawnedObjects.Enqueue(spawned);
    }
}

