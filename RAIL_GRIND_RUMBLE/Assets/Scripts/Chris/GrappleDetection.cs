using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class GrappleDetection : MonoBehaviour
{
    private GameObject aimingCamREF;
    public Transform currentAim;
    public List<Transform> aimPoints;
    [SerializeField]private int aimPointCount;
    [SerializeField]private int aimPointChoice;
    private Transform aimLookAtREF;
    private Transform nextAim;
    //WIP Potential AimPoint Solution
    private Transform prevAim;
    //
    private Transform player;
    private GrappleHook grappleHookScript;

    private bool canSwitch = false;
    private bool lookAtSwitchActive = false;

    private CinemachineFreeLook cinemachineCam;

    void Start()
    {
        GameObject cameraPrefabREF = GameObject.Find("camerasPrefab");
        aimingCamREF = cameraPrefabREF.transform.Find("AimingCam").gameObject;

        GameObject orientationREF = GameObject.Find("Orientation");
        aimLookAtREF = orientationREF.transform.Find("AimingLookAt");

        cinemachineCam = aimingCamREF.gameObject.GetComponent<CinemachineFreeLook>();
        aimPointCount = 0;
        aimPointChoice = 0;
        aimPoints = new List<Transform>();

        GameObject playerREF = GameObject.Find("playerPrefab");
        player = playerREF.GetComponent<Transform>();
        grappleHookScript = playerREF.GetComponent<GrappleHook>();
    }

    
    //old input system implementation
    void Update()
    {
        if (currentAim == null && aimPoints.Count != 0)
        {
           currentAim = aimPoints[0];
        }

        //Smooth Aim Point Transition
        if (lookAtSwitchActive == true)
        {
            if (currentAim != null && aimLookAtREF.transform.position != currentAim.transform.position)
            {
                aimLookAtREF.transform.position = Vector3.MoveTowards(aimLookAtREF.transform.position, currentAim.transform.position, 0.5f);
                cinemachineCam.m_LookAt = aimLookAtREF;
            } 
        }

        //Aim Point Closest
        if (aimPoints.Count != 0 && aimPoints.Count >= 2 && !GameObject.Find("AimingCam"))
        {
            
            for (int i = 0; i < aimPoints.Count; i++)
            {
                if (aimPoints[0] != null && Vector3.Distance(player.position, aimPoints[0].position) > Vector3.Distance(player.position, aimPoints[i].position))
                {
                    Transform temp = aimPoints[i];
                    aimPoints.Remove(aimPoints[i]);
                    aimPoints.Insert(0, temp);
                    currentAim = aimPoints[0];
                }
            }
        }

    }

    IEnumerator CurrentAimDelay()
    {
        yield return new WaitForSeconds(0.2f);
        SetCurrentAim();
    }

    public void SetCurrentAim()
    {
        //Debug.Log ("Current Aim Set");
        lookAtSwitchActive = false;
        currentAim = aimPoints[aimPointChoice];
        grappleHookScript.CheckObjectType(currentAim);
        AimSwitch();
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
        prevAim = currentAim;
        nextAim = aimPoints[aimPointChoice];
        currentAim = nextAim;
        aimLookAtREF.transform.position = prevAim.transform.position;
        lookAtSwitchActive = true;
        StartCoroutine(CurrentAimDelay());
    }
    

    void OnTriggerEnter(Collider collision)
    {
        /*if (collision.gameObject.tag == "AimPoint")
        use COMPARETAG instead of == string comparison*/
        if(collision.gameObject.CompareTag("AimPoint") || collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (!aimPoints.Exists(element => element == (collision.gameObject.transform)))
            {
                    if (aimPoints.Count == 0)
                    {
                        currentAim = collision.gameObject.transform;
                    } 

                    aimPoints.Add(collision.gameObject.transform);
                    aimPointCount++;   
            }
            
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("AimPoint") || collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            aimPoints.Remove(collision.gameObject.transform);
            aimPointCount--;

            //WIP potential solution for incorrect aimpoints
            if (aimPointChoice > 0 && GameObject.Find("AimingCam"))
            {
                aimPointChoice = 0;
                SetCurrentAim();
            }
        }
    }

    public void RemovePoint(Transform point)
    {
        aimPoints.Remove(point);
        aimPointCount--;
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
