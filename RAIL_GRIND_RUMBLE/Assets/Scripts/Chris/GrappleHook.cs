using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrappleHook : MonoBehaviour
{
    //Keys
    KeyCode swingKey = KeyCode.Mouse0;

    //References
    public LineRenderer line;
    public Transform hookTip;
    public Transform orientation;
    public Transform player;
    public GameObject playerREF;
    public Transform camTransform;
    public Camera cam;

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
    public Rigidbody rigidBody;
    public float horizontalThrustForce;
    public float forwardThrustForce;
    public float extendCableSpeed;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(swingKey))
        {
            StartSwing();
        }

        if (Input.GetKeyUp(swingKey))
        {
            StopSwing();
        }

        if (joint != null)
        {
            AirMovement();
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
        //Checks for grappleable surface
        //RaycastHit hit;
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, maxSwingDistance, canGrapple))
        {
            Debug.Log("SwingStart");
            swingPoint = hit.point;
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
    }

    void StopSwing()
    {
        Debug.Log("StopSwing");
        line.positionCount = 0;
        playerREF.gameObject.GetComponent<ThirdPersonMovement>().isGrappling = false;
        Destroy(joint);
    }

    void AirMovement()
    {
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
}
