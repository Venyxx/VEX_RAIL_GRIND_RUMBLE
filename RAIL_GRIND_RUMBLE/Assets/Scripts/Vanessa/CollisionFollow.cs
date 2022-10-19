using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using PathCreation.Examples;

public class CollisionFollow : MonoBehaviour
{
    private PathFollower pathFollowerREF;
    private PathCreator pathCreatorREF;
    
    // Start is called before the first frame update
    private void OnCollisionEnter (Collision rail)
    {
        if (rail.gameObject.tag == "Rail")
    {
        
        var RoadCreator = rail.transform.Find("Road Creator");
        pathFollowerREF = gameObject.GetComponent<PathFollower>();
       
        pathCreatorREF = RoadCreator.GetComponent<PathCreator>();
        
        pathFollowerREF.pathCreator = pathCreatorREF;

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
