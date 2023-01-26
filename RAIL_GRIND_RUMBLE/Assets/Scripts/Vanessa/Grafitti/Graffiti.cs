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

    GameObject graffitiParticle;
    GameObject graffitiParticle2;

   [SerializeField] private Transform canLocation;
   private GameObject canLocationForParticle;

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
        graffitiParticle = Resources.Load("Particle_1") as GameObject;
        graffitiParticle2 = Resources.Load("Particle_2") as GameObject;

         canLocationForParticle = GameObject.FindGameObjectWithTag("PlayerCan");
         Debug.Log(" tried to load " + graffitiParticle);
        
    }


    void GraffitiFire()
    {
        
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.forward, 20.0f);
        
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            Debug.Log(hits.Length);
            Debug.Log("hit object " + hit.collider.gameObject);

            GameObject madeGraffiti;
            GameObject particle;
            GameObject particle2;

            if (hit.collider.gameObject.layer == 8)
            {
                
                if (hit.collider.gameObject.tag == "Poster")
                {
                    Debug.Log("detected poster, player would rec boost");
                    GameObject posterInfo = hit.collider.gameObject;
                    var spawnLoc = posterInfo.transform.Find("DecalSpawnLoc");
                    particle = Instantiate (graffitiParticle, canLocationForParticle.transform.position, canLocation.transform.rotation);
                    madeGraffiti = Instantiate (graffiti, spawnLoc.transform.position,canLocation.transform.rotation); 
                    particle2 = Instantiate (graffitiParticle2, player.transform.position, player.transform.rotation);

                    return;
                    
                } else 
                {
                    Debug.Log("detected no poster");
                    madeGraffiti = Instantiate (graffiti, hit.point, canLocation.transform.rotation);
                    particle = Instantiate (graffitiParticle, canLocationForParticle.transform.position, canLocation.transform.rotation);
                    Vector3 newPos = new Vector3 (player.transform.position.x, madeGraffiti.transform.position.y, player.transform.position.z);
                    madeGraffiti.transform.position = newPos;
                    return;
                }
                
                
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
