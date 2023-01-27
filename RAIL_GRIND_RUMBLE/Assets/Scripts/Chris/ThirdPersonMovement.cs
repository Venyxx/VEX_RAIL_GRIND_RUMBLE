using System.Collections;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor;
using TMPro;
using UnityEngine.PlayerLoop;

public class ThirdPersonMovement : MonoBehaviour
{
    //Movement
    public float moveSpeed;
    [SerializeField] private float walkSpeed = 0.3f;
    public float currentSpeed;
    private float baseMoveSpeed;
    private float speedLerp;
    public float groundDrag;
    Vector3 standingStill = new Vector3 (0,0,0);
    private Vector2 moveInput;
    public float maxSkateSpeed;
    private float slopeLimit;

    public double speedPrint;

    private CollisionFollow playerCollisionFollowREF;
    private PlayerRailLeftCollider playerLeftColREF;
    private PlayerRailRightCollider playerRightColREF;

    //Jump
    //[SerializeField]private float jumpForceMax;
    //[SerializeField]private float jumpForceMin;
    [SerializeField]private float jumpForce;
    //[SerializeField]private float additionalJumpForce;
    bool isJumping;
    bool jumpDelayRunning = false;
    float jumpTimeCounter;
    private float jumpTimer;

    public float jumpCoolDown;
    public float airMultiplier;
    public bool canJump;
    
    //Grounded Check
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool Grounded;

    //Grapple Check
    public bool isGrappling;
    public float swingSpeed;

    private Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;
    Vector3 skateDirection;

    public GameObject DialogueBox { get; private set; }
    public DialogueTemplate nearestDialogueTemplate = null;



    // animation IDs
    private float _animationBlend;
    private Animator _animator;
    private int _animIDSpeed;
    private int _animIDGrounded;
    private int _animIDJump;
    private int _animIDFreeFall;
    private int _animIDWalking;
    private int _animIDGrinding;
    private int _animIDMotionSpeed;
    private int _animIDWallRunLeft;
    private int _animIDWallRunRight;
    public float SpeedChangeRate = 10.0f;
    private float targetSpeed;
    public bool analogMovement;

    


    //Acceleration Timer
    private float maxTime = 1.5f;
    private float currentTime;
    private bool canAccelerate;

    public InputHandler playerActions { get; private set; }
    private bool moveKeyUp;

    private bool jump;
    private bool jumpCancel;

    public bool isWalking { get; private set; } = true;


    public Rigidbody rigidBody;

    //Coins
    public static int coinCount;
    GameObject coinCounterREF;
    TextMeshProUGUI coinCountText;

    //speed hud
    private TextMeshProUGUI speedUIText;

    //Camera Switching
    GameObject currentCam;

    private GameObject[] skateWheels;
    private GameObject[] skateShoes;
    private GameObject playerWeapon;
    private GameObject sprayCan;
    [SerializeField] private Material skatesOnMaterial;
    [SerializeField] private Material skatesOffMaterial;

    //COLLIDER STUFF (so ari gets shorter when skates are turned off)
    private CapsuleCollider ariCollider;
    private static float skateCenterY = 0.8497467f;
    private static float skateHeight = 1.92239f;
    private static float walkCenterY = 0.8990228f;
    private static float walkHeight = 1.823838f; 

    //Sound Effects
    [SerializeField] private AudioClip[] playerSounds;
    private AudioSource audioSource;

 
        
    // Start is called before the first frame update
    void Start()
    {

        moveSpeed = 0;
        baseMoveSpeed= 8;
        speedLerp = 2.22f;
        //max skate speed will be a better changable var later
        maxSkateSpeed = 15;
        playerCollisionFollowREF = gameObject.GetComponent<CollisionFollow>();
        playerLeftColREF = transform.Find("AriRig").gameObject.transform.Find("LeftCollider").gameObject.GetComponent<PlayerRailLeftCollider>();
        playerRightColREF = transform.Find("AriRig").gameObject.transform.Find("RightCollider").gameObject.GetComponent<PlayerRailRightCollider>();


        playerActions = new InputHandler();
        playerActions.Player.Enable();
        rigidBody.freezeRotation = true;
        isGrappling = false;
        canJump = true;

        //animation setting
        _animator = transform.Find("AriRig").gameObject.GetComponent<Animator>();
         AssignAnimationIDs();

        currentTime = 0;

        GameObject orientationREF = GameObject.Find("Orientation");
        orientation = orientationREF.gameObject.GetComponent<Transform>();
        

        //Coin Counter
        coinCounterREF = GameObject.Find("CoinCounter");
        coinCountText = coinCounterREF.GetComponent<TextMeshProUGUI>();

        //Change/remove this line later based on level-to-level gameplay
        coinCount = 0;

        //set speed UI element
        speedUIText = GameObject.Find("Speed").GetComponent<TextMeshProUGUI>();

        skateShoes = GameObject.FindGameObjectsWithTag("SkateBody");
        skateWheels = GameObject.FindGameObjectsWithTag("SkateWheel");
        playerWeapon = GameObject.FindGameObjectWithTag("PlayerWeapon");
        sprayCan = GameObject.FindGameObjectWithTag("PlayerCan");
        ariCollider = GetComponent<CapsuleCollider>();
        WalkToggleHelper();

        //Audio
        audioSource = GetComponent<AudioSource>();
        
        DialogueBox = GameObject.FindWithTag("DialogueBox");
    }

