using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graffiti : MonoBehaviour
{
   [SerializeField] private GameObject graffiti;
   [SerializeField] private Transform canLocation;
   private Camera cam;
    
    void Start ()
    {
        cam = Camera.main;
        
    }
    void FixedUpdate()
    {
         if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("input");
            Shoot ();
        }
            
    }

    void Shoot ()
    {
       
            Debug.Log("ray");
            Ray rayOrigin = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit; 
            Ray ray = cam.ScreenPointToRay(Input.mousePosition); 

            if (Physics.Raycast(ray, out hit, 100.0f)) 
            {
                Debug.Log(hit.collider.gameObject);
                if (hit.collider.gameObject.layer == 8)
                {
                    Debug.Log("layer 8");
                    Instantiate (graffiti, hit.point, Quaternion.LookRotation(hit.normal));

                    Vector3 direction = hit.point - canLocation.position;
                    canLocation.rotation = Quaternion.LookRotation(direction);
                }
            }
    }
}
