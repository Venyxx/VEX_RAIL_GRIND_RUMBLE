using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrappleHook : MonoBehaviour
{
    //Keys
    KeyCode swingKey = KeyCode.Mouse0;

    //References
    private LineRenderer line;
    public Transform hookTip;
    public Transform orientation;
    private Transform player;
    private GameObject playerREF;
    public Transform camTransform;
    public Camera cam;
    [SerializeField] GameObject grappleDetectorREF;

    public LayerMask canGrapple;

    //Swinging
    private Vector3 swingPoint;
    private SpringJoint joint;
    private Vector3 currentGrapplePosition;

    //Tweakable Physics Values
    public float maxSwingDistance;
    // public float spring;
    // public float damper;
    // public float massScale;

    //Air Movement
    private Rigidbody rigidBody;
    private float horizontalThrustForce = 2000f;
    private float forwardThrustForce = 3000f;
    private float extendCableSpeed = 20f;

    //Cooldown Check
    private bool canShoot;
    private bool grappleStored;
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

    void Start()
    {
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
    }

    void Update()
    {
        if (Input.GetKeyDown(swingKey) && canShoot == true && grappleStored == true)
        {
            StartSwing();
        }

        if (Input.GetKeyUp(swingKey) && joint != null)
        {
            StopSwing();
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

        //Checks for grappleable surface
        //RaycastHit hit;
        //Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        //if (Physics.Raycast(ray, out hit, maxSwingDistance, canGrapple))
        //{
            Debug.Log("SwingStart");
            //swingPoint = hit.point;
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

        //}
    }

    void StopSwing()
    {
        Debug.Log("StopSwing");
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
    }

    IEnumerator Cooldown ()
    {
        canShoot = false;
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(1f);
            Debug.Log("Grapple Cooldown: "+ i);
        }
        canShoot = true;
        grappleStored = true;
    }

    void AirMovement()
    {
        //Enemy Grapple Logic
        if (enemyPullTo == true)
        {
            Debug.Log("Enemy Pull To");
            swingPoint = enemyObject.transform.position;
        }


        //Right Force
        if (Input.GetKey(KeyCode.D))
        {
            rigidBody.AddForce(orientation.right * horizontalThrustForce * Time.deltaTime);
        }

        //Left Force
        if (Input.GetKey(KeyCode.A))
        {
            rigidBody.AddForce(-orientation.right * horizontalThrustForce * Time.deltaTime);
        }

        //Shorten Grapple Cable
        if (Input.GetKey(KeyCode.Space))
        {
            Vector3 directionToPoint = swingPoint - transform.position;
            rigidBody.AddForce(directionToPoint.normalized * forwardThrustForce * Time.deltaTime);

            float distanceFromPoint = Vector3.Distance(transform.position, swingPoint);

            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;
        }

        //Extend Cable
        if (Input.GetKey(KeyCode.S))
        {
            float extendedDistanceFromPoint = Vector3.Distance(transform.position, swingPoint) + extendCableSpeed;

            joint.maxDistance = extendedDistanceFromPoint * 0.8f;
            joint.minDistance = extendedDistanceFromPoint * 0.25f;
        }

        
    }

    void CheckForSwingPoints()
    {
        if (joint != null)
        {
            return;
        }

        //RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

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
                //enemyPullTo = false;
            }
        }

        predictionHit = raycastHit.point == Vector3.zero ? sphereCastHit : raycastHit;

    }

    void OnCollisionEnter (Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") && grappleStored == false)
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
        }
    }

    void PullObject()
    {
        Debug.Log("Pull Active");
        playerREF.gameObject.GetComponent<ThirdPersonMovement>().canJump = false;
        if (Input.GetKey(KeyCode.Space))
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

}