    // Update is called once per frame
    void Update()
    {
        
        //Grounded Check
        Grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        _animator.SetBool(_animIDGrounded, Grounded);

        PlayerInput();

        //Drag
        if (Grounded == true && currentSpeed <= 0)
        {
            rigidBody.drag = groundDrag;
 
        }else 
        {
            rigidBody.drag = 0;  
        }


        //Hold Jump WIP
        if (jumpDelayRunning == false)
        {
            if (isJumping == true && jumpTimeCounter > 0)
            {
                TapJump(jumpForce/3);
                jumpTimeCounter -= Time.deltaTime;
            } else 
            {
                isJumping = false;
            }
        }

        //grinding anims
        if (gameObject.GetComponent<CollisionFollow>().isGrinding)
            _animator.SetBool(_animIDGrinding, true);
        else if (!gameObject.GetComponent<CollisionFollow>().isGrinding)
            _animator.SetBool(_animIDGrinding, false);

        

        //animation transitioning

        if (rigidBody.velocity.magnitude > 4 )     
        {
            //slide by anim
            if (verticalInput == 0)
                targetSpeed = Mathf.Lerp(targetSpeed, 2, 0.35f);
            else
            targetSpeed = Mathf.Lerp(targetSpeed, 3, 0.5f);
              
        }
        else if (rigidBody.velocity.magnitude < 4 && rigidBody.velocity.magnitude > 1)
        {
            if (horizontalInput == 0 || verticalInput == 0)
            {
               //normal walking slow run animation, only plays during decel not accel
                targetSpeed = Mathf.Lerp(targetSpeed, 2, 0.35f);
            }
        }
        else 
            targetSpeed = Mathf.Lerp(targetSpeed, 1, 0.35f);
            


    }

    
    
    public void Jump(InputAction.CallbackContext context)
    {
        if (isWalking || DialogueBox.activeInHierarchy) return;
        //if (isGrappling) return;
        
        if (context.started && Grounded)
        {
            isJumping = true;
            jumpTimeCounter = 0.35f;
            _animator.SetBool(_animIDJump, true);
            _animator.SetBool(_animIDGrounded, false);
            TapJump(jumpForce);
            PlaySound(1);

            ExitRailMain();
            StartCoroutine(JumpHoldDelay());
        }

        if (context.canceled)
        {
            isJumping = false;
            
        }
    }

    public void ToggleWalk(InputAction.CallbackContext context)
    {
        if (!context.started || DialogueBox.activeInHierarchy) return;
        isWalking = !isWalking;
        WalkToggleHelper();
    }

    void WalkToggleHelper()
    {
        Debug.Log($"Walking: {isWalking}");

        foreach (GameObject skateWheel in skateWheels)
            skateWheel.SetActive(!isWalking); //if walking, turn off wheels. 

        foreach (GameObject shoe in skateShoes)
            shoe.GetComponent<SkinnedMeshRenderer>().material = isWalking ? skatesOffMaterial : skatesOnMaterial;

        if (isWalking)
        {
            ariCollider.center = new Vector3(ariCollider.center.x, walkCenterY, ariCollider.center.z);
            ariCollider.height = walkHeight;
            playerWeapon.SetActive(false);
            sprayCan.SetActive(true);
        }
        else
        {
            ariCollider.center = new Vector3(ariCollider.center.x, skateCenterY, ariCollider.center.z);
            ariCollider.height = skateHeight;
            playerWeapon.SetActive(true);
            sprayCan.SetActive(false);
        }
    }

