using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRailRightCollider : MonoBehaviour
{
    [SerializeField]private ThirdPersonMovement ThirdPersonMovementREF;
    [SerializeField]private RailModularity RailModularityREF;
    [SerializeField]private RailModularity nextRailModularityREF;
    [SerializeField]private GameObject playerPrefabREF;
    [SerializeField]private CollisionFollow playerCollisionFollowREF;
    private PositiveRunner positiveRunnerREF;

    [SerializeField]private GameObject PositiveRunner;
    [SerializeField]private GameObject NegativeRunner;
    
    
    [SerializeField]private GameObject RailMover;
    private bool thisScript;

    // Start is called before the first frame update
    void Start()
    {
        thisScript = false;
        playerPrefabREF = gameObject.transform.root.gameObject;
        ThirdPersonMovementREF = playerPrefabREF.GetComponent<ThirdPersonMovement>();
        playerCollisionFollowREF = playerPrefabREF.GetComponent<CollisionFollow>();

    }

    // Update is called once per frame
    void Update()
    {
        if (playerCollisionFollowREF.isGrinding && !ThirdPersonMovementREF.walking && thisScript)
        {
            //move player to runner
            playerPrefabREF.transform.position = Vector3.Lerp (playerPrefabREF.transform.position, RailMover.transform.position, 0.2f);
        }
    }

    void OnTriggerEnter (Collider col)
    {
        //did we run into the POSITIVE LEFT rail collider while skating?
        if (col.gameObject.name == "NegativeRightRunner" && !ThirdPersonMovementREF.walking && !playerCollisionFollowREF.isGrinding)
        {
            
            thisScript = true;
            Debug.Log("the right side of the player hit the right side of the negative runner");
            RailMover = col.transform.parent.gameObject;

            // set player as child 
            playerPrefabREF.transform.parent = col.gameObject.transform.parent.transform;

            // change speed of runner to players speed and set the rail to stop at the end instead of looping
            positiveRunnerREF = col.gameObject.transform.parent.GetComponent<PositiveRunner>();
            positiveRunnerREF.SpeedAdjustment(ThirdPersonMovementREF.moveSpeed);
            positiveRunnerREF.endOfPathInstruction = PathCreation.EndOfPathInstruction.Stop;


            //get components of current rail
            RailModularityREF = col.transform.root.gameObject.GetComponent<RailModularity>();
            PositiveRunner = RailModularityREF.PositiveRunner;
            NegativeRunner = RailModularityREF.NegativeRunner;

            // turn off the other rail runner
            PositiveRunner.SetActive(false);

            //we are now grinding
            playerCollisionFollowREF.isGrinding = true; 

        }else if (col.gameObject.name == "PositiveLeftRunner" && !ThirdPersonMovementREF.walking && !playerCollisionFollowREF.isGrinding)
        {
            
            thisScript = true;
            Debug.Log("the right side of the player hit the left side of the positive runner");
            RailMover = col.transform.parent.gameObject;

            // set player as child 
            playerPrefabREF.transform.parent = col.gameObject.transform.parent.transform;

            // change speed of runner to players speed and set the rail to stop at the end instead of looping
            positiveRunnerREF = col.gameObject.transform.parent.GetComponent<PositiveRunner>();
            positiveRunnerREF.SpeedAdjustment(ThirdPersonMovementREF.moveSpeed);
            positiveRunnerREF.endOfPathInstruction = PathCreation.EndOfPathInstruction.Stop;

            //get components of current rail
            RailModularityREF = col.transform.root.gameObject.GetComponent<RailModularity>();
            PositiveRunner = RailModularityREF.PositiveRunner;
            NegativeRunner = RailModularityREF.NegativeRunner;

            //Turn off other 
            NegativeRunner.SetActive(false);

            //grind em
            playerCollisionFollowREF.isGrinding = true; 
        }    


    }

    public void ExitRailRight ()
    {
        //free player from rail
            playerPrefabREF.transform.parent = null;

            //put base rail speed back
            if(PositiveRunner != null) PositiveRunner.GetComponent<PositiveRunner>().SpeedReAlignment();
            if (NegativeRunner != null) NegativeRunner.GetComponent<PositiveRunner>().SpeedReAlignment();

            //Rail cooldown
            if(PositiveRunner != null) StartCoroutine(PositiveRunner.GetComponent<PositiveRunner>().Cooldown());
            if (NegativeRunner != null) StartCoroutine(NegativeRunner.GetComponent<PositiveRunner>().Cooldown());

            //turn both runners back on
            PositiveRunner.SetActive(true);
            NegativeRunner.SetActive(true);
            playerCollisionFollowREF.isGrinding = false;

            //Reset loop behavior
            positiveRunnerREF.endOfPathInstruction = PathCreation.EndOfPathInstruction.Loop;
            thisScript = false;
    }

    public void JumpOffRail(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        //turn both runners back on
        PositiveRunner.SetActive(true);
        NegativeRunner.SetActive(true);

        //free player from rail
        playerPrefabREF.transform.parent = null;
        playerCollisionFollowREF.isGrinding = false;

        //Reset loop behavior
        positiveRunnerREF.endOfPathInstruction = PathCreation.EndOfPathInstruction.Loop;
        thisScript = false;
    }
}
