using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Cinemachine;

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

    
    void Update()
    {
        if (canSwitch = true && Input.GetKeyDown(KeyCode.Mouse1))
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
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "AimPoint")
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
