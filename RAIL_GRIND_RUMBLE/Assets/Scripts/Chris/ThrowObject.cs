using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObject : MonoBehaviour
{
    private bool isHoldingObject;
    [SerializeField] GameObject heldObjectFakeREF;
    [SerializeField] GameObject heldObjectThrowREF;
    [SerializeField] Transform throwPoint;
    [SerializeField] Transform orientation;

    private float throwForce = 70f;
    private float throwUpwardForce = 50f;

    // Start is called before the first frame update
    void Start()
    {
        isHoldingObject = false;
        throwForce = throwForce * Time.deltaTime;
        throwUpwardForce = throwUpwardForce * Time.deltaTime;
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
        
        Vector3 forceToAdd = orientation.transform.forward * throwForce + transform.up * throwUpwardForce;
        thrownObject.gameObject.GetComponent<Rigidbody>().AddForce(forceToAdd, ForceMode.Impulse);
    }
}
