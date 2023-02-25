
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WayPointManager : MonoBehaviour
{
    [System.Serializable]
   
   struct Data 
   {
    public int totalWayPoints;
    public GameObject waypointCanvas;
    public GameObject worldWayPoints;
    public GameObject waypointUIPrefab;
    public List <GameObject> waypointPrefab;
   }

   [SerializeField] Data data;

   private void Awake ()
   {
    for (int i = 0; i < data.totalWayPoints; i++)
    {
        if (data.waypointPrefab.Count > 0)
        {
            for ( int j = 0; j < data.waypointPrefab.Count; j++)
            {
                GameObject tmpWaypoint = Instantiate(data.waypointPrefab[j]);
                WaypointController tmpWayPointController = tmpWaypoint.GetComponent<WayPointController>();

                GameObject tmpWaypointUI = Instantiate(data.waypointUIPrefab);
                tmpWaypointUI.GetComponent<Image>().sprite = tmpWayPointController.wayPointBaseController.data.item.icon;
                tmpWaypointUI.transform.SetParent(data.waypointCanvas.transform);

                tmpWayPointController.wayPointBaseController.data.item.image = tmpWaypointUI.GetComponent<Image>();
                tmpWayPointController.wayPointBaseController.data.item.message = tmpWaypointUI.transform.GetChild(0).GetComponent<Text>();
                tmpWayPointController.wayPointBaseController.data.item.waypointUI = tmpWaypointUI;
                tmpWayPointController.transform.SetParent(data.worldWayPoints.transform);
                tmpWayPointController.transform.position = new Vector3 (Random.Range(5,995), 0.5f, Random.Range(5.995));
            }
        }
    }
   }
}
