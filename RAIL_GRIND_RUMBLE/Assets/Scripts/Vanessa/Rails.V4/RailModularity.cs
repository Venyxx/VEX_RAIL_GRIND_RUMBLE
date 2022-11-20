using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailModularity : MonoBehaviour
{
    private GameObject PointA;
    private GameObject PointB;
    private GameObject currentRail;
    

    //these values get passed in from pointmodularity
    private GameObject NearB;
    private GameObject NearA;
    private GameObject nextRail;
    private GameObject pastRail;


    // Start is called before the first frame update
    void Start()
    {
        currentRail = gameObject;
        PointA = currentRail.transform.Find("PointA").gameObject;
        PointB = currentRail.transform.Find("PointB").gameObject;
        

    }


    // Update is called once per frame
    void Update()
    {
   
    }

    public void SetNearAndNext (GameObject near, GameObject next)
    {
        NearA = near;
        nextRail = next;
    }

    public void SetNearBAndPast (GameObject near, GameObject past)
    {
        NearB = near;
        pastRail = past;
    }

    


}
