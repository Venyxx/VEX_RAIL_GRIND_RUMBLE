using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Animations.Rigging;

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

    //Arm Rig
    private RigBuilder ik;
    private GameObject leftArmGrappleREF;
    private GameObject rootRigREF;

    //UI
    private Image grappleMeter;
    [SerializeField] Sprite[] grappleMeterImages;
    private GameObject reticleREF;

    //Swinging
    private Vector3 swingPoint;
    private SpringJoint joint;
    private Vector3 currentGrapplePosition;
    bool isGrappling;
    bool zipRunning;

    //Tweakable Physics Values
    public float maxSwingDistance;
    // public float spring;
    // public float damper;
    // public float massScale;

    //Air Movement
    private Rigidbody rigidBody;
    private float rbDefaultMass;
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
    public bool canPull;
    public GameObject pullableObject;
    private ThrowObject throwObjectScript;

    //Check if Grapple Point is an enemy
    public bool enemyPullTo;
    public GameObject enemyObject;

    //Adjust cable length
    private bool shorteningCable;
    private bool extendingCable;

    public bool pullingObject;
    
    //Audio
    [SerializeField] private AudioClip[] grappleSounds;
    private AudioSource audioSource;

    void Start()
    {
        cooldownRunning = false;
        canShoot = true;
        canPull = false;
        enemyPullTo = false;
        grappleStored = true;
        isGrappling = false;
        zipRunning = false;
        shorteningCable = false;
        extendingCable = false;

        //Set References
        playerREF = this.gameObject;
        throwObjectScript = playerREF.gameObject.GetComponent<ThrowObject>();
        player = playerREF.gameObject.GetComponent<Transform>();
        rigidBody = playerREF.gameObject.GetComponent<Rigidbody>();
        line = playerREF.gameObject.GetComponent<LineRenderer>();
        grappleDetectorREF = GameObject.Find("GrappleDetector");
        _thirdPersonMovement = FindObjectOfType<ThirdPersonMovement>();
        audioSource = GetComponent<AudioSource>();
        
        rbDefaultMass = rigidBody.mass;

        //Maybe make this more efficient
        GameObject basicCam = GameObject.Find("BasicCam");
        camTransform = basicCam.gameObject.GetComponent<Transform>();

        GameObject mainCamREF = GameObject.Find("Main Camera");
        cam = mainCamREF.gameObject.GetComponent<Camera>();

        GameObject orientationREF = GameObject.Find("Orientation");
        orientation = orientationREF.gameObject.GetComponent<Transform>();

        GameObject hookTipREF = GameObject.Find("HookTip");
        hookTip = hookTipREF.gameObject.GetComponent<Transform>();

        GameObject grappleMeterREF = GameObject.Find("GrappleMeter");
        grappleMeter = grappleMeterREF.GetComponent<Image>();

        reticleREF = GameObject.Find("Reticle");
        reticleREF.SetActive(false);

        //Arm Rig
        rootRigREF = GameObject.Find("Root");
        leftArmGrappleREF = GameObject.Find("LeftArmGrapple");
        ik = rootRigREF.GetComponent<RigBuilder>();
        ik.enabled = false;
        
        
    }

    void Update()
    {

        if (playerActions == null)
        {
            playerActions = _thirdPersonMovement.playerActions;
        }

        //CheckForSwingPoints();

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

    //Left Click
    public void GrapplePull(InputAction.CallbackContext context)
    {
        if (context.started && canShoot == true && grappleStored == true && GameObject.Find("AimingCam") != null && isGrappling == false)
        {
            StartSwing();
            StartCoroutine(ZipRunning());
        } 
        
        if (context.performed)
        {
            if (isGrappling == true)
            {
                shorteningCable = true;
            }
        }
        
        if (context.canceled)
        {
            if (joint == null && canShoot == false && cooldownRunning == false && GameObject.Find("PickUpHeld") == null)
            {
                canShoot = true;
            }

            if (zipRunning == false)
            {
                shorteningCable = false;
            }
        }    
    }

    public void NoAimGrapple(InputAction.CallbackContext context)
    {
        if (context.started && playerREF.GetComponent<ThirdPersonMovement>().isWalking == false)
        {
            if (!GameObject.Find("AimingCam") && grappleDetectorREF.GetComponent<GrappleDetection>().aimPoints.Count > 0 && isGrappling == false && grappleStored == true)
            {
                //Need to fix ability to account for pickups/pulling to player
                CheckObjectType(grappleDetectorREF.GetComponent<GrappleDetection>().currentAim);
                StartSwing();
                StartCoroutine(ZipRunning());
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
        // if (predictionHit.point == Vector3.zero)
        // {
        //     return;
        // }

        //Sound
        audioSource.clip = grappleSounds[0];
        audioSource.Play(0);
        StartCoroutine(SoundDelay());
        

        //Insta-zip
        shorteningCable = true;
        rigidBody.mass = (rbDefaultMass/1.75f);
        isGrappling = true;

            //swingPoint = predictionHit.point;
            swingPoint = grappleDetectorREF.GetComponent<GrappleDetection>().currentAim.transform.position;
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

            //Arm Rig
            ik.enabled = true;
            leftArmGrappleREF.GetComponent<TwoBoneIKConstraint>().data.target = grappleDetectorREF.GetComponent<GrappleDetection>().currentAim;

            playerREF.gameObject.GetComponent<ThirdPersonMovement>().isGrappling = true;
    }

    void StopSwing()
    {
        //Arm Rig
        ik.enabled = false;

        //Sound
        audioSource.clip = grappleSounds[4];
        audioSource.Play(0);

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
            grappleMeter.sprite = grappleMeterImages[3];
            Debug.Log("Swings Empty");
        }

        //UI GrappleMeter
        if (swingCount == 1)
        {
            grappleMeter.sprite = grappleMeterImages[1];
        } else if (swingCount == 2)
        {
            grappleMeter.sprite = grappleMeterImages[2];
        }

        
        enemyPullTo = false;
        shorteningCable = false;
        rigidBody.mass = rbDefaultMass;
        isGrappling = false;
    }

    IEnumerator Cooldown ()
    {
        cooldownRunning = true;
        canShoot = false;
        for (int i = 3; i > 0; i--)
        {
            yield return new WaitForSeconds(1f);
            Debug.Log("Grapple Cooldown: "+ i);
            grappleMeter.sprite = grappleMeterImages[i - 1];
        }
        canShoot = true;
        grappleStored = true;
        cooldownRunning = false;
    }

    IEnumerator ZipRunning()
    {
        zipRunning = true;
        yield return new WaitForSeconds(0.8f);
        zipRunning = false;
    }

    IEnumerator SoundDelay()
    {
        yield return new WaitForSeconds(0.2f);
        ZipSound();
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
        //if (verticalInput < 0)
        if (extendingCable == true)
        {
            float extendedDistanceFromPoint = Vector3.Distance(transform.position, swingPoint) + extendCableSpeed;

            joint.maxDistance = extendedDistanceFromPoint * 0.8f;
            joint.minDistance = extendedDistanceFromPoint * 0.25f;
        }

        //Shorten Cable
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

    //Space Bar
    public void ShortenGrappleCable(InputAction.CallbackContext context)
    {
        //Debug.Log("shorten grapple cable");
        if (joint != null)
        {
            // if (context.started)
            // {
            //     if (shorteningCable == true)
            //     {
            //         shorteningCable = false;
            //     } 
            // }

            // if (context.performed)
            // {
            //     shorteningCable = true;
            // } else if (context.canceled)
            // {
            //     shorteningCable = false;
            // }

            if (context.started)
            {
                StopSwing();
            }
        }
    }

    //Left Shift
    public void ReleaseAim(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (joint != null)
            {
                extendingCable = true;
                shorteningCable = false;
            } else if (grappleDetectorREF.GetComponent<GrappleDetection>().aimPoints.Count > 0){
                CheckObjectType(grappleDetectorREF.GetComponent<GrappleDetection>().currentAim);
                reticleREF.SetActive(true);
            }
        }

        if (context.performed)
        {
            shorteningCable = false;
        }

        if (context.canceled)
        {
            if (joint == null)
            {
                canPull = false;
                enemyPullTo = false;
            }
            reticleREF.SetActive(false);
            extendingCable = false;
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

        //Check if point is a pullable object or enemy
        // if (sphereCastHit.collider != null)
        // {
        //     if (sphereCastHit.collider.gameObject.layer == LayerMask.NameToLayer("GrapplePickUp"))
        //     {
        //         canPull = true;
        //         enemyPullTo = false;
        //         pullableObject = sphereCastHit.collider.gameObject;
        //         pullingObject = true;
        //     } else if (sphereCastHit.collider.gameObject.layer == LayerMask.NameToLayer("Enemy")) 
        //     {
        //         enemyPullTo = true;
        //         canPull = false;
        //         enemyObject = sphereCastHit.collider.gameObject;
        //     } else {
        //         canPull = false;
        //         enemyPullTo = false;
        //     }
        // }

        predictionHit = raycastHit.point == Vector3.zero ? sphereCastHit : raycastHit;

    }

    public void CheckObjectType(Transform currentAim)
    {
        if (currentAim == null)
        {
            canPull = false;
            enemyPullTo = false;
        } else {

            // if (!GameObject.Find("AimingCam"))
            // {
            // return;
            // }

            if (currentAim.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                enemyPullTo = true;
                canPull = false;
                enemyObject = currentAim.gameObject;
                pullingObject = false;
            } else if (currentAim.gameObject.layer == LayerMask.NameToLayer("GrapplePickUp")){
                canPull = true;
                enemyPullTo = false;
                pullableObject = currentAim.gameObject;
                pullingObject = true;
            } else {
                canPull = false;
                enemyPullTo = false;
                pullingObject = false;
            }
        }
        
    }

    void OnCollisionEnter (Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") && grappleStored == false && cooldownRunning == false)
        {
            StartCoroutine(Cooldown());
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("GrapplePickUp") && joint != null)
        {
            //Camera
            if (GameObject.Find("AimingCam"))
            {
                GameObject aimingCamREF = GameObject.Find("AimingCam");
                aimingCamREF.GetComponent<ThirdPersonCamera>().SwitchCameraCanceled();
            }
            
            pullingObject = false;
            grappleDetectorREF.gameObject.GetComponent<GrappleDetection>().RemovePoint(collision.transform);
            StopSwing();
            Destroy(collision.gameObject);
            throwObjectScript.SpawnHeldObject();
            
            canShoot = false;
        }

        if (collision.gameObject.tag == "AimPoint")
        {
            if (shorteningCable == true)
            {
                StopSwing();
            }
        }
    }

    void PullObject()
    {
        //Debug.Log("Pull Active");
        //playerREF.gameObject.GetComponent<ThirdPersonMovement>().canJump = false;
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
        if (context.canceled)
        {
            pullingObject = false;
        }
    }

    void ZipSound()
    {
        if (canPull == false)
        {
            int pickSound = Random.Range(1,3);
            audioSource.clip = grappleSounds[pickSound];
            audioSource.Play(0);
        } else {
            audioSource.clip = grappleSounds[3];
            audioSource.Play(0);
        }
    }

}
