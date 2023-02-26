using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class TotalWaypointController : MonoBehaviour
{

    public List<GameObject> waypoints;

    public Transform finalDestination;
    private GameObject playerREF;
    private Transform player;
    private int currentIndex;
    


    private TMP_Text distanceText;
    private RectTransform waypoint;
    public RectTransform prefab;
    public bool posToFinal;
    // Start is called before the first frame update
    void Start()
    {
        var canvas = GameObject.Find("WaypointsCanvas").transform;

        waypoint = Instantiate(prefab, canvas);
        
        currentIndex = 1;
        waypoints = new List<GameObject>();
        GameObject playerREF = GameObject.Find("AriRig");
        player = playerREF.GetComponent<Transform>();


        waypoints = GameObject.FindGameObjectsWithTag("WayPoint").ToList();

        distanceText = waypoint.GetComponentInChildren<TMP_Text>();


    }

    // Update is called once per frame
    void Update()
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
            //if(currentIndex == 0) return;

            if (Vector3.Distance(player.position, finalDestination.position)> Vector3.Distance(waypoints[i].transform.position, finalDestination.position))
            {
                //Debug.Log(i);

                if (i < waypoints.Count - 1)
                {
                    if (Vector3.Distance(player.position, finalDestination.position) < 
                        Vector3.Distance(waypoints[i + 1].transform.position, finalDestination.position))
                        {
                            //set the waypoint to wp[i]
                            //Debug.Log("setting the wp to" + waypoints[i]);

                            currentIndex = i;
                           
                        }
                } 
                else
                {
                    //Debug.Log("setting wp to farthest point");
                    currentIndex = i;
                    
                }

                var screenPos = Camera.main.WorldToScreenPoint(waypoints[i].transform.position);
                waypoint.position = screenPos;
        
                if (posToFinal)
                    distanceText.text = Vector3.Distance(player.position, finalDestination.position).ToString("0.0") + " m";
                else
                    distanceText.text = Vector3.Distance(player.position, waypoints[currentIndex].transform.position).ToString("0.0") + " m";
            } 
                  
        }

        
        
    }


    private void UpdateWaypoints ()
    {
        

        
    }
}
