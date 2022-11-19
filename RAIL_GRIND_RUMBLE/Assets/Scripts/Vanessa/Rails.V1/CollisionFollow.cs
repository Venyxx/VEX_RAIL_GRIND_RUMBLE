using UnityEngine;
using PathCreation;
using PathCreation.Examples;
using UnityEngine.InputSystem;

public class CollisionFollow : MonoBehaviour
{
    private PathFollower pathFollowerREF;
    private PathCreator pathCreatorREF;
    
    private Transform PointA;
    private Transform PointB;
    private bool isGrinding;
    

    
    // Start is called before the first frame update
    //ive come to realize if you stay on it too long it resets the collision again.
        void OnCollisionEnter (Collision rail)
        {
            if (rail.gameObject.tag == "RailStraight" && !isGrinding)
            {
                Debug.Log("entering col");
                isGrinding = true;
                Debug.Log(isGrinding + "grinding state");
        
                //check for direction the player will go
                /*PointA = rail.transform.Find("PointA");
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
                }*/


                var RoadCreator = rail.transform.Find("Road Creator");
                pathFollowerREF = gameObject.GetComponent<PathFollower>();
            
                pathCreatorREF = RoadCreator.GetComponent<PathCreator>();
                
                pathFollowerREF.endOfPathInstruction = PathCreation.EndOfPathInstruction.Reverse;
                pathFollowerREF.pathCreator = pathCreatorREF;

            }

            if (rail.gameObject.tag == "RailCircular" && !isGrinding )
            {

                isGrinding = true;
                var RoadCreator = rail.transform.Find("Road Creator");
                pathFollowerREF = gameObject.GetComponent<PathFollower>();
            
                pathCreatorREF = RoadCreator.GetComponent<PathCreator>();
                
                pathFollowerREF.endOfPathInstruction = PathCreation.EndOfPathInstruction.Loop;
                pathFollowerREF.pathCreator = pathCreatorREF;

            }

        }
            
    


    void OnCollisionExit (Collision rail)
    {
        if (rail.gameObject.tag == "RailStraight" || rail.gameObject.tag == "RailCircular")
        { 
            pathFollowerREF.distanceTravelled = 0;
        }
    }
    void Update ()
    {
    
        /*if (Input.GetKey(KeyCode.Space) && pathFollowerREF != null)
        {
            pathFollowerREF.pathCreator = null;
        }*/
    }

    public void JumpOffRail(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (pathFollowerREF == null) return;
        pathFollowerREF.pathCreator = null;
        isGrinding = false;
    }
    
}
