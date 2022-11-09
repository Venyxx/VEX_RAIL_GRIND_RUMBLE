using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrappleHook : MonoBehaviour
{

    //References
    private LineRenderer line;
    private Transform hookTip;
    private Transform orientation;
    private Transform player;
    private GameObject playerREF;
    private Transform camTransform;
    private Camera cam;
    private GameObject grappleDetectorREF;
    private InputHandler playerActions;
    private ThirdPersonMovement _thirdPersonMovement;

    public LayerMask canGrapple;

    //Swinging
    private Vector3 swingPoint;
    private SpringJoint joint;
    private Vector3 currentGrapplePosition;

    //Tweakable Physics Values
    public float maxSwingDistance;
    // public float spring;
    // public float spring;
    // public float damper;
    // public float massScale;

    //Air Movement
    private Rigidbody rigidBody;
    private float horizontalThrustForce = 2000f;
    private float forwardThrustForce = 3000f;
    private float extendCableSpeed = 20f;

    //Cooldown Check
    private bool cooldownRunning;
    private bool canShoot;
    public bool grappleStored;
    private int maxSwings = 3;
    private int swingCount = 0;

    //Aim Prediction
    public RaycastHit predictionHit;
    private float predictionSphereCastRadius = 6f;
    //public Transform predictionPoint;

    //Check if Grapple Point is a throwable object
    private bool canPull;
    private GameObject pullableObject;
    private ThrowObject throwObjectScript;

    //Check if Grapple Point is an enemy
    private bool enemyPullTo;
    private GameObject enemyObject;

    private bool shorteningCable;
    private bool pullingObject;
    
    

    void Start()
    {
        cooldownRunning = false;
        canShoot = true;
        canPull = false;
        enemyPullTo = false;
        grappleStored = true;

        //Set References
        playerREF = this.gameObject;
        throwObjectScript = playerREF.gameObject.GetComponent<ThrowObject>();
        player = playerREF.gameObject.GetComponent<Transform>();
        rigidBody = playerREF.gameObject.GetComponent<Rigidbody>();
        line = playerREF.gameObject.GetComponent<LineRenderer>();
        grappleDetectorREF = GameObject.Find("GrappleDetector");
        _thirdPersonMovement = FindObjectOfType<ThirdPersonMovement>();

        //Maybe make this more efficient
        GameObject basicCam = GameObject.Find("BasicCam");
        camTransform = basicCam.gameObject.GetComponent<Transform>();

        GameObject mainCamREF = GameObject.Find("Main Camera");
        cam = mainCamREF.gameObject.GetComponent<Camera>();

        GameObject orientationREF = GameObject.Find("Orientation");
        orientation = orientationREF.gameObject.GetComponent<Transform>();

        GameObject hookTipREF = GameObject.Find("HookTip");
        hookTip = hookTipREF.gameObject.GetComponent<Transform>();
        
    }

    void Update()
    {

        if (playerActions == null)
        {
            playerActions = _thirdPersonMovement.playerActions;
        }

        CheckForSwingPoints();

        if (joint != null)
        {
            if (canPull == false)
            {
                AirMovement();
            } else {
                PullObject();
            }
        }
    }

    public void GrapplePull(InputAction.CallbackContext context)
    {
        if (context.started && canShoot == true && grappleStored == true && GameObject.Find("AimingCam") != null)
        {
            StartSwing();
        }

        if (context.canceled && joint != null)
        {
            StopSwing();
        }
        
        if (context.canceled && joint == null && canShoot == false && cooldownRunning == false && GameObject.Find("PickUpHeld") == null)
        {
            canShoot = true;
        }
    }

    void LateUpdate()
    {
        DrawRope();
    }

    void DrawRope()
    {
        //Don't draw line if player isn't grappling
        if (!joint)
        {
            return;
        }

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, swingPoint, Time.deltaTime * 8f);

        line.SetPosition(0, hookTip.position);
        line.SetPosition(1, swingPoint);
    }

    void StartSwing()
    {
        if (predictionHit.point == Vector3.zero)
        {
            return;
        }
            swingPoint = predictionHit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = swingPoint;

            float distanceFromPoint = Vector3.Distance(player.position, swingPoint);

            //Distance from grapple point
            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            //Other physics values
            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            line.positionCount = 2;
            currentGrapplePosition = hookTip.position;

            playerREF.gameObject.GetComponent<ThirdPersonMovement>().isGrappling = true;
    }

    void StopSwing()
    {
        //Debug.Log("StopSwing");
        line.positionCount = 0;
        playerREF.gameObject.GetComponent<ThirdPersonMovement>().isGrappling = false;
        Destroy(joint);

        //WIP Method for limiting in-air swings
        if (swingCount < maxSwings - 1)
        {
            swingCount++;
        } else {
            swingCount = 0;
            grappleStored = false;
            Debug.Log("Swings Empty");
        }

        enemyPullTo = false;
        shorteningCable = false;
    }

    IEnumerator Cooldown ()
    {
        cooldownRunning = true;
        canShoot = false;
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(1f);
            Debug.Log("Grapple Cooldown: "+ i);
        }
        canShoot = true;
        grappleStored = true;
        cooldownRunning = false;
    }

    void AirMovement()
    {
        //Enemy Grapple Logic
        if (enemyPullTo == true)
        {
            Debug.Log("Enemy Pull To");
            swingPoint = enemyObject.transform.position;
        } 
        
        Vector2 moveInput = playerActions.Player.Move.ReadValue<Vector2>();
        float horizontalInput = moveInput.x;
        float verticalInput = moveInput.y;


        //Right Force
        if (horizontalInput > 0)
        {
            rigidBody.AddForce(orientation.right * horizontalThrustForce * Time.deltaTime);
        }

        //Left Force
        if (horizontalInput < 0)
        {
            rigidBody.AddForce(-orientation.right * horizontalThrustForce * Time.deltaTime);
        }
        
        //Extend Cable
        if (verticalInput < 0)
        {
            float extendedDistanceFromPoint = Vector3.Distance(transform.position, swingPoint) + extendCableSpeed;

            joint.maxDistance = extendedDistanceFromPoint * 0.8f;
            joint.minDistance = extendedDistanceFromPoint * 0.25f;
        }

        if (shorteningCable == true)
        {
            Debug.Log("Shortening Cable");
            Vector3 directionToPoint = swingPoint - transform.position;
            rigidBody.AddForce(directionToPoint.normalized * forwardThrustForce * Time.deltaTime);

            float distanceFromPoint = Vector3.Distance(transform.position, swingPoint);

            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;
        }


    }

    public void ShortenGrappleCable(InputAction.CallbackContext context)
    {
        //Debug.Log("shorten grapple cable");
        if (joint != null)
        {
            //Debug.Log("joint is not null");
            if (canPull == false)
            {
                //Debug.Log("canpull is false");
                /*if (enemyPullTo == true)
                {
                    //Debug.Log("Enemy Pull To");
                    swingPoint = enemyObject.transform.position;
                }*/
                
                if (context.performed)
                {
                    shorteningCable = true;
                } else if (context.canceled)
                {
                    shorteningCable = false;
                }
            }
        }
    }

    void CheckForSwingPoints()
    {
        if (joint != null)
        {
            return;
        }

        //RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition); //Input.mousePosition is the final old input system line that must be purged before we can switch to new input exclusively
        //Debug.Log(Input.mousePosition);

        //mainCam.ScreenToWorldPoint(Mouse.current.position);

        RaycastHit sphereCastHit;
        Physics.SphereCast(ray, predictionSphereCastRadius, out sphereCastHit, maxSwingDistance, canGrapple);

        RaycastHit raycastHit;
        Physics.Raycast(ray, out raycastHit, maxSwingDistance, canGrapple);

        Vector3 realHitPoint;

        //Direct Hit
        if (raycastHit.point != Vector3.zero)
        {
            realHitPoint = raycastHit.point;
        } 
        //Indirect Hit
        else if (sphereCastHit.point != Vector3.zero)
        {
            realHitPoint = sphereCastHit.point;
        } 
        else 
        {
            realHitPoint = Vector3.zero;
        }

        //Aiming Reticle Code (WIP)
        // //realHitPoint found
        // if (realHitPoint != Vector3.zero)
        // {
        //     predictionPoint.gameObject.SetActive(true);
        //     predictionPoint.position = realHitPoint;
        // }
        // //realHitPoint not found
        // else 
        // {
        //     predictionPoint.gameObject.SetActive(false);
        // }

        //Check if point is a pullable object or enemy
        if (sphereCastHit.collider != null)
        {
            if (sphereCastHit.collider.gameObject.layer == LayerMask.NameToLayer("GrapplePickUp"))
            {
                canPull = true;
                enemyPullTo = false;
                pullableObject = sphereCastHit.collider.gameObject;
            } else if (sphereCastHit.collider.gameObject.layer == LayerMask.NameToLayer("Enemy")) 
            {
                enemyPullTo = true;
                canPull = false;
                enemyObject = sphereCastHit.collider.gameObject;
            } else {
                canPull = false;
                enemyPullTo = false;
            }
        }

        predictionHit = raycastHit.point == Vector3.zero ? sphereCastHit : raycastHit;

    }

    void OnCollisionEnter (Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") && grappleStored == false && cooldownRunning == false)
        {
            StartCoroutine(Cooldown());
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("GrapplePickUp") && joint != null)
        {
            grappleDetectorREF.gameObject.GetComponent<GrappleDetection>().aimPoints.Remove(collision.gameObject.transform);
            StopSwing();
            Destroy(collision.gameObject);
            throwObjectScript.SpawnHeldObject();
            
            //Temporary solution
            playerREF.gameObject.GetComponent<ThirdPersonMovement>().canJump = true;
            canShoot = false;
        }
    }

    void PullObject()
    {
        //Debug.Log("Pull Active");
        playerREF.gameObject.GetComponent<ThirdPersonMovement>().canJump = false;
        if (pullingObject)
        {
            // Vector3 directionToPoint = transform.position - swingPoint;
            // pullableObject.AddForce(directionToPoint.normalized * forwardThrustForce * Time.deltaTime);

            pullableObject.transform.position = Vector3.MoveTowards(pullableObject.transform.position, transform.position, 25f * Time.deltaTime);

            float distanceFromPoint = Vector3.Distance(transform.position, swingPoint);

            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;
            swingPoint = pullableObject.transform.position;
        }
    }

    public void PullObjectInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            pullingObject = true;
        }
        else
        {
            pullingObject = false;
        }
    }

}
