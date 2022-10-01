using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonMovement : MonoBehaviour
{
    //Movement
    public float moveSpeed;
    public float baseMoveSpeed;
    public float speedLerp;
    public float groundDrag;
     Vector3 standingStill = new Vector3 (0,0,0);

    public float jumpForce;
    public float jumpCoolDown;
    public float airMultiplier;
    bool canJump;

    //Keys
    KeyCode jump = KeyCode.Space;

    //Grounded Check
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;

    //Grapple Check
    public bool isGrappling;
    public float swingSpeed;

    public Transform orientation;

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


    public Rigidbody rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody.freezeRotation = true;
        isGrappling = false;
        canJump = true;
        _hasAnimator = TryGetComponent(out _animator);
        //_input = GetComponent<StarterAssetsInputs>();
        //_playerInput = GetComponent<PlayerInput>();
         AssignAnimationIDs();

         currentTime = maxTime;
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
        if (Input.GetKey(KeyCode.W) && canAccelerate == true)
        {
            Mathf.Lerp(moveSpeed, moveSpeed + 5, speedLerp * Time.deltaTime);
            moveSpeed += 5;
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            moveSpeed = baseMoveSpeed;
        }


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
    }

    void FixedUpdate()
    {
        PlayerMovement();
    }

    void PlayerInput()
    {
        //VS i changed these from getaxisraw to getaxis to induce smoothing
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jump) && canJump == true && grounded == true)
        {
            //Debug.Log("Jump");
            canJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCoolDown);
        }
    }

    void PlayerMovement()
    {
        //Uncomment once air movement is programmed
        // if (isGrappling == true)
        // {
        //     return;
        // }
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
            //rigidBody.velocity = Vector3.Lerp(flatVel, limitedVel, speedLerp * Time.deltaTime); testing if i could lerp instead
            
        }
        
        if (Input.GetKeyUp(KeyCode.W))
        {
            Debug.Log("lerp speed down");
            rigidBody.velocity = Vector3.Lerp(rigidBody.velocity, standingStill, speedLerp * Time.deltaTime);
        }
    }

    void Jump()
    {
        rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z);

        rigidBody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
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
}
