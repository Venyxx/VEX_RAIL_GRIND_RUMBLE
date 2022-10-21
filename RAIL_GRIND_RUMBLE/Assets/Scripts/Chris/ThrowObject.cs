using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    private bool isHoldingObject;
    [SerializeField] GameObject heldObjectFakeREF;
    [SerializeField] GameObject heldObjectThrowREF;
    [SerializeField] Transform throwPoint;
    Transform orientation;
    GrappleHook grappleHookScript;

    [SerializeField] private float throwForce;
    [SerializeField] private float throwUpwardForce;

    // Start is called before the first frame update
    void Start()
    {
        isHoldingObject = false;
        grappleHookScript = gameObject.GetComponent<GrappleHook>();
        GameObject orientationREF = GameObject.Find("Orientation");
        orientation = orientationREF.gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && isHoldingObject == true) 
        {
            ThrowObjectAction();
        }
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

        if (Input.GetKey(KeyCode.LeftShift))
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
}
