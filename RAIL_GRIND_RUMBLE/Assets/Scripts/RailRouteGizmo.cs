using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailRouteGizmo : MonoBehaviour
{
    [SerializeField]
    private Transform[] controlPoints;

    private Vector2 gizmosPositionREF;

    private void OnDrawGizmos()
    {
        /*for (float t = 0; t <= 1; t += 0.05f)
        {
         gizmosPositionREF = Mathf.Pow (1-t, 3) * controlPoints[0].position + 
            3 * Mathf.Pow(1 - t, 2) * t * controlPoints[1].position + 
            3 * (1 - t) * Mathf.Pow(t,2) * controlPoints[2].position +
            Mathf.Pow(t,3) * controlPoints[3].position;

            Gizmos.DrawSphere(gizmosPositionREF, 0.25f);
        }
    
        Gizmos.DrawLine (new Vector3(controlPoints[0].position, controlPoints[1].position));
        Gizmos.DrawLine (new Vector3(controlPoints[1].position, controlPoints[2].position));
    */
    }
}

