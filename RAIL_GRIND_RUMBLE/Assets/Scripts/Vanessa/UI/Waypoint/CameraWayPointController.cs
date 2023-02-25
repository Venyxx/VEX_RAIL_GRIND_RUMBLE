
public class CameraWayPointController : CameraWayPointBaseController
{

    void FixedUpdate()
    {
        if (data.waypoints !=null && data.waypoints.Count > 0)
        {
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        foreach (WayPointController waypoint in data.waypoints)
        {
            waypoint.wayPointBaseController.data.item.image.transform.position = UIImagePosition(waypoint.wayPointBaseController.data.item);
            waypoint.wayPointBaseController.data.item.message.text = WayPointDistance(waypoint.waypointBaseController.data.item) + "M";
        }
    }
}
