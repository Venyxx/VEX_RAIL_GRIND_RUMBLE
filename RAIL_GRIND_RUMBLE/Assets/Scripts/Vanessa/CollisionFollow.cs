using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using PathCreation.Examples;

public class CollisionFollow : MonoBehaviour
{
    private PathFollower pathFollowerREF;
    private PathCreator pathCreatorREF;
    
    private Transform PointA;
    private Transform PointB;
    
    

    
    // Start is called before the first frame update
    private void OnCollisionEnter (Collision rail)
    {
        if (rail.gameObject.tag == "Rail")
    {
        //check for direction the player will go
        PointA = rail.transform.Find("PointA");
        PointB = rail.transform.Find("PointB");

        Vector3 currentPos = transform.position;
        float distanceFromA = Vector3.Distance(PointA.position, currentPos);
        float distanceFromB = Vector3.Distance(PointB.position, currentPos);

        
        if (distanceFromA <= distanceFromB)
        {
            Debug.Log("found a is the closest");
        }
        else if (distanceFromA > distanceFromB)
        {
            Debug.Log("found b is the closest");
        }



        var RoadCreator = rail.transform.Find("Road Creator");
        pathFollowerREF = gameObject.GetComponent<PathFollower>();
       
        pathCreatorREF = RoadCreator.GetComponent<PathCreator>();
        
        pathFollowerREF.pathCreator = pathCreatorREF;

    }

    }


    private void OnCollisionExit (Collision rail)
    {
        if (rail.gameObject.tag == "Rail")
        {
           
         pathFollowerREF.distanceTravelled = 0;
       
        
        }
    }
    private void Update ()
   {
    
    if (Input.GetKey(KeyCode.Space) && pathFollowerREF != null)
    {
        pathFollowerREF.pathCreator = null;
    }
   }
}
