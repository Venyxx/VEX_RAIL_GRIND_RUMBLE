using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneSpawner : MonoBehaviour
{
    public static int droneCount = 0;
    [SerializeField] GameObject drone;
    [SerializeField] bool limit;
    [SerializeField] int limitCount;
    bool spawnRunning;
    bool inRange;
    // Start is called before the first frame update
    void Start()
    {
        spawnRunning = false;
        droneCount = 0;
        inRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (droneCount < 3 && spawnRunning == false && inRange == true)
        {
            if (limit == true && limitCount > 0)
            {
                StartCoroutine(SpawnDrone());
            } else if (limit == false){
                StartCoroutine(SpawnDrone());
            }
            
        }
    }

    IEnumerator SpawnDrone()
    {
        spawnRunning = true;
        GameObject spawnedDrone = Instantiate(drone, transform.position, Quaternion.identity);
        droneCount++;
        if (limit == true)
        {
            limitCount--;
            Debug.Log("Drones left: "+limitCount);
        }
        //Debug.Log("Drone Count: " + droneCount);
        yield return new WaitForSeconds(30f);
        spawnRunning = false;
    }

    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Drone Trigger Enter");
        if (collision.gameObject.tag == "Player")
        {
            inRange = true;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        Debug.Log("Drone Trigger Exit");
        if (collision.gameObject.tag == "Player")
        {
            inRange = false;
        }
    }
}
