
using System.Collections.Generic;
using UnityEngine;

public class CameraWayPointBaseController : MonoBehaviour
{
    [System.Serializable]

    public struct WaypointData
    {
        public GameObject worldWayPoints;
        public List<WayPointController> waypoints;
        [HideInInspector] public int screenWidth;
        [HideInInspector] public int screenHeight;
        [HideInInspector] public Camera fpsCam;
    }

    public WaypointData data;

    public void Start()
    {
        data.worldWayPoints = GameObject.Find("WorldWayPoints");
        data.fpsCam = Camera.main;
        data.screenWidth = Screen.width;
        data.screenHeight = Screen.height; 

        if (data.worldWayPoints.transform.childCount > 0 )
        {
            for (int i = 0; i < data.worldWayPoints.transform.childCount; i++)
            {
                WayPointController tmpWayPointController = data.worldWayPoints.transform.GetChild(i).GetComponent<WayPointController>();

                if (tmpWayPointController != null)
                {
                    data.waypoints.Add(tmpWayPointController);
                }
            }
        }
    }

    public Vector2 UIImagePosition (WayPointItem item)
    {
        float itemImageWidth = item.image.GetPixelAdjustedRect().width / 2;
        float itemImageHeight = item.image.GetPixelAdjustedRect().height / 2;

        Vector2 screenPosition = GetItemScreenPosition(item);
        screenPosition.x = ScreenClamp(screenPosition.x, itemImageWidth, data.screenWidth);
        screenPosition.y = ScreenClamp(screenPosition.y, itemImageHeight, data.screenHeight);

        return screenPosition;

    }

    public float WayPointDistance (WayPointItem item)
    {
        return Mathf.Round(Vector3.Distance(item.transform.position, transform.position));

    }

    public float ScreenClamp (float screenPosition, float itemImageWidth, int screenWidth)
    {
        return Mathf.Clamp (screenPosition, itemImageWidth, screenWidth - itemImageWidth);
    }

    public Vector2 GetItemScreenPosition (WayPointItem item)
    {
        float x = item.transform.position.x;
        float y = item.transform.position.y + item.heightOffset;
        float z = item.transform.position.z;
        Vector2 screenPosition = data.fpsCam.WorldToScreenPoint(new Vector3(x,y,z));

        if (Vector3.Dot((item.transform.position - transform.position), transform.forward) < 0)
        {
            if (screenPosition.x < Screen.width / 2)
            {
                screenPosition.x = Screen.width - item.image.GetPixelAdjustedRect().width / 2;
            } else
            {
                screenPosition.x = item.image.GetPixelAdjustedRect().width / 2;
            }
            
             if (screenPosition.y < Screen.height / 2)
            {
                screenPosition.y = Screen.height - item.image.GetPixelAdjustedRect().height / 2;
            } else
            {
                screenPosition.y = item.image.GetPixelAdjustedRect().height / 2;
            }
        }

        return screenPosition;
    }
}
