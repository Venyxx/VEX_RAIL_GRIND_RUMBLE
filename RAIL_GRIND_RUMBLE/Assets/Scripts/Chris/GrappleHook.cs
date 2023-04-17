using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Animations.Rigging;
using Cinemachine;


public class GrappleHook : MonoBehaviour
{

    //Arm Rig //////////////////////////////////////////////////////////////////////////////////// uncomment when troubleshooting - Raul

    //private RigBuilder pointToGrapplePoint;
    private GameObject rootRigREF;
    private GameObject leftArmGrappleREF;
   

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////




    

    //References
    private LineRenderer line;
    private Transform hookTip;
    private Transform orientation;
    private Transform player;
    private GameObject playerREF;
    private Transform camTransform;
    private Camera cam;
    private GrappleDetection grappleDetector;
    private InputHandler playerActions;
    private ThirdPersonMovement _thirdPersonMovement;
    public Animator _animator;
    [SerializeField] private CinemachineFreeLook mainGameCam;
    
    
    public LayerMask canGrapple;


    //UI
    private Image grappleMeter;
    [SerializeField] Sprite[] grappleMeterImages;
    //private GameObject reticleREF;

    //Swinging
    private Vector3 swingPoint;
    private SpringJoint joint;
    private Vector3 currentGrapplePosition;
   public bool isGrappling;
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
    public int maxSwings = 3;
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

        //Fixes launch on first grapple
        line.positionCount = 0;

        //GameObject grappleDetectorREF = GameObject.Find("GrappleDetector");
        grappleDetector = this.transform.Find("GrappleDetector").GetComponent<GrappleDetection>(); 
        _thirdPersonMovement = FindObjectOfType<ThirdPersonMovement>();
        audioSource = GetComponent<AudioSource>();
        
        rbDefaultMass = rigidBody.mass;

        
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

