using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class ThirdPersonMovement : MonoBehaviour
{
    //Movement
    public float moveSpeed;
    [SerializeField] private float walkSpeed = 0.3f;
    private float baseMoveSpeed;
    private float speedLerp;
    public float groundDrag;
    Vector3 standingStill = new Vector3 (0,0,0);

    //Jump
    //[SerializeField]private float jumpForceMax;
    //[SerializeField]private float jumpForceMin;
    [SerializeField]private float jumpForce;
    //[SerializeField]private float additionalJumpForce;
    bool isJumping;
    bool jumpDelayRunning = false;
    float jumpTimeCounter;

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



    // animation IDs
    private float _animationBlend;
    private Animator _animator;
    private bool _hasAnimator;
    private int _animIDSpeed;
    private int _animIDGrounded;
    private int _animIDJump;
    private int _animIDFreeFall;
    private int _animIDMotionSpeed;
    public float SpeedChangeRate = 10.0f;
    private float targetSpeed;
    public bool analogMovement;


    //Acceleration Timer
    private float maxTime = 3.0f;
    private float currentTime;
    private bool canAccelerate;

    public InputHandler playerActions { get; private set; }
    private bool moveKeyUp;

    private bool jump;
    private bool jumpCancel;

    public bool walking = true;


    public Rigidbody rigidBody;

    //Coins
    public static int coinCount;
    GameObject coinCounterREF;
    TextMeshProUGUI coinCountText;

    //Camera Switching
    GameObject currentCam;

    private GameObject[] skateWheels;
    private GameObject[] skateShoes;
    [SerializeField] private Material skatesOnMaterial;
    [SerializeField] private Material skatesOffMaterial;

    //COLLIDER STUFF (so ari gets shorter when skates are turned off)
    private CapsuleCollider ariCollider;
    private static float skateCenterY = 0.8903141f;
    private static float skateHeight = 1.841251f;
    private static float walkCenterY = 0.929162f;
    private static float walkHeight = 1.763556f; 
        
    // Start is called before the first frame update
    void Start()
    {

        moveSpeed = 0;
        baseMoveSpeed= 8;
        speedLerp = 2.22f;


        playerActions = new InputHandler();
        playerActions.Player.Enable();
        rigidBody.freezeRotation = true;
        isGrappling = false;
        canJump = true;

        //animation setting
        //_hasAnimator = TryGetComponent(out _animator);
        _animator = transform.Find("AriRig").gameObject.GetComponent<Animator>();
         AssignAnimationIDs();

         currentTime = maxTime;

        GameObject orientationREF = GameObject.Find("Orientation");
        orientation = orientationREF.gameObject.GetComponent<Transform>();
        

        //Coin Counter
        coinCounterREF = GameObject.Find("CoinCounter");
        coinCountText = coinCounterREF.GetComponent<TextMeshProUGUI>();
        //Change/remove this line later based on level-to-level gameplay
        coinCount = 0;

        skateShoes = GameObject.FindGameObjectsWithTag("SkateBody");
        skateWheels = GameObject.FindGameObjectsWithTag("SkateWheel");
        ariCollider = GetComponent<CapsuleCollider>();
        WalkToggleHelper();
    }

    // Update is called once per frame
    void Update()
    {

        //Grounded Check
        Grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        PlayerInput();
        SpeedControl();

        //Add function for increasing player speed when keys held down
        
    

        //Drag
        if (Grounded == true)
        {
            
            rigidBody.drag = groundDrag;

            if (_hasAnimator)
            {
                _animator.SetBool(_animIDGrounded, Grounded);
            }
        } else {
            rigidBody.drag = 0;
        }

        //Hold Jump WIP
        if (jumpDelayRunning == false)
        {
            if (isJumping == true && jumpTimeCounter > 0)
            {
                TapJump(jumpForce/3);
                jumpTimeCounter -= Time.deltaTime;
            } else {
                isJumping = false;
            }
        }
    }

    private float jumpTimer;
    

    public void Jump(InputAction.CallbackContext context)
    {
        if (walking) return;
        //if (isGrappling) return;
        
        if (context.started && Grounded)
        {
            isJumping = true;
            jumpTimeCounter = 0.35f;
            TapJump(jumpForce);
            StartCoroutine(JumpHoldDelay());
        }

        if (context.canceled)
        {
            isJumping = false;
        }
    }

    public void ToggleWalk(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        walking = !walking;
        WalkToggleHelper();
    }

    void WalkToggleHelper()
    {
        Debug.Log($"Walking: {walking}");

        foreach (GameObject skateWheel in skateWheels)
            skateWheel.SetActive(!walking); //if walking, turn off wheels. 

        foreach (GameObject shoe in skateShoes)
            shoe.GetComponent<MeshRenderer>().material = walking ? skatesOffMaterial : skatesOnMaterial;

        if (walking)
        {
            ariCollider.center = new Vector3(ariCollider.center.x, walkCenterY, ariCollider.center.z);
            ariCollider.height = walkHeight;
        }
        else
        {
            ariCollider.center = new Vector3(ariCollider.center.x, skateCenterY, ariCollider.center.z);
            ariCollider.height = skateHeight;
        }
    }

    IEnumerator JumpHoldDelay()
    {
        jumpDelayRunning = true;
        yield return new WaitForSeconds(0.15f);
        jumpDelayRunning = false;
    }

    // IEnumerator ChargeJump()
    // {
    //     for (jumpForce = jumpForceMin; jumpForce < jumpForceMax; jumpForce++)
    //     {
    //         yield return new WaitForSeconds(0.15f);
    //     }
    // }

    void FixedUpdate()
    {
        PlayerMovement();
    }

    

    void PlayerInput()
    {
        //horizontalInput = Input.GetAxisRaw("Horizontal");
        //verticalInput = Input.GetAxisRaw("Vertical");
        Vector2 moveInput = playerActions.Player.Move.ReadValue<Vector2>();
        horizontalInput = moveInput.x/2;
        verticalInput = moveInput.y/2;

        if (horizontalInput != 0 || verticalInput != 0)
        {
            moveKeyUp = false;
            TimerSpace();
           
        }

        if (canAccelerate)
        {
            Mathf.Lerp(moveSpeed, moveSpeed + 5, speedLerp * Time.deltaTime);
            moveSpeed += 2;
            Debug.Log("add");

        }

        if (horizontalInput == 0 && verticalInput == 0 && !moveKeyUp)
        {
            moveSpeed = baseMoveSpeed;
            canAccelerate = false;
            moveKeyUp = true;
            Debug.Log(canAccelerate);
            
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

        if (walking)
        {
            rigidBody.velocity = new Vector3(moveDirection.normalized.x * walkSpeed * 10f, rigidBody.velocity.y, moveDirection.normalized.z * walkSpeed * 10f);
            
        }
        else if (Grounded)
        {
            rigidBody.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        } 
        else 
        {
            if (isGrappling == true)
            {
                rigidBody.AddForce(moveDirection.normalized * moveSpeed * 10f * swingSpeed, ForceMode.Force);
            } else {
                rigidBody.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
            }
        }

        //adjust animator speed
        targetSpeed = moveSpeed;

        _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate);
        if (_animationBlend < 0.01f) _animationBlend = 0f;

        
        //will eventually be _animIDSpeed, _animationBlend
        
        //_animator.SetFloat(_animIDMotionSpeed, inputMagnitude);
        if (moveSpeed > 0 )
        {
            _animator.SetFloat(_animIDMotionSpeed, 0);
        }else 
        {
            _animator.SetFloat(_animIDMotionSpeed, 1);
        }

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
    }

    private void TimerSpace ()
    {
            if (currentTime <= 0)
            {
                canAccelerate = true;
                currentTime = maxTime;
                //Debug.Log(currentTime);
            }       
            else 
            {
                canAccelerate = false;
                //Debug.Log("cant acc");
                currentTime -= Time.deltaTime;
            }
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

    public void AddCoin(int coin)
    {
            coinCount = coinCount + coin;
            Debug.Log(coinCount);
            coinCountText.text = $"{coinCount}";
    }

    //Camera Switch
    public void SwitchCameraTPM(InputAction.CallbackContext context)
    {
        if (context.performed) return;
        
        if (!walking)
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
}
