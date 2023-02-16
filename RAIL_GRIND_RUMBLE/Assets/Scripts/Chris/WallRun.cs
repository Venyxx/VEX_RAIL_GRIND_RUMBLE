using UnityEngine;
using UnityEngine.InputSystem;

public class WallRun : MonoBehaviour
{
    //References
    private GameObject playerREF;
    private GameObject playerObjectREF;
    private Transform orientation;
    private ThirdPersonMovement playerScript;
    private Rigidbody rigidBody;
    private InputHandler playerActions;

    //Wallrunning
    public LayerMask wall;
    private float wallRunForce = 50f;
    //public float maxWallRunTime;
    private float wallRunTimer;
    public bool isWallRunning;
    [SerializeField]private float wallJumpSideForce = 12f;
    [SerializeField]private float wallJumpUpForce = 7f;

    //Input
    private float horizontalInput;
    private float verticalInput;

    //Wall Detection
    private float wallCheckDistance = 1f;
    private RaycastHit leftWallHit;
    private RaycastHit rightWallHit;
    public bool wallLeft;
    public bool wallRight;

    //Exiting Wall
    private bool exitingWall = false;
    private float exitWallTime = 0.2f;
    private float exitWallTimer;


    private float setY;
    
    void Start()
    {
       playerREF = this.gameObject;
       playerScript = playerREF.gameObject.GetComponent<ThirdPersonMovement>();
       playerObjectREF = GameObject.FindWithTag("PlayerObject");
       rigidBody = playerREF.GetComponent<Rigidbody>();

       GameObject orientationREF = GameObject.Find("Orientation");
       orientation = orientationREF.gameObject.GetComponent<Transform>();
    }

    void Update()
    {
        if (playerActions == null)
        {
            playerActions = playerScript.playerActions;
        }
        CheckForWall();
        StateMachine();
    }

    void FixedUpdate()
    {
        if (isWallRunning == true && !playerScript.isWalking)
        {
            Debug.Log("WallRunning");
            WallRunMovement();
        }
    }

    private void CheckForWall()
    {
        wallRight = Physics.Raycast(playerObjectREF.transform.position, orientation.right, out rightWallHit, wallCheckDistance, wall);
        wallLeft = Physics.Raycast(playerObjectREF.transform.position, -orientation.right, out leftWallHit, wallCheckDistance, wall);
    }

    private void StateMachine()
    {
        //Get Player Input
        Vector2 moveInput = playerActions.Player.Move.ReadValue<Vector2>();
        float horizontalInput = moveInput.x;
        float verticalInput = moveInput.y;

        //Wallrunning
        if((wallLeft || wallRight) && verticalInput > 0 && playerScript.Grounded == false && !exitingWall && !playerScript.isWalking)
        {
            ///Check if player is wallrunnning
            StartWallRun();
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
    
    public void WallJumpInput(InputAction.CallbackContext context)
    {
        //if the context is anything BUT just pressing the button/key, end the method.
        //ensures that the method doesn't get called one for a press, once every frame held, and once when released
        if (!context.started) return;
        if (!isWallRunning) return;
        if (playerScript.dialogueBox.activeInHierarchy) return;
        Debug.Log("Wall jump input detected");
        playerScript.isJumping = true; // added by Kevin for air attack
        WallJump();
    }
    
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
