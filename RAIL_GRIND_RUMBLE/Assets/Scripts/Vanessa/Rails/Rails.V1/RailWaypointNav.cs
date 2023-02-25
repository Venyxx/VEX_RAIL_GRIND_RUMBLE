using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RailWaypointNav : MonoBehaviour
{
    [SerializeField]
    private Moveable MoveableREF;
    [SerializeField]
    private ThirdPersonMovement ThirdPersonMovementREF;

    private List<Transform> waypoints;

    private int nextWayPointIndex;

    private void FixedUpdate()
    {
        if (MoveableREF.canGrind == true)
        {
            waypoints = GetComponentsInChildren<Transform>().ToList();
            
            MoveToNextWaypoint();
        }

    }

    private void MoveToNextWaypoint()
    {
      
        if (MoveableREF.startToEnd)
        {
            nextWayPointIndex = 1;
            
        }
        

        var targetWayPointTransform = waypoints[nextWayPointIndex];
        MoveableREF.MoveTo(targetWayPointTransform.position);

        
        MoveableREF.transform.LookAt(waypoints[nextWayPointIndex].position); //CURRENTLY CHANGES LOOK DIRECTION

        if (Vector3.Distance(MoveableREF.transform.position,targetWayPointTransform.position) < 0.001f )
        {
            
            //nextWayPointIndex++;
            //Debug.Log(nextWayPointIndex);
                MoveableREF.canGrind = false;

                //A LIL PUSH
                ThirdPersonMovementREF.rigidBody.velocity =
                    new Vector3(ThirdPersonMovementREF.rigidBody.velocity.x, 0f, ThirdPersonMovementREF.rigidBody.velocity.z);

                ThirdPersonMovementREF.rigidBody.AddForce(transform.forward * 2f, ForceMode.Impulse);
                //nextWayPointIndex = 0; //THIS LINE WOULD BE TO MAKE IT AN INFINITE LOOP, CLOSED CIRCLE
            
        }
        
    }
}
