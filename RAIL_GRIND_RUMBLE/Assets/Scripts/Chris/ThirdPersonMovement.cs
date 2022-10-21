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
    public bool canJump;

    //Keys
    KeyCode jump = KeyCode.Space;

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

        GameObject orientationREF = GameObject.Find("Orientation");
        orientation = orientationREF.gameObject.GetComponent<Transform>();
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
        
        //old input system version
        if (Input.GetKey(KeyCode.W) && canAccelerate == true)
        {
            Mathf.Lerp(moveSpeed, moveSpeed + 5, speedLerp * Time.deltaTime);
            moveSpeed += 5;
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            // was commented out for longer speed decrease time -V
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

    //new input system version, tied to W for now
    /*public void Move(InputAction.CallbackContext context)
    {
        //if held, end method
        if (context.performed) return;
        
        //if button pressed this frame, do stuff
        if (context.started && canAccelerate)
        {
            Debug.Log("did we make it");
            Mathf.Lerp(moveSpeed, moveSpeed + 5, speedLerp * Time.deltaTime);
            moveSpeed += 5;
        }
        //else if button released this frame, do other stuff
        else if (context.canceled)
        {
            moveSpeed = baseMoveSpeed;
        }
    }
*/
    void FixedUpdate()
    {
        PlayerMovement();
    }

    void PlayerInput()
    {
    
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        
        if (Input.GetKey(jump) && canJump == true && grounded == true)
        {
            //Debug.Log("Jump");
            canJump = false;

            Jump();

            //Invoke(nameof(ResetJump), jumpCoolDown);
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
        
        if (Input.GetKeyUp(KeyCode.W))
        {
            //Debug.Log("lerp speed down");
            //rigidBody.velocity = Vector3.Lerp(rigidBody.velocity, standingStill, speedLerp * Time.deltaTime);
            rigidBody.velocity -= 0.1f * rigidBody.velocity;
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
}
