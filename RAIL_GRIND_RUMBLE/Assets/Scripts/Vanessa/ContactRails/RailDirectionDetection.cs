using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailDirectionDetection : MonoBehaviour
{
   
[SerializeField]
    private Moveable MoveableREF;
    [SerializeField]
    private ThirdPersonMovement ThirdPersonMovementREF;
    private Vector3 contactHit;


    

  void OnTriggerEnter (Collider collision)
   {
    //Debug.Log("hit");
    
    if (collision.tag == "RailStart")
    {
        Debug.Log("move towards railstart");
    }
    else if (collision.tag == "RailEnd")
    {
        Debug.Log("Move towards railend");
    }
   }



   

}
