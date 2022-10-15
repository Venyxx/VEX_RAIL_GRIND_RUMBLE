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
    public LayerMask canPull;

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

    //Cooldown Check
    private bool canShoot;

    //Aim Prediction
    public RaycastHit predictionHit;
    public float predictionSphereCastRadius;
    //public Transform predictionPoint;

    void Start()
    {
        canShoot = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(swingKey) && canShoot == true)
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
    }

    IEnumerator Cooldown ()
    {
        canShoot = false;
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(1f);
            //Debug.Log("Grapple Cooldown: "+ i);
        }
        canShoot = true;
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

        predictionHit = raycastHit.point == Vector3.zero ? sphereCastHit : raycastHit;

    }

    void OnCollisionEnter (Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            StartCoroutine(Cooldown());
        }
    }


}
