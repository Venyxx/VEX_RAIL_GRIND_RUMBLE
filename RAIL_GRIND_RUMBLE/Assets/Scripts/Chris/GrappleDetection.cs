using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class GrappleDetection : MonoBehaviour
{
    private GameObject aimingCamREF;
    private Camera mainCamREF;
    public Transform currentAim;
    public List<Transform> aimPoints;
    [SerializeField]public int aimPointCount;
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
    float mouseValue;

    //For visibility checking
    public List<Transform> aimPointsNonVisible;
    public LayerMask viewBlockers;
    public LayerMask grappleable;
    RaycastHit hit;

    bool aimIsVisible;

    //Throwable Checking
    private ThrowObject throwObjectScript;

    //For adaptive music
    AdaptiveMusic adaptiveMusic;
    public List<Transform> enemiesInRange;

    void Start()
    {
        GameObject cameraPrefabREF = GameObject.Find("camerasPrefab");
        aimingCamREF = cameraPrefabREF.transform.Find("AimingCam").gameObject;

        mainCamREF = UnityEngine.Camera.main;
        //mainCamREF = GameObject.Find("Main Camera");

        GameObject orientationREF = GameObject.Find("Orientation");
        aimLookAtREF = orientationREF.transform.Find("AimingLookAt");

        cinemachineCam = aimingCamREF.gameObject.GetComponent<CinemachineFreeLook>();
        aimPointCount = 0;
        aimPointChoice = 0;
        aimPoints = new List<Transform>();
        aimPointsNonVisible = new List<Transform>();

        GameObject playerREF = GameObject.Find("playerPrefab");
        player = playerREF.GetComponent<Transform>();
        grappleHookScript = playerREF.GetComponent<GrappleHook>();
        throwObjectScript = playerREF.GetComponent<ThrowObject>();

        adaptiveMusic = GameObject.Find("Music").GetComponent<AdaptiveMusic>();
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
                //Sorts aimPoints by distance
                for (int i = 0; i < aimPoints.Count; i++)
                {
                    if (aimPoints[0] != null && Vector3.Distance(player.position, aimPoints[0].position) > Vector3.Distance(player.position, aimPoints[i].position))
                    {
                        Transform temp = aimPoints[i];
                        aimPoints.Remove(aimPoints[i]);
                        aimPoints.Insert(0, temp);
                    }
                }

                //Prioritize AimPoint in camera view
                for (int i = 0; i < aimPoints.Count; i++)
                {
                    //Checks if aimPoint is behind wall or not
                    /*Vector3 raycastDir = aimPoints[i].position - player.position;
                    if (Physics.Raycast(player.position, raycastDir, out hit, Vector3.Distance(transform.position, aimPoints[i].position), viewBlockers))
                    {
                        aimPointsNonVisible.Add(aimPoints[i]);
                        aimPoints.Remove(aimPoints[i]);
                        aimPointCount--;
                    } else*/ if (Vector3.Dot((aimPoints[i].position - mainCamREF.transform.position), mainCamREF.transform.forward) < 6)
                    {
                        currentAim = aimPoints[0];
                    } else if (Vector3.Dot((currentAim.position - mainCamREF.transform.position), mainCamREF.transform.forward) < 6)
                    {

                        currentAim = aimPoints[i];

                    }
                }
        }

        if (adaptiveMusic != null)
        {
            if (enemiesInRange.Count > 0 && adaptiveMusic.combatMusic.volume == 0)
            {
                adaptiveMusic.StartCoroutine(adaptiveMusic.SwitchSongs("Combat"));
            } else if (enemiesInRange.Count == 0 && adaptiveMusic.standardMusic.volume == 0)
            {
                adaptiveMusic.StartCoroutine(adaptiveMusic.SwitchSongs("Standard"));
            }
        }
        

        if (aimPoints.Count == 0)
        {
            aimPointCount = 0;
        } else if (aimPointCount != aimPoints.Count)
        {
            aimPointCount = aimPoints.Count;
        }

        //Checking blocked AimPoints
        /*if (aimPointsNonVisible.Count != 0)
        {
            for (int i = 0; i < aimPointsNonVisible.Count; i++)
            {
                Vector3 raycastDir = aimPointsNonVisible[i].position - player.position;
                if (Physics.Raycast(player.position, raycastDir, out hit, Vector3.Distance(transform.position, aimPointsNonVisible[i].position), grappleable))
                    {
                        aimPoints.Add(aimPointsNonVisible[i]);
                        aimPointCount++;
                        aimPointsNonVisible.Remove(aimPointsNonVisible[i]);
                    }
            }
        }*/

    }

    void FixedUpdate()
    {
        //Raycast to check for walls (only works sometimes!!! WHY)
        /*Transform playerObject = GameObject.FindWithTag("PlayerObject").transform;
        var ray = new Ray(this.transform.position, currentAim.transform.position - playerObject.position);
        RaycastHit hit;
        int wallLayer = LayerMask.NameToLayer("wallrun");
        if (Physics.Raycast(ray, out hit, Vector3.Distance(playerObject.position, currentAim.transform.position)))
        {
            //Why this aint work yo look it up dummy
            Debug.DrawRay(playerObject.position, currentAim.transform.position - playerObject.position, Color.green);
            if (hit.transform.gameObject.layer == wallLayer)
            {
                //Debug.Log("Wall Hit");
                aimIsVisible = false;
            } else {
                aimIsVisible = true;
            }
        }*/
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
    
    //Right Stick Right
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

    //Right Stick Left
    public void GrappleSwitchBack(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        if (!canSwitch) return;

        if (aimPointChoice > 0)
        {
            aimPointChoice--;
        } else {
            aimPointChoice = aimPointCount - 1;
        }
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
        if(collision.gameObject.CompareTag("AimPoint") || collision.gameObject.CompareTag("DP1Plug"))
        {
            if (!aimPoints.Exists(element => element == (collision.gameObject.transform)))
            {
                    if (aimPoints.Count == 0)
                    {
                        currentAim = collision.gameObject.transform;
                        
                    } 

                    aimPoints.Add(collision.gameObject.transform);
                    aimPointCount++;
                    GetComponent<Reticle>().ReticleToggle(true); 
            }
            
        }

        //Adaptive Music
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            /*if (enemiesInRange.Count == 0)
            {
                adaptiveMusic.StartCoroutine(adaptiveMusic.SwitchSongs("Combat"));
            }*/
            enemiesInRange.Add(collision.gameObject.transform);
        }
    }

    //Enemy aimPoint conditional
    void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (throwObjectScript.isHoldingObject == true && !aimPoints.Exists(element => element == collision.gameObject.transform))
            {
                aimPoints.Add(collision.gameObject.transform);
                aimPointCount++;
            } else if (throwObjectScript.isHoldingObject == false && aimPoints.Exists(element => element == collision.gameObject.transform))
            {
                aimPoints.Remove(collision.gameObject.transform);
                aimPointCount--;
                if (aimPoints.Count > 0) 
                {
                    currentAim = aimPoints[0];
                }
            }
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (aimPoints.Exists(element => element == collision.gameObject.transform))
            {
                aimPoints.Remove(collision.gameObject.transform);
                aimPointCount--;
            }

            //Adaptive Music
            enemiesInRange.Remove(collision.gameObject.transform);
            /*if (enemiesInRange.Count == 0)
            {
                adaptiveMusic.StartCoroutine(adaptiveMusic.SwitchSongs("Standard"));
            }*/
        }


        if (collision.gameObject.CompareTag("AimPoint") || collision.gameObject.CompareTag("DP1Plug"))
        {
            if (aimPoints.Contains(collision.gameObject.transform))
            {
                aimPoints.Remove(collision.gameObject.transform);
                aimPointCount--;
            } else if (aimPointsNonVisible.Contains(collision.gameObject.transform))
            {
                aimPointsNonVisible.Remove(collision.gameObject.transform);
            }
            

            //WIP potential solution for incorrect aimpoints
            if (aimPointChoice > 0 && GameObject.Find("AimingCam"))
            {
                aimPointChoice = 0;
                SetCurrentAim();
            }

            //Deactivate Reticle
            if (aimPoints.Count == 0)
            {
                GetComponent<Reticle>().ReticleToggle(false);
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
