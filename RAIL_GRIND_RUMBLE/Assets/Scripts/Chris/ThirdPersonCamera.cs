using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    //References
    public Transform orientation;
    public Transform player;
    public GameObject playerREF;
    public GameObject playerPrefabREF;
    private Transform playerTransform;
    public Transform cam;
    public Rigidbody rigidBody;
    public GameObject grappleDetection;

    public float rotationSpeed;

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

        playerTransform = playerREF.gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //Aiming
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            SwitchCameraStyle(CameraStyle.Aiming);

            grappleDetection.gameObject.GetComponent<GrappleDetection>().AimSwitch();

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
