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

}