        //Arm rig uncomment when troubleshooting -Raul 
        // rootRigREF = GameObject.Find("mixamorig:LeftShoulder");
        leftArmGrappleREF = GameObject.Find("LeftArmGrapple");
        // pointToGrapplePoint = rootRigREF.GetComponent<RigBuilder>();
        // pointToGrapplePoint.enabled = false;

        
        
        
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
        if (context.started && canShoot == true && grappleStored == true && GameObject.Find("AimingCam") != null && isGrappling == false && !_thirdPersonMovement.dialogueManager.freezePlayer && ProgressionManager.Get().grappleUnlocked == true)
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
        if (context.started && playerREF.GetComponent<ThirdPersonMovement>().isWalking == false && ProgressionManager.Get().grappleUnlocked == true)
        {
            if (!GameObject.Find("AimingCam") && grappleDetector.aimPoints.Count > 0 && isGrappling == false && grappleStored == true)
            {
                //Need to fix ability to account for pickups/pulling to player
                CheckObjectType(grappleDetector.currentAim);
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

        //Make sure all return values come first so nothing else activates if any of these conditions are true

        if (grappleDetector.currentAim.gameObject.layer == LayerMask.NameToLayer("Enemy")) return;
        if (GetComponent<ThrowObject>().isHoldingObject == true) return;
        if (cooldownRunning == true) return;

        //swing animation uncomment when troubleshooting - Raul
        if(pullingObject == false && throwObjectScript.isHoldingObject == false)
        {
            _animator.SetBool("isGrappling", true);
        }

        //Switch back to normal camera / resumes time
        if (GameObject.Find("AimingCam"))
        {
            GameObject aimingCamREF = GameObject.Find("AimingCam");
            aimingCamREF.GetComponent<ThirdPersonCamera>().SwitchCameraCanceled();
        }

        //Sound
        audioSource.clip = grappleSounds[0];
        audioSource.Play(0);
        StartCoroutine(SoundDelay());
        

        //Insta-zip
        shorteningCable = true;
        rigidBody.mass = (rbDefaultMass/2.75f);
        isGrappling = true;

        //swing animation uncomment when troubleshooting - Raul
        //_animator.SetBool("isGrappling", true); 

            //swingPoint = predictionHit.point;
            swingPoint = grappleDetector.currentAim.transform.position;
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


        //Check for plug in Phase 1 of Donovan Fight
        if (grappleDetector.currentAim.gameObject.tag == "DP1Plug")
        {
            Phase1Plug plug = grappleDetector.currentAim.GetComponent<Phase1Plug>();
            plug.StartCoroutine(plug.Grappled());
        }

        playerREF.gameObject.GetComponent<ThirdPersonMovement>().isGrappling = true;

        //Arm Rig uncomment when troubleshooting - Raul
        // pointToGrapplePoint.enabled = true;
        // leftArmGrappleREF.GetComponent<TwoBoneIKConstraint>().data.target = grappleDetector.GetComponent<GrappleDetection>().currentAim;
    }

    public void StopSwing()
    {
        //Sound
        audioSource.clip = grappleSounds[4];
        audioSource.Play(0);

        //Debug.Log("StopSwing");
        line.positionCount = 0;
        playerREF.gameObject.GetComponent<ThirdPersonMovement>().isGrappling = false;
        Destroy(joint);

        //swing animation uncomment when troubleshooting - Raul
        _animator.SetBool("isGrappling", false);

        //Arm Rig uncomment when troubleshooting - Raul
        //pointToGrapplePoint.enabled = false;

        //WIP Method for limiting in-air swings
        if (!SettingsManager.godMode)
        {
            if (swingCount < maxSwings - 1)
        {
            swingCount++;
        } else {
            swingCount = 3;
            grappleStored = false;
            Debug.Log("Swings Empty");
        }   
        }
        

        //UI GrappleMeter
        if (swingCount == 1)
        {
            grappleMeter.sprite = grappleMeterImages[1];
        } else if (swingCount == 2)
        {
            grappleMeter.sprite = grappleMeterImages[2];
        } else if (swingCount == 3)
        {
            grappleMeter.sprite = grappleMeterImages[3];
        }

        
        enemyPullTo = false;
        shorteningCable = false;
        rigidBody.mass = rbDefaultMass;
        isGrappling = false;

        //solution for aririg object moving slightly after grapple
        //Debug.Log(aririg.transform.position);
        //var aririg = GameObject.Find("AriRig").transform;
        //Vector3 newPos = new Vector3 (0f,0f,0f);
        //aririg.transform.position = newPos;
        
    }

    IEnumerator Cooldown ()
    {
        cooldownRunning = true;
        canShoot = false;
        //grappleStored = false;
        for (int i = swingCount; i > 0; i--)
        {
            yield return new WaitForSeconds(1f);
            Debug.Log("Grapple Cooldown: "+ i);
            grappleMeter.sprite = grappleMeterImages[i - 1];
        }
        swingCount = 0;
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
            } else if (grappleDetector.aimPoints.Count > 0){
                CheckObjectType(grappleDetector.currentAim);
                //reticleREF.SetActive(true);
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
            //reticleREF.SetActive(false);
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

            /*if (currentAim.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                enemyPullTo = true;
                canPull = false;
                enemyObject = currentAim.gameObject;
                pullingObject = false;
            } else */
            
            if (currentAim.gameObject.layer == LayerMask.NameToLayer("GrapplePickUp")){
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
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") && isGrappling == false && cooldownRunning == false) //&& grappleStored == false 
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
            grappleDetector.RemovePoint(collision.transform);
            StopSwing();

            //Check for held object type
            if (collision.gameObject.GetComponent<Drone>() != null)
            {
                throwObjectScript.SpawnHeldObject("Drone");
            } else {
                throwObjectScript.SpawnHeldObject("Trashcan");
            }

            Destroy(collision.gameObject);
        
            
            //canShoot = false;
        }

        if (collision.gameObject.tag == "AimPoint")
        {
            if (shorteningCable == true)
            {
                StopSwing();
            }
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") && isGrappling == false && cooldownRunning == false && swingCount <= maxSwings) //&& grappleStored == false 
        {
            StartCoroutine(Cooldown());
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
