using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Graffiti : MonoBehaviour
{
   [SerializeField] private GameObject graffiti;
    GameObject graffitiDown;
    GameObject graffitiUp;
    GameObject graffitiRight;

   [SerializeField] private Transform canLocation;
   private GameObject player;
   private int playerX;
   private int playerZ;
   private Camera cam;
    
    void Start ()
    {
        cam = Camera.main;
        player = GameObject.Find("playerPrefab");
         graffitiDown = Resources.Load("ai1") as GameObject;
         graffitiUp = Resources.Load("ai2") as GameObject;
         graffitiRight = Resources.Load("ai3") as GameObject;
        
    }

    void Update()
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
                
                
                GameObject madeGraffiti;
                if (hit.collider.gameObject.tag == "Poster")
                {
                    Debug.Log("detected poster, player would rec boost");
                    GameObject posterInfo = hit.collider.gameObject;
                    var spawnLoc = posterInfo.transform.Find("DecalSpawnLoc");
                    madeGraffiti = Instantiate (graffiti, spawnLoc.transform.position, player.transform.rotation); 
                } else 
                {
                    Debug.Log("detected no poster");
                    madeGraffiti = Instantiate (graffiti, hit.point, player.transform.rotation);
                    Vector3 newPos = new Vector3 (player.transform.position.x, madeGraffiti.transform.position.y, player.transform.position.z);
                    madeGraffiti.transform.position = newPos;
                }

                Vector3 direction = hit.point - canLocation.position;
                canLocation.rotation = Quaternion.LookRotation(direction);
                DecalProjector currentDecal = madeGraffiti.GetComponent<DecalProjector>();
                //currentDecal.fadeFactor = Mathf.Lerp(currentDecal.fadeFactor, 1, 2f * Time.deltaTime);
                currentDecal.fadeFactor = 1;
                
            }
        }
    }



     public void GraffitiDown(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            graffiti = graffitiDown;
            GraffitiFire();
        }

    }
         public void GraffitiUp(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            graffiti = graffitiUp;
            GraffitiFire();
        }

    }
         public void GraffitiRight(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            graffiti = graffitiRight;
            GraffitiFire();
        }

    }


}
