using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class GrappleDetection : MonoBehaviour
{
    public GameObject aimingCamREF;
    public Transform currentAim;
    public List<Transform> aimPoints;
    private int aimPointCount;
    private int aimPointChoice;
    public Transform aimLookAtREF;
    private Transform nextAim;

    private bool canSwitch = false;
    private bool lookAtSwitchActive = false;

    public CinemachineFreeLook cinemachineCam;

    void Start()
    {
        cinemachineCam = aimingCamREF.gameObject.GetComponent<CinemachineFreeLook>();
        aimPointCount = 0;
        aimPointChoice = 0;
        aimPoints = new List<Transform>();
    }

    
    //old input system implementation
    void Update()
    {
        
        //Figure this shit out homie!!!
        if (currentAim == null && aimPoints.Count != 0)
        {
           currentAim = aimPoints[0];
        }

        //Smooth Aim Point Transition
        if (lookAtSwitchActive == true)
        {
            if (nextAim != null && aimLookAtREF.transform.position != nextAim.transform.position)
            {
                aimLookAtREF.transform.position = Vector3.MoveTowards(aimLookAtREF.transform.position, nextAim.transform.position, 0.5f);
                cinemachineCam.m_LookAt = aimLookAtREF;
            } else {
                lookAtSwitchActive = false;
                currentAim = aimPoints[aimPointChoice];
                AimSwitch();
            }
        }

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
        
        if (aimPointChoice < aimPointCount - 1)
        {
            aimPointChoice++;
        }
        else
        {
            aimPointChoice = 0;
        }
        //Activates smooth aim switch in update
        nextAim = aimPoints[aimPointChoice];
        aimLookAtREF.transform.position = currentAim.transform.position;
        lookAtSwitchActive = true;
    }
    

    void OnTriggerEnter(Collider collision)
    {
        /*if (collision.gameObject.tag == "AimPoint")
        use COMPARETAG instead of == string comparison*/
        if(collision.gameObject.CompareTag("AimPoint") || collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            currentAim = collision.gameObject.GetComponent<Transform>();
            if (!aimPoints.Exists(element => element == (currentAim)))
            {
                if (aimPointCount < 6)
                {
                    //aimPoints[aimPointCount] = collision.gameObject.GetComponent<Transform>();
                    aimPoints.Add(collision.gameObject.transform);
                    aimPointCount++;
                    //Array.Resize(ref aimPoints, aimPointCount);
                } else {
                    aimPointCount = 0;
                    aimPoints.Add(collision.gameObject.transform);
                    //aimPoints[aimPointCount] = collision.gameObject.GetComponent<Transform>();
                }
                
            }
            
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("AimPoint") || collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            aimPoints.Remove(collision.gameObject.transform);
            aimPointCount--;
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
