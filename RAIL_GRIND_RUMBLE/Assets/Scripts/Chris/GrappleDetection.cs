using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GrappleDetection : MonoBehaviour
{
    public GameObject aimingCamREF;
    public Transform currentAim;

    public CinemachineFreeLook cinemachineCam;

    void Start()
    {
        cinemachineCam = aimingCamREF.gameObject.GetComponent<CinemachineFreeLook>();
    }

    
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("GrappleSwing"))
        {
            currentAim = collision.gameObject.GetComponent<Transform>();
        }
    }

    public void AimSwitch()
    {
        if (currentAim != null)
        {
            aimingCamREF.gameObject.GetComponent<ThirdPersonCamera>().aimingLookAt = currentAim;
            cinemachineCam.m_LookAt = currentAim;
        }
        
    }
}
