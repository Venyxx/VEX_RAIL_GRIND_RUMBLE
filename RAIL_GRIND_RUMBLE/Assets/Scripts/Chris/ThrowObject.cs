using UnityEngine;
using UnityEngine.InputSystem;

public class ThrowObject : MonoBehaviour
{
    //holding and throwing anim stuff - Raul ////////////////////////////////////////////////////////////////////////////////////
    
    [SerializeField] Animator ariAnimator;
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    public bool isHoldingObject;
    string objectHolding;
    [SerializeField] GameObject heldTrashcanFakeREF;
    [SerializeField] GameObject heldTrashcanThrowREF;
    [SerializeField] GameObject heldDroneFakeREF;
    [SerializeField] GameObject heldDroneThrowREF;
    [SerializeField] Transform throwPoint;
    Transform orientation;
    GrappleHook grappleHookScript;
    private bool _targeting = false;

    [SerializeField] private float throwForce;
    [SerializeField] private float throwUpwardForce;

    private ThirdPersonMovement thirdPersonMovement;
    private GrappleDetection grappleDetection;


    // Start is called before the first frame update
    void Start()
    {
       
        isHoldingObject = false;
        grappleHookScript = gameObject.GetComponent<GrappleHook>();
        GameObject orientationREF = GameObject.Find("Orientation");
        orientation = orientationREF.gameObject.GetComponent<Transform>();
        thirdPersonMovement = GetComponent<ThirdPersonMovement>();
        grappleDetection = gameObject.transform.Find("GrappleDetector").GetComponent<GrappleDetection>();
        objectHolding = "";
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Mouse0) && isHoldingObject == true) 
        {
            ThrowObjectAction();
        }*/


        
    }

    public void SpawnHeldObject(string obj)
    {
        //ariAnimator.SetBool("isHolding", true);
        isHoldingObject = true;
        objectHolding = obj;

        if (obj == "Trashcan")
        {
            heldTrashcanFakeREF.SetActive(true);
        } else if (obj == "Drone")
        {
            heldDroneFakeREF.SetActive(true);
        }
        
    }

    void ThrowObjectAction()
    {
        ariAnimator.SetBool("isHolding", false);
        isHoldingObject = false;
        heldTrashcanFakeREF.SetActive(false);
        heldDroneFakeREF.SetActive(false);

        //Resumes time
        if (GameObject.Find("AimingCam"))
        {
            GameObject aimingCamREF = GameObject.Find("AimingCam");
            aimingCamREF.GetComponent<ThirdPersonCamera>().SwitchCameraCanceled();
        }

        GameObject thrownObject = null;

        if (objectHolding == "Trashcan")
        {
            thrownObject = Instantiate(heldTrashcanThrowREF, throwPoint.transform.position, heldTrashcanThrowREF.transform.rotation);
        } else if (objectHolding == "Drone")
        {
            thrownObject = Instantiate(heldDroneThrowREF, throwPoint.transform.position, heldDroneThrowREF.transform.rotation);
        }
        
        objectHolding = "";
        
        if(_targeting /*grappleDetection.aimPoints.Count > 0*/ && grappleDetection.currentAim.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            thrownObject.gameObject.GetComponent<PlayerThrownObject>().target = true;
            return;
        } else {
            thrownObject.gameObject.GetComponent<PlayerThrownObject>().target = false;
        }
        
        Vector3 forceToAdd = orientation.transform.forward * throwForce + transform.up * throwUpwardForce;
        //Debug.Log($"Transform Up:{transform.up}\nOrientation Transform Forward:{orientation.transform.forward}\nThrow Force{throwForce}\nThrow Upward Force{throwUpwardForce}");
        thrownObject.gameObject.GetComponent<Rigidbody>().AddForce(forceToAdd, ForceMode.Impulse);
      
    }

    public void Throw(InputAction.CallbackContext context)
    {
        if ((!(context.started && isHoldingObject)) || thirdPersonMovement.dialogueBox.activeInHierarchy) return;
        ThrowObjectAction();
        gameObject.GetComponent<ThirdPersonMovement>().PlaySound(2);
    }

    public void SetTarget(InputAction.CallbackContext context)
    {
        if (context.performed) 
            _targeting = true;
        else 
            _targeting = false;
    }
    
}
