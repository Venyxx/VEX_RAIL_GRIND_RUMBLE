using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class ThirdPersonCamera : MonoBehaviour
{
    //References
    private Transform orientation;
    private Transform player;
    private GameObject playerREF;
    private GameObject playerPrefabREF;
    private Transform playerTransform;
    public Transform cam;
    private Rigidbody rigidBody;
    private GameObject grappleDetection;
    private InputHandler playerActions;
    private ThirdPersonMovement _thirdPersonMovement;
    private CinemachineFreeLook _freeLook;
    private DialogueManager dialogueManager;
    private float rotationSpeed = 7f;
    

    //Aiming
    public GameObject basicCamREF;
    public GameObject aimingCamREF;
    //public GameObject reticleREF;

    public CameraStyle currentStyle;

    public Transform aimingLookAt;

    public enum CameraStyle
    {
        Basic,
        Aiming
    }


    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        playerREF = GameObject.FindWithTag("PlayerObject");
        playerPrefabREF = GameObject.Find("playerPrefab");
        playerTransform = playerREF.gameObject.transform;

        player = playerPrefabREF.transform;
        rigidBody = playerPrefabREF.gameObject.GetComponent<Rigidbody>();
        GameObject orientationREF = GameObject.Find("Orientation");
        orientation = orientationREF.gameObject.GetComponent<Transform>();
        grappleDetection = GameObject.Find("GrappleDetector");
        _thirdPersonMovement = playerPrefabREF.GetComponent<ThirdPersonMovement>();
        GameObject basicCam = GameObject.Find("BasicCam");
        if(basicCam!=null)
            _freeLook = basicCam.GetComponent<CinemachineFreeLook>();
        GameObject aimingCam = GameObject.Find("AimingCam");
        if(aimingCam != null)
        {
             _freeLook = aimingCam.GetComponent<CinemachineFreeLook>();
             _freeLook.LookAt = aimingLookAt;
        } else {
            _freeLook.LookAt = player;
        }

        //v hot fix for rotatation lock on load
        currentStyle = CameraStyle.Aiming;
        currentStyle = CameraStyle.Basic;
           
        
        _freeLook.Follow = player;
        

        // GameObject mainCamREF = GameObject.Find("Main Camera");
        // cam = mainCamREF.gameObject.GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(_thirdPersonMovement == null);
        //not assigning on start to avoid NullReferenceException since it is
        //instantiated/created on start in another class (ThirdPersonMovement)
        if (playerActions == null)
        {
            playerActions = _thirdPersonMovement.playerActions;
        }

        Vector2 moveInput = new Vector2(0, 0);
        if (!dialogueManager.freezePlayer)
        {
            moveInput = playerActions.Player.Move.ReadValue<Vector2>();
        }
        float horizontalInput = moveInput.x;
        float verticalInput = moveInput.y;

            //Rotate Orientation
            Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
            
            orientation.forward = viewDir.normalized;

            if (currentStyle == CameraStyle.Basic)
            {
               //Rotate Player Object
                Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

                if (inputDir != Vector3.zero && !_thirdPersonMovement.isBraking)
                {
                    playerTransform.forward = Vector3.Slerp(playerTransform.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
                }
                /*else if (inputDir == Vector3.zero && playerPrefabREF.GetComponent<Rigidbody>().velocity.magnitude > 1) // added so that you can control direction with RTS while sliding
                {   
                   playerTransform.forward = Vector3.Slerp(playerTransform.forward, viewDir.normalized, Time.deltaTime * rotationSpeed);
                }*/
                
            } else if (currentStyle == CameraStyle.Aiming) {
                if (transform != null)
                {
                    Vector3 dirToAimingLookAt = aimingLookAt.position - new Vector3(transform.position.x, aimingLookAt.position.y, transform.position.z);
                    orientation.forward = dirToAimingLookAt.normalized;
                    playerTransform.forward = dirToAimingLookAt.normalized;
                }
                
            }


            
           
            
            
        
        }

    public void Look(InputAction.CallbackContext context)
    {
        _freeLook.m_XAxis.m_InputAxisValue = context.ReadValue<Vector2>().x;
        _freeLook.m_YAxis.m_InputAxisValue = context.ReadValue<Vector2>().y; 
    }

    public void SwitchCameraStarted()
    {
        Debug.Log ("Aim Mode Input detected");

        if (ProgressionManager.Get().grappleUnlocked == false) return;

        //Aiming
        if (grappleDetection.gameObject.GetComponent<GrappleDetection>().aimPoints.Count != 0 && playerPrefabREF.gameObject.GetComponent<GrappleHook>().grappleStored)
        {
            SwitchCameraStyle(CameraStyle.Aiming);
            //grappleDetection.gameObject.GetComponent<GrappleDetection>().SetCurrentAim();
            grappleDetection.gameObject.GetComponent<GrappleDetection>().AimSwitch();

            //if (playerPrefabREF.gameObject.GetComponent<ThirdPersonMovement>().Grounded == false)
            //{
                Time.timeScale = 0.3f;
            //}
        }
    }

    public void SwitchCameraCanceled()
    {
        SwitchCameraStyle(CameraStyle.Basic);
        Time.timeScale = 1f;
    }

    void SwitchCameraStyle(CameraStyle newStyle)
    {
        //Turn off all existing cams
        aimingCamREF.SetActive(false);
        basicCamREF.SetActive(false);
        //reticleREF.SetActive(false);

        //Switch to alternate angle
        if (newStyle == CameraStyle.Basic)
        {
            basicCamREF.SetActive(true);
            //reticleREF.SetActive(false);
        }

        if (newStyle == CameraStyle.Aiming)
        {
            aimingCamREF.SetActive(true);
            //reticleREF.SetActive(true);
        }
        
        currentStyle = newStyle;
    }


     
}
