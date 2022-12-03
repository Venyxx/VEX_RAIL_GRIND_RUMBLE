using UnityEngine;
using PathCreation;
using PathCreation.Examples;
using UnityEngine.InputSystem;

public class CollisionFollow : MonoBehaviour
{
    private PathFollower pathFollowerREF;
    private PathCreator pathCreatorREF;
    private RailModularity RailModularityREF;

    public bool isGrinding;
    public bool leftGrinding;
    public bool rightGrinding;
    

    
    // Start is called before the first frame update
        void OnCollisionEnter (Collision rail)
        {


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

    
    void OnTriggerExit (Collider col)
    {
        if (col.gameObject.GetComponent("PlayerRailRightCollider"))
        {
            Debug.Log("there was a right col");
            if (col.gameObject.name == "PositiveLeftRunner" || col.gameObject.name == "NegativeRightRunner")
            {
                col.gameObject.GetComponent<PlayerRailRightCollider>().ExitRailRight();
            }
        }

        else if (col.gameObject.GetComponent("PlayerRailLeftCollider"))
        {
            Debug.Log("there was a left col");
            if (col.gameObject.name == "PositiveRightCollider" || col.gameObject.name == "NegativeLeftCollider")
                {
                    col.gameObject.GetComponent<PlayerRailLeftCollider>().ExitRailLeft();
                }
        }
        
    }
 
    
}
