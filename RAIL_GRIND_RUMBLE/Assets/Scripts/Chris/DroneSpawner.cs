using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneSpawner : MonoBehaviour
{
    public static int droneCount = 0;
    [SerializeField] GameObject drone;
    bool spawnRunning;
    // Start is called before the first frame update
    void Start()
    {
        spawnRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (droneCount < 3 && spawnRunning == false)
        {
            StartCoroutine(SpawnDrone());
        }
    }

    IEnumerator SpawnDrone()
    {
        spawnRunning = true;
        GameObject spawnedDrone = Instantiate(drone, transform.position, Quaternion.identity);
        droneCount++;
        Debug.Log("Drone Count: " + droneCount);
        yield return new WaitForSeconds(30f);
        spawnRunning = false;
    }
}
