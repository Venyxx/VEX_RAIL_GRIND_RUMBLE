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
    GameObject graffitiLeft;

   [SerializeField] private Transform canLocation;
   private GameObject player;
   private int playerX;
   private int playerZ;
   private Camera cam;

   private ThirdPersonMovement ThirdPersonMovementREF;
    
    void Start ()
    {
        ThirdPersonMovementREF = GameObject.Find("playerPrefab").GetComponent<ThirdPersonMovement>();
        cam = Camera.main;
        player = GameObject.Find("playerPrefab");
        graffitiUp = Resources.Load("Decal_1") as GameObject;
        graffitiDown = Resources.Load<GameObject>("Decal_2");
        graffitiLeft = Resources.Load("Decal_3") as GameObject;
        graffitiRight = Resources.Load("Decal_4") as GameObject;
         
         Debug.Log(graffitiDown);
        
    }
    
    public void GraffitiAction(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        GraffitiFire();
    }

    void GraffitiFire()
    {
        
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.forward, 20.0f);
        
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            
            Debug.Log("layer " + hit.collider.gameObject);
            if (hit.collider.gameObject.layer == 8)
            {
                GameObject madeGraffiti;
                Debug.Log("correct layer");
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
        //Debug.Log("fire context");
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

        public void GraffitiLeft(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            graffiti = graffitiLeft;
            GraffitiFire();
        }

    }


}
