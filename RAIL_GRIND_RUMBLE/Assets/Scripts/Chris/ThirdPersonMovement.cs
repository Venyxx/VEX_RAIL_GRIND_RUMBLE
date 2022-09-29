using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    //Movement
    public float moveSpeed;
    public float groundDrag;

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

    public Rigidbody rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody.freezeRotation = true;
        isGrappling = false;
        canJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Grounded Check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        PlayerInput();
        SpeedControl();

        //Add function for increasing player speed when keys held down

        //Drag
        if (grounded == true)
        {
            rigidBody.drag = groundDrag;
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
}
