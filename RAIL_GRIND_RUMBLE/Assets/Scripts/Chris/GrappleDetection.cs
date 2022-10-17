using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class GrappleDetection : MonoBehaviour
{
    public GameObject aimingCamREF;
    public Transform currentAim;
    public Transform[] aimPoints;
    private int aimPointCount;
    private int aimPointChoice;

    private bool canSwitch = false;

    public CinemachineFreeLook cinemachineCam;

    void Start()
    {
        cinemachineCam = aimingCamREF.gameObject.GetComponent<CinemachineFreeLook>();
        aimPointCount = 0;
        aimPointChoice = 0;
        aimPoints = new Transform[5];
    }

    
    //old input system implementation
    void Update()
    {
        /*if (canSwitch == true && Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (aimPointChoice < aimPointCount && aimPoints[aimPointChoice + 1] != null)
            {
                aimPointChoice++; 
            } else {
                aimPointChoice = 0;
            }

            currentAim = aimPoints[aimPointChoice];
            AimSwitch();
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            canSwitch = false;
            aimPointChoice = 0;
        }*/
    }

    //new input system conversion, method tied to RELEASING SHIFT for now.
    public void GrappleRelease(InputAction.CallbackContext context)
    {
        //context.canceled == Input.GetKeyUp
        //if the "context" is anything other than releasing the button/key, end the method.
        //ensures that the method doesn't get called one for a press, once every frame held, and once when released
        if (!context.canceled) return;
        
        canSwitch = false;
        aimPointChoice = 0;
    }
    
    //new input system conversion; method tied to MOUSE1 for now
    public void GrappleSwitch(InputAction.CallbackContext context)
    {
        //context.started == Input.GetKeyDown
        //if the context is anything BUT just pressing the button/key, end the method.
        //ensures that the method doesn't get called one for a press, once every frame held, and once when released
        if (!context.started) return;
        
        //this if statement could be merged with the above if canSwitch is the only condition
        //that will ever be checked. example: if(context.started && canSwitch)
        //not a mind reader so leaving it this way
        if (!canSwitch) return;
        
        if (aimPointChoice < aimPointCount && aimPoints[aimPointChoice + 1] != null)
        {
            aimPointChoice++;
        }
        else
        {
            aimPointChoice = 0;
        }

        currentAim = aimPoints[aimPointChoice];
        AimSwitch();
        
    }

    

    void OnTriggerEnter(Collider collision)
    {
        /*if (collision.gameObject.tag == "AimPoint")
        use COMPARETAG instead of == string comparison*/
        if(collision.gameObject.CompareTag("AimPoint") || collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            currentAim = collision.gameObject.GetComponent<Transform>();
            if (!Array.Exists(aimPoints, element => element == (currentAim)))
            {
                if (aimPointCount < 6)
                {
                    aimPoints[aimPointCount] = collision.gameObject.GetComponent<Transform>();
                    aimPointCount++;
                } else {
                    aimPointCount = 0;
                    aimPoints[aimPointCount] = collision.gameObject.GetComponent<Transform>();
                }
                
            }
            
        }
    }

    public void AimSwitch()
    {
        canSwitch = true;

        if (currentAim != null)
        {
            aimingCamREF.gameObject.GetComponent<ThirdPersonCamera>().aimingLookAt = currentAim;
            cinemachineCam.m_LookAt = currentAim;
        }
        
    }
}