    IEnumerator JumpHoldDelay()
    {
        jumpDelayRunning = true;
        yield return new WaitForSeconds(0.15f);
        jumpDelayRunning = false;
    }

    void FixedUpdate()
    {
        PlayerMovement();
    }

    

    public void PlayerInput()
    {
        
        if (!DialogueBox.activeInHierarchy)
        {
            moveInput = playerActions.Player.Move.ReadValue<Vector2>();
            horizontalInput = moveInput.x/2;
            verticalInput = moveInput.y/2;
        }
        else
        {
            moveInput = new Vector2(0, 0);
            horizontalInput = 0;
            verticalInput = 0;
        }


        if (rigidBody.velocity.magnitude < 1)
        { currentSpeed = 0; }
                

        //if there is player input and we are, accelerate
        if (verticalInput > 0.1 && GetComponent<WallRun>().isWallRunning == false)
        {
            moveKeyUp = false;
            //kick start movement
            if (rigidBody.velocity.magnitude < baseMoveSpeed)
                currentSpeed = baseMoveSpeed;

            //accelerate
            float acceleration = 2f;
            currentSpeed += acceleration * Time.deltaTime;

        } else if (verticalInput < 0.1)
        {
            //check if the back arrow is active    
            if (verticalInput < 0.1 && rigidBody.velocity.magnitude > 2)
            {
                //decel faster as a breaking mech
                moveKeyUp = true;
                float Adeceleration = 10f;

                //if moving, decelerate
                //Debug.Log("breaking mech");
                if (currentSpeed > 0)
                currentSpeed -= Adeceleration * Time.deltaTime;

            }else 
            {
            //if no input forward
            moveKeyUp = true;
            float deceleration = 5f;

            //if moving, decelerate
            if (currentSpeed > 0 || rigidBody.velocity.magnitude > 0)
            currentSpeed -= deceleration * Time.deltaTime;

            }
                
       
        }

    }

    void PlayerMovement()
    {
        //Uncomment once air movement is programmed
        if (isGrappling == true)
        {
            return;
        }
        
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (verticalInput != 0 && horizontalInput != 0 && !DialogueBox.activeInHierarchy)
            skateDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        else 
            skateDirection = orientation.forward;

            if (isWalking)
        {
            rigidBody.velocity = new Vector3(moveDirection.normalized.x * walkSpeed * 10f, rigidBody.velocity.y, moveDirection.normalized.z * walkSpeed * 10f);

            //change anim
            if ((moveInput.x != 0 || moveInput.y != 0) && !DialogueBox.activeInHierarchy)
                _animator.SetBool(_animIDWalking, true);
            else
                _animator.SetBool(_animIDWalking, false);
                
            //target speed changes the anim blend tree.
            targetSpeed = 0;
            
        }
        else if (Grounded)
        {
            //moves with acceleration for forward, just walks sideways
            if (currentSpeed > 1)
            {
                 rigidBody.velocity = new Vector3(skateDirection.normalized.x * currentSpeed, rigidBody.velocity.y, skateDirection.normalized.z * currentSpeed);
                 //PlaySound(3);
            }  
            else if (verticalInput < 0 || horizontalInput != 0)
                rigidBody.velocity = new Vector3(moveDirection.normalized.x * walkSpeed * 10f, rigidBody.velocity.y, moveDirection.normalized.z * walkSpeed * 10f);

            //else 
            //rigidBody.velocity = new Vector3( rigidBody.velocity.x, rigidBody.velocity.y,  rigidBody.velocity.z); //PROBLEM AREA
            
            
            //change anim
            _animator.SetBool(_animIDWalking, false);
            _animator.SetBool(_animIDJump, false);
  
        } 
        else 
        {
            if (isGrappling == true)
            {
                rigidBody.AddForce(moveDirection.normalized * currentSpeed * 10f * swingSpeed, ForceMode.Force);
            } else {
                rigidBody.AddForce(moveDirection.normalized * currentSpeed * 10f * airMultiplier, ForceMode.Force);
            }
            _animator.SetBool(_animIDJump, true);
        }

        //checking for wall running animations
        if (moveInput.x != 0 || moveInput.y != 0)
        {
            if (gameObject.GetComponent<WallRun>().wallLeft  ||   gameObject.GetComponent<WallRun>().isWallRunning)
                _animator.SetBool(_animIDWallRunLeft, true);
            else if (gameObject.GetComponent<WallRun>().wallRight   ||   gameObject.GetComponent<WallRun>().isWallRunning)
                _animator.SetBool(_animIDWallRunRight, true);
            else
            {
                _animator.SetBool(_animIDWallRunLeft, false);
                _animator.SetBool(_animIDWallRunRight, false);
            }
                
        }

        //checking for grinding animations
        if (gameObject.GetComponent<CollisionFollow>().isGrinding)
        {
            _animator.SetBool(_animIDGrinding, true);
        }else
            _animator.SetBool(_animIDGrinding, false);


        //clamp skate speed
        if (rigidBody.velocity.magnitude > 14) //arbritrary cap point
        {

            Vector3 passing = Vector3.Normalize(rigidBody.velocity);
            passing *= maxSkateSpeed;

            rigidBody.velocity = passing;
        }
            



        
        //adjust animator speed
        _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate);
        if (_animationBlend < 0.01f) _animationBlend = 0f;

