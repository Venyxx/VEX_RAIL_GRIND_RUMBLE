using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDespawner : MonoBehaviour
{
    // Start is called before the first frame update
void OnTriggerEnter (Collider col)
{
    if (col.gameObject.name == "FrontCheck" || col.gameObject.tag == "MovingCar")
    {
        Destroy(col.gameObject);
    }
}
}
