using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneSpawner : MonoBehaviour
{
    public static int droneCount = 0;
    [SerializeField] GameObject drone;
    [SerializeField] bool limit;
    [SerializeField] int limitCount;
    [SerializeField] bool bossSpawner;
    //[SerializeField] float respawnTime;
    [SerializeField] Hernandez hernandez;
    bool spawnRunning;
    bool inRange;
    // Start is called before the first frame update
    void Start()
    {
        spawnRunning = false;
        droneCount = 0;
        
        if (bossSpawner == true)
        {
            inRange = true;
            //hernandez = GameObject.FindWithTag("Hernandez").GetComponent<Hernandez>();
        } else {
            inRange = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ((droneCount < 3))
        {
            if(spawnRunning == false && inRange == true)
            {
                if (hernandez) // added for testing -v
                {
                    if (bossSpawner == true && hernandez.stunned == true) return;

                    if (bossSpawner == true && !GameObject.FindWithTag("Hernandez")) return;
                } 
                    

                if (limit == true && limitCount > 0)
                {
                    StartCoroutine(SpawnDrone());
                } else if (limit == false){
                    StartCoroutine(SpawnDrone());
                }
                
            }
        }

        if (droneCount < 0)
        {
            droneCount = 0;
        }
    }

    IEnumerator SpawnDrone()
    {
        if (bossSpawner && !GameObject.FindWithTag("Hernandez")) yield break;

        spawnRunning = true;
        GameObject spawnedDrone = Instantiate(drone, transform.position, Quaternion.identity);
        droneCount++;
        Debug.Log("Drone Count " + droneCount);
        if (limit == true)
        {
            limitCount--;
            Debug.Log("Drones left: "+limitCount);
        }
        //Debug.Log("Drone Count: " + droneCount);
        if (bossSpawner)
        {
            yield return new WaitForSeconds(10f);
            spawnedDrone.GetComponent<Drone>().bossDrone = true;
        } else {
            yield return new WaitForSeconds(20f);
        }
        
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
        if (collision.gameObject.tag == "Player" && bossSpawner == false)
        {
            inRange = false;
        }
    }
}
