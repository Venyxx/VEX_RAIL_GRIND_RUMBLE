using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private float rotationSpeed = 7f;

    //Aiming
    public GameObject basicCamREF;
    public GameObject aimingCamREF;
    public GameObject reticleREF;

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
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        playerREF = GameObject.Find("PlayerObject");
        playerTransform = playerREF.gameObject.GetComponent<Transform>();
        playerPrefabREF = GameObject.Find("playerPrefab");
        player = playerPrefabREF.gameObject.GetComponent<Transform>();
        rigidBody = playerPrefabREF.gameObject.GetComponent<Rigidbody>();
        GameObject orientationREF = GameObject.Find("Orientation");
        orientation = orientationREF.gameObject.GetComponent<Transform>();
        grappleDetection = GameObject.Find("GrappleDetector");

        // GameObject mainCamREF = GameObject.Find("Main Camera");
        // cam = mainCamREF.gameObject.GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        //Aiming

        //Add reference to Grapple Hook Script so you can only aim after cooldown
        if (Input.GetKeyDown(KeyCode.LeftShift) && grappleDetection.gameObject.GetComponent<GrappleDetection>().aimPoints.Count != 0 && playerPrefabREF.gameObject.GetComponent<GrappleHook>().grappleStored == true)
        {
            SwitchCameraStyle(CameraStyle.Aiming);

            grappleDetection.gameObject.GetComponent<GrappleDetection>().AimSwitch();
            //grappleDetection.gameObject.GetComponent<GrappleDetection>().aimPointChoice = 0;

            if (playerPrefabREF.gameObject.GetComponent<ThirdPersonMovement>().grounded == false)
            {
                Time.timeScale = 0.3f;
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            SwitchCameraStyle(CameraStyle.Basic);
            Time.timeScale = 1f;
        }


        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        //if (direction.magnitude >= 0.1f)
        //{
            //Rotate Orientation
            Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
            orientation.forward = viewDir.normalized;

            if (currentStyle == CameraStyle.Basic)
            {
                //Rotate Player Object
                Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

                if (inputDir != Vector3.zero)
                {
                    playerTransform.forward = Vector3.Slerp(playerTransform.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
                }
            } else if (currentStyle == CameraStyle.Aiming) {
                Vector3 dirToAimingLookAt = aimingLookAt.position - new Vector3(transform.position.x, aimingLookAt.position.y, transform.position.z);
                orientation.forward = dirToAimingLookAt.normalized;

                playerTransform.forward = dirToAimingLookAt.normalized;
            }
            
        //}
        
    }

    void SwitchCameraStyle(CameraStyle newStyle)
    {
        //Turn off all existing cams
        aimingCamREF.SetActive(false);
        basicCamREF.SetActive(false);
        reticleREF.SetActive(false);

        //Switch to alternate angle
        if (newStyle == CameraStyle.Basic)
        {
            basicCamREF.SetActive(true);
            reticleREF.SetActive(false);
        }

        if (newStyle == CameraStyle.Aiming)
        {
            aimingCamREF.SetActive(true);
            reticleREF.SetActive(true);
        }



        currentStyle = newStyle;
    }

}
