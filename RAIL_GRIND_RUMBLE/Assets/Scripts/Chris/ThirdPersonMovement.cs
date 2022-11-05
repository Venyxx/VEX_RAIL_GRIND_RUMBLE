using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class ThirdPersonMovement : MonoBehaviour
{
    //Movement
    public float moveSpeed;
    public float baseMoveSpeed;
    public float speedLerp;
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
    public bool grounded;

    //Grapple Check
    public bool isGrappling;
    public float swingSpeed;

    private Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;



    // animation IDs
    private Animator _animator;
    private bool _hasAnimator;
    private int _animIDSpeed;
    private int _animIDGrounded;
    private int _animIDJump;
    private int _animIDFreeFall;
    private int _animIDMotionSpeed;


    //Acceleration Timer
    private float maxTime = 3.0f;
    private float currentTime;
    private bool canAccelerate;

    public InputHandler playerActions { get; private set; }
    private bool moveKeyUp;

    private bool jump;
    private bool jumpCancel;


    public Rigidbody rigidBody;

    //Coins
    public static int coinCount;
    GameObject coinCounterREF;
    TextMeshProUGUI coinCountText;

    // Start is called before the first frame update
    void Start()
    {
        playerActions = new InputHandler();
        playerActions.Player.Enable();
        rigidBody.freezeRotation = true;
        isGrappling = false;
        canJump = true;
        _hasAnimator = TryGetComponent(out _animator);
        //_input = GetComponent<StarterAssetsInputs>();
        //_playerInput = GetComponent<PlayerInput>();
         AssignAnimationIDs();

         currentTime = maxTime;

        GameObject orientationREF = GameObject.Find("Orientation");
        orientation = orientationREF.gameObject.GetComponent<Transform>();
        

        //Coin Counter
        coinCounterREF = GameObject.Find("CoinCounter");
        coinCountText = coinCounterREF.GetComponent<TextMeshProUGUI>();
        //Change/remove this line later based on level-to-level gameplay
        coinCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Grounded Check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        PlayerInput();
        SpeedControl();

        //Add function for increasing player speed when keys held down
        TimerSpace();


        //Drag
        if (grounded == true)
        {
            
            rigidBody.drag = groundDrag;

            if (_hasAnimator)
            {
                //_animator.SetBool(_animIDGrounded, Grounded);
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
        if (context.started && grounded)
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

    IEnumerator JumpHoldDelay()
    {
        jumpDelayRunning = true;
        for (int i = 0; i < 1; i++)
        {
            yield return new WaitForSeconds(0.15f);
        }
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
        }

        if (canAccelerate)
        {
            Mathf.Lerp(moveSpeed, moveSpeed + 5, speedLerp * Time.deltaTime);
            moveSpeed += 5;
        }

        if (horizontalInput == 0 && verticalInput == 0 && !moveKeyUp)
        {
            moveSpeed = baseMoveSpeed;
            moveKeyUp = true;
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

        if (grounded == true)
        {
            rigidBody.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        } else if (grounded == false)
        {
            if (isGrappling == true)
            {
                rigidBody.AddForce(moveDirection.normalized * moveSpeed * 10f * swingSpeed, ForceMode.Force);
            } else {
                rigidBody.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
            }

            
            
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
}
