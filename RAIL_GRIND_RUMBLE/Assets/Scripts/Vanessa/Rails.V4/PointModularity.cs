using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointModularity : MonoBehaviour
{
    private RailModularity RailModularityREF;
    private bool gotNextB;
    private bool gotNextA;

    private GameObject NearB;
    private GameObject NearA;
    private GameObject nextRail;
    private GameObject pastRail;

   private void Start ()
    {
        RailModularityREF = gameObject.transform.parent.GetComponent<RailModularity>();
    }
   private void OnTriggerEnter (Collider col)
    {
        if (col.gameObject.name == "PointA" && !gotNextA)
        {
            NearA = col.gameObject;
            nextRail = col.gameObject.transform.parent.gameObject;
            gotNextA = true;
            Debug.Log("nearest A is on " + col.gameObject);
            RailModularityREF.SetNearAndNext(NearA, nextRail);

            
        }
        else if (col.gameObject.name == "PointB" && !gotNextB)
        {
            NearB = col.gameObject;
            pastRail = col.gameObject.transform.parent.gameObject;
            gotNextB = true;
            Debug.Log("nearest B is on " + col.gameObject);
            RailModularityREF.SetNearBAndPast(NearB, pastRail);
        }
    }
}
