using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graffiti : MonoBehaviour
{
   [SerializeField] private GameObject graffiti;
   [SerializeField] private Transform canLocation;
   private GameObject player;
   private int playerX;
   private int playerZ;
   private Camera cam;
    
    void Start ()
    {
        cam = Camera.main;
        player = GameObject.Find("playerPrefab");
        
    }
    void FixedUpdate()
    {
         if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("input");
            GraffitiFire();
        }
            
    }

    void GraffitiFire()
    {
        //Debug.Log(gameObject);
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.forward, 20.0f);

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            
            //Debug.Log(hit.collider.gameObject);
            if (hit.collider.gameObject.layer == 8)
            {
                
                GameObject madeGraffiti = Instantiate (graffiti, hit.point, Quaternion.LookRotation(hit.normal));
                Vector3 newPos = new Vector3 (player.transform.position.x, madeGraffiti.transform.position.y, player.transform.position.z);
                madeGraffiti.transform.position = newPos;
                
                Vector3 direction = hit.point - canLocation.position;
                canLocation.rotation = Quaternion.LookRotation(direction);
            }
        }
    }
}
