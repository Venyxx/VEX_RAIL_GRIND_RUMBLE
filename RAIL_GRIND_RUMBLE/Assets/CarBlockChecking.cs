using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBlockChecking : MonoBehaviour
{
    private CarMovement carMovementREF;
    
    // Start is called before the first frame update
    void Start()
    {
        carMovementREF = gameObject.transform.parent.GetComponent<CarMovement>();

    }

    void OnTriggerEnter (Collider col)
    {
        Debug.Log("noticed trigger");
        carMovementREF.StopCar();
    }

    void OnTriggerExit (Collider col)
    {
        carMovementREF.StartCar();
    }
}
