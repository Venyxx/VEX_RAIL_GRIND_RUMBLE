using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThrowObject : MonoBehaviour
{
    private bool isHoldingObject;
    [SerializeField] GameObject heldObjectFakeREF;
    [SerializeField] GameObject heldObjectThrowREF;
    [SerializeField] Transform throwPoint;
    Transform orientation;
    GrappleHook grappleHookScript;
    private bool _targeting = false;

    [SerializeField] private float throwForce;
    [SerializeField] private float throwUpwardForce;

    private ThirdPersonMovement thirdPersonMovement;


    // Start is called before the first frame update
    void Start()
    {
        isHoldingObject = false;
        grappleHookScript = gameObject.GetComponent<GrappleHook>();
        GameObject orientationREF = GameObject.Find("Orientation");
        orientation = orientationREF.gameObject.GetComponent<Transform>();
        thirdPersonMovement = GetComponent<ThirdPersonMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Mouse0) && isHoldingObject == true) 
        {
            ThrowObjectAction();
        }*/
    }

    public void SpawnHeldObject()
    {
      
        isHoldingObject = true;
        heldObjectFakeREF.SetActive(true);
    }

    void ThrowObjectAction()
    {
        isHoldingObject = false;
        heldObjectFakeREF.SetActive(false);

        GameObject thrownObject;
        thrownObject = Instantiate(heldObjectThrowREF, throwPoint.transform.position, heldObjectThrowREF.transform.rotation);
        
        if(_targeting)
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
        if ((!(context.started && isHoldingObject)) || thirdPersonMovement.DialogueBox.activeInHierarchy) return;
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
