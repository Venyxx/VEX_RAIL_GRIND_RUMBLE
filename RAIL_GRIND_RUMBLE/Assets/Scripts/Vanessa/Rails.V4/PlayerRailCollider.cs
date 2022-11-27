using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRailCollider : MonoBehaviour
{
    private ThirdPersonMovement ThirdPersonMovementREF;
    private RailModularity RailModularityREF;
    private RailModularity nextRailModularityREF;
    private GameObject RailMover;
    private bool isGrinding;

    // Start is called before the first frame update
    void Start()
    {
        ThirdPersonMovementREF = gameObject.GetComponent<ThirdPersonMovement>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrinding && !ThirdPersonMovementREF.walking)
        {
            //move player to runner
            gameObject.transform.position = Vector3.Lerp (gameObject.transform.position, RailMover.transform.position, 0.2f);
        }
    }

    void OnTriggerEnter (Collider col)
    {
        //did we run into the rail collider while skating?
        if (col.gameObject.name == "PositiveRunner" && !ThirdPersonMovementREF.walking)
        {
            RailMover = col.gameObject;
            // set player as child// change speed of runner to players speed
            gameObject.transform.parent = col.transform;
            col.gameObject.GetComponent<PositiveRunner>().SpeedAdjustment(ThirdPersonMovementREF.moveSpeed);
            //get components of current rail
            RailModularityREF = col.gameObject.transform.parent.GetComponent<RailModularity>();
            isGrinding = true; 
        }

        //are we alr grinding, and did we reach a modular rail end? A TO B
        if (isGrinding && col.gameObject.name == "PointA")
        {
            //Debug.Log("this is the end of the first rail keep moving");
        }

    }

    void OnTriggerExit (Collider col)
    {
        if (col.gameObject.name == "PositiveRunner")
        {
            var Rail = col.gameObject;
            transform.parent = null;
            Rail.GetComponent<PositiveRunner>().SpeedReAlignment();
            StartCoroutine(Rail.GetComponent<PositiveRunner>().Cooldown());
            isGrinding = false;
        }
    }

    public void JumpOffRail(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        transform.parent = null;
    }
}
