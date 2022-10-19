using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WallRun : MonoBehaviour
{
    //References
    private GameObject playerREF;
    public Transform orientation;
    private ThirdPersonMovement playerScript;
    private Rigidbody rigidBody;

    //Wallrunning
    public LayerMask wall;
    private float wallRunForce = 200f;
    public float maxWallRunTime;
    private float wallRunTimer;
    private bool isWallRunning;
    private float wallJumpSideForce = 12f;
    private float wallJumpUpForce = 7f;

    //Input
    private float horizontalInput;
    private float verticalInput;

    //Wall Detection
    private float wallCheckDistance = 0.7f;
    private RaycastHit leftWallHit;
    private RaycastHit rightWallHit;
    private bool wallLeft;
    private bool wallRight;

    //Exiting Wall
    private bool exitingWall = false;
    private float exitWallTime = 0.2f;
    private float exitWallTimer;


    private float setY;
    
    void Start()
    {
        playerREF = this.gameObject;
       playerScript = playerREF.gameObject.GetComponent<ThirdPersonMovement>();
       rigidBody = playerREF.GetComponent<Rigidbody>();
    }

    void Update()
    {
        CheckForWall();
        StateMachine();
    }

    void FixedUpdate()
    {
        if (isWallRunning == true)
        {
             WallRunMovement();
        }
    }

    private void CheckForWall()
    {
        wallRight = Physics.Raycast(transform.position, orientation.right, out rightWallHit, wallCheckDistance, wall);
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallHit, wallCheckDistance, wall);
    }

    private void StateMachine()
    {
        //Get Player Input
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //Wallrunning
        if((wallLeft || wallRight) && verticalInput > 0 && playerScript.grounded == false && !exitingWall)
        {
            ///Check if player is wallrunnning
            StartWallRun();
            

            //Walljump (old input system implementation)
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Wall jump input detected");
                WallJump();
            }
        } 
        else if (exitingWall == true){

            if (isWallRunning == true)
            {
                StopWallRun();
            }

            if (exitWallTimer > 0)
            {
                exitWallTimer -= Time.deltaTime;
            }

            if (exitWallTimer <= 0)
            {
                exitingWall = false;
            }

        } else {
            StopWallRun();
        }
    }

    //new input system version, tied to SPACEBAR for now
    //needs additional logic to prevent the player from 
    //walljumping forever after a wall run.
    /*
    public void WallJumpInput(InputAction.CallbackContext context)
    {
        //if the context is anything BUT just pressing the button/key, end the method.
        //ensures that the method doesn't get called one for a press, once every frame held, and once when released
        if (!context.started) return;
        Debug.Log("Wall jump input detected");
        WallJump();
    }
    */
    private void StartWallRun()
    {
        rigidBody.useGravity = false;
        isWallRunning = true;
    }
    
    private void WallRunMovement()
    {
        //Suspend player's vertical movement
        rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z);

        //Discover forward direction of wall based on player orientation
        Vector3 wallNormal = wallRight ? rightWallHit.normal : leftWallHit.normal;
        Vector3 wallForward = Vector3.Cross(wallNormal, transform.up);

        //Decide forward direction of player based on approach
        if ((orientation.forward - wallForward).magnitude > (orientation.forward - -wallForward).magnitude)
        {
            wallForward = -wallForward;
        }

        //Forward Force
        rigidBody.AddForce(wallForward * wallRunForce, ForceMode.Force);
        

    }

    private void StopWallRun()
    {
        isWallRunning = false;
        rigidBody.useGravity = true;
    }

    private void WallJump()
    {
        //Enter exit wall state
        exitingWall = true;
        exitWallTimer = exitWallTime;

        Vector3 wallNormal = wallRight ? rightWallHit.normal : leftWallHit.normal;
        Vector3 forceToApply = transform.up * wallJumpUpForce + wallNormal * wallJumpSideForce;

        //Reset y velocity, then add force to rigidbody
        rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z);
        rigidBody.AddForce(forceToApply, ForceMode.Impulse);
        Debug.Log("completed wall jump method");
    }

}