        _animator.SetFloat(_animIDSpeed, _animationBlend);
        //_animator.SetFloat(_animIDMotionSpeed, inputMagnitude);

        //update speed UI
        SetSpeedUI();

    }

    void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rigidBody.velocity = new Vector3(limitedVel.x, rigidBody.velocity.y, limitedVel.z); 
            
            
        }

        if (horizontalInput == 0 && verticalInput == 0 && !moveKeyUp)
        {
            rigidBody.velocity -= 0.1f * rigidBody.velocity;
            moveKeyUp = true;
        }

        
        
    }

    void TapJump(float force)
    {
        rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z);
        rigidBody.AddForce(transform.up * force, ForceMode.Impulse);
    }

    void ResetJump()
    {
        canJump = true;
    }

    private void AssignAnimationIDs()
    {
        _animIDSpeed = Animator.StringToHash("Speed");
        _animIDGrounded = Animator.StringToHash("Grounded");
        _animIDJump = Animator.StringToHash("Jump");
        _animIDFreeFall = Animator.StringToHash("FreeFall");
        _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
        _animIDWalking = Animator.StringToHash("Walking");
        _animIDGrinding = Animator.StringToHash("Grinding");
        _animIDWallRunLeft = Animator.StringToHash("WallRunLeft");
        _animIDWallRunRight = Animator.StringToHash("WallRunRight");
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") || collision.gameObject.tag == "Rail")
        {
            if (canJump == false)
            {
                Invoke(nameof(ResetJump), jumpCoolDown);
            }
        }
    }

    void OnTriggerEnter (Collider collider)
    {
        if (collider.gameObject.tag == "END")
        {
            ExitRailMain();
        }
    }

    public void AddCoin(int coin)
    {
            coinCount = coinCount + coin;
            Debug.Log(coinCount);
            coinCountText.text = $"{coinCount}";
            PlaySound(0);
    }

    //Camera Switch
    public void SwitchCameraTPM(InputAction.CallbackContext context)
    {
        if (context.performed || DialogueBox.activeInHierarchy) return;
        
        if (!isWalking)
        {
            if (context.started)
            {
                if (GameObject.Find("BasicCam") && isGrappling) 
                {
                    return;
                } else if (GameObject.Find("BasicCam"))
                {
                    currentCam = GameObject.Find("BasicCam");
                } else if (GameObject.Find("AimingCam"))
                {
                    currentCam = GameObject.Find("AimingCam");
                }
                currentCam.GetComponent<ThirdPersonCamera>().SwitchCameraStarted();
            }

            if (context.canceled)
            {
                currentCam.GetComponent<ThirdPersonCamera>().SwitchCameraCanceled();
            }
        }
        
    }

    private void SetSpeedUI()
    { 
        //m/s to mph
        var passing = rigidBody.velocity.magnitude;
        speedPrint =  (double) passing * 2.2369362920544;
        int print = (int) speedPrint;

        //print
        if (print < 10)
        {
            speedUIText.text = "0" + print.ToString();
        }else
        speedUIText.text = print.ToString();
    }

    private void ExitRailMain ()
    {
        //release from rail if grinding
            if (playerCollisionFollowREF.isGrinding)
            {
                if (playerCollisionFollowREF.leftGrinding)
                    playerLeftColREF.ExitRailLeft();
                else
                    playerRightColREF.ExitRailRight();

            }
    }

    public void PlaySound(int sound)
    {
        audioSource.clip = playerSounds[sound];
        audioSource.Play(0);

        //Coin = 0
        //Jump = 1
        //Railing = 2
        //Skating = 3
    }
  
}
