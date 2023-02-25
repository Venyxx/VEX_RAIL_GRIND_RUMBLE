
using UnityEngine;


//world space waypoint track distance when nearby
public class WayPointController : MonoBehaviour
{
    public WayPointBaseController wayPointBaseController;
    
    // Start is called before the first frame update
    void Awake()
    {
        wayPointBaseController.SetTarget(GameObject.FindGameObjectWithTag("PlayerObject"));
        wayPointBaseController.SetTransform(transform);
        wayPointBaseController.EffectExist(false);

        if (transform.childCount > 0)
        {
            wayPointBaseController.EffectExist(true);
            wayPointBaseController.SetWayPointEffect (transform.GetChild(0).gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (wayPointBaseController.GetDistance(transform.position,wayPointBaseController.data.item.target.transform.position)
            < wayPointBaseController.data.interactDistance)
        {
            wayPointBaseController.EnableWaypoint(false);
            wayPointBaseController.EnableEffect(true);
        } else
        {
            wayPointBaseController.EnableWaypoint(true);
            wayPointBaseController.EnableEffect(false);
        }
    }
}
