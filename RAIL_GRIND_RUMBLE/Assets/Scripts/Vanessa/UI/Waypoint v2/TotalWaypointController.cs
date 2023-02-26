using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TotalWaypointController : MonoBehaviour
{

    public List<GameObject> waypoints;

    public Transform finalDestination;
    private GameObject playerREF;
    Transform player;
    public int currentIndex;
    private GameObject realWaypoint;
    // Start is called before the first frame update
    void Start()
    {
        currentIndex = 1;
        waypoints = new List<GameObject>();
        GameObject playerREF = GameObject.Find("AriRig");
        player = playerREF.GetComponent<Transform>();

        realWaypoint = GameObject.Find("Waypoint");
        waypoints = GameObject.FindGameObjectsWithTag("WayPoint").ToList();


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //normally this would check if you have a quest, then the waypoints would be passed through quest giver into here.


        //Sorts waypoints by distance
        for (int i = 0; i < waypoints.Count; i++)
        {
            if (waypoints[0] != null && Vector3.Distance(finalDestination.position, waypoints[0].transform.position) > Vector3.Distance(finalDestination.position, waypoints[i].transform.position))
            {
                GameObject temp = waypoints[i];
                waypoints.Remove(waypoints[i]);
                waypoints.Insert(0, temp);
            }
        }
        for  (int i = 0; i < waypoints.Count; i++)
        {
            if(currentIndex == 0) return;

            if (Vector3.Distance(player.position, finalDestination.position)> Vector3.Distance(waypoints[i].transform.position, finalDestination.position))
            {
                //Debug.Log(i);

                if (i < waypoints.Count - 1)
                {
                    if (Vector3.Distance(player.position, finalDestination.position) < 
                        Vector3.Distance(waypoints[i + 1].transform.position, finalDestination.position))
                        {
                            //set the waypoint to wp[i]
                            Debug.Log("setting the wp to" + waypoints[i]);

                            currentIndex = i;
                            
                        }
                } 
                else
                {
                    Debug.Log("setting wp to farthest point");
                    currentIndex = i;
                    
                }

                
            } 
                  
        }
        
    }


    private void UpdateWaypoints ()
    {
        

        
    }
}
