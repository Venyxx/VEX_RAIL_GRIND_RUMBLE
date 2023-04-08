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

    GameObject passingGraffiti;

    GameObject graffitiParticle;
    GameObject graffitiParticle2;

   [SerializeField] private Transform canLocation;
   private GameObject canLocationForParticle;

   private GameObject player;
   private int playerX;
   private int playerZ;
   private Camera cam;
   private ThirdPersonMovement ThirdPersonMovementREF;
   private PlayerAttack playerAttackREF; 

   public float graffitiBuffTimer;
    
    void Start ()
    {
        ThirdPersonMovementREF = GameObject.Find("playerPrefab").GetComponent<ThirdPersonMovement>();
        playerAttackREF = GameObject.Find("playerPrefab").GetComponent<PlayerAttack>();
        cam = Camera.main;
        player = GameObject.Find("playerPrefab");
        graffitiUp = Resources.Load(SaveManager.Instance.state.ariGraffitiSlotUp1) as GameObject;
        graffitiDown = Resources.Load(SaveManager.Instance.state.ariGraffitiSlotDown3) as GameObject;
        graffitiLeft = Resources.Load(SaveManager.Instance.state.ariGraffitiSlotLeft4) as GameObject;
        graffitiRight = Resources.Load(SaveManager.Instance.state.ariGraffitiSlotRight2) as GameObject;

        Debug.Log("Graffiti List: " + graffitiUp + " and " + graffitiDown + " and " + graffitiLeft + " and " + graffitiRight); 

        
        
        graffitiParticle = Resources.Load("Particle_1") as GameObject;
        graffitiParticle2 = Resources.Load("Particle_2") as GameObject;

         canLocationForParticle = GameObject.FindGameObjectWithTag("PlayerCan");
         //Debug.Log(" tried to load " + graffitiParticle);
        
    }

    void Update ()
    {
        if (graffitiBuffTimer > 0)
        {
             graffitiBuffTimer -= Time.deltaTime;
             playerAttackREF.isBuffed = true;
             
        } else 
            playerAttackREF.isBuffed = false;
           
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
                    //particle = Instantiate (graffitiParticle, canLocationForParticle.transform.position, canLocation.transform.rotation);
                    madeGraffiti = Instantiate (graffiti, spawnLoc.transform.position,canLocation.transform.rotation); 
                    particle2 = Instantiate (graffitiParticle2, player.transform.position, player.transform.rotation);
                    
                    ProgressionManager manager = ProgressionManager.Get();
                    if (manager.currentQuest is CountQuest countQuest && countQuest.GetCountQuestType() is CountQuestType.Graffiti)
                    {
                        countQuest.IncrementCount();
                    }

                    //buff
                    graffitiBuffTimer = 60f;
                    playerAttackREF.isBuffed = true;
                    //60 seconds
                    return;
                    
                } else 
                {
                    Debug.Log("detected no poster");
                    Debug.Log(hit.normal);
                    Vector3 reverseHit = new Vector3(-hit.normal.x, -hit.normal.y, -hit.normal.z);
                    madeGraffiti = Instantiate (graffiti, hit.point, Quaternion.LookRotation(reverseHit));
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
        if (PauseMenu.isPaused == true) return;

        if (context.started)
        {
            if (GameObject.Find("CustomizationVendor") || GameObject.Find ("CustomizationVendor(Clone)"))
            {
                 if (GameObject.Find("CustomizationVendor").GetComponent<PickingGraffiti>().isPickingGraffiti || GameObject.Find("CustomizationVendor(Clone)").GetComponent<PickingGraffiti>().isPickingGraffiti)
            {
                WhichGraffiti();
                graffitiDown = passingGraffiti;
                Debug.Log("Switching graffiti down to" + passingGraffiti);
                if (GameObject.Find("CustomizationVendor"))
                        GameObject.Find("CustomizationVendor").GetComponent<PickingGraffiti>().isPickingGraffiti = false;
                if (GameObject.Find("CustomizationVendor(Clone)"))
                        GameObject.Find("CustomizationVendor(Clone)").GetComponent<PickingGraffiti>().isPickingGraffiti = false;

                SaveManager.Instance.state.ariGraffitiSlotDown3 = graffitiDown.gameObject.name;
                SaveManager.Instance.Save();
                    return;         
            }
            }
           
            graffiti = graffitiDown;
            GraffitiFire();
        }

    }
         public void GraffitiUp(InputAction.CallbackContext context)
    {
        if (PauseMenu.isPaused == true) return;
        //Debug.Log("fire context");
        if (context.started)
        {
            if (GameObject.Find("CustomizationVendor") || GameObject.Find ("CustomizationVendor(Clone)"))
            {
                if (GameObject.Find("CustomizationVendor").GetComponent<PickingGraffiti>().isPickingGraffiti || GameObject.Find("CustomizationVendor(Clone)").GetComponent<PickingGraffiti>().isPickingGraffiti)
            {
                WhichGraffiti();
                graffitiUp = passingGraffiti;
                Debug.Log("Switching graffiti up to" + passingGraffiti);
                if (GameObject.Find("CustomizationVendor"))
                        GameObject.Find("CustomizationVendor").GetComponent<PickingGraffiti>().isPickingGraffiti = false;
                if (GameObject.Find("CustomizationVendor(Clone)"))
                        GameObject.Find("CustomizationVendor(Clone)").GetComponent<PickingGraffiti>().isPickingGraffiti = false;
                
                SaveManager.Instance.state.ariGraffitiSlotUp1 = graffitiUp.gameObject.name;
                SaveManager.Instance.Save();
                return;           
            }
            }
            
            
            graffiti = graffitiUp;
            GraffitiFire();
        }

    }
         public void GraffitiRight(InputAction.CallbackContext context)
    {
        if (PauseMenu.isPaused == true) return;
        if (context.started)
        {
            if (GameObject.Find("CustomizationVendor") || GameObject.Find ("CustomizationVendor(Clone)"))
            {
              if (GameObject.Find("CustomizationVendor").GetComponent<PickingGraffiti>().isPickingGraffiti || GameObject.Find("CustomizationVendor(Clone)").GetComponent<PickingGraffiti>().isPickingGraffiti)
            {
                WhichGraffiti();
                graffitiRight = passingGraffiti;
                Debug.Log("Switching graffiti right to" + passingGraffiti);
                if (GameObject.Find("CustomizationVendor"))
                        GameObject.Find("CustomizationVendor").GetComponent<PickingGraffiti>().isPickingGraffiti = false;
                if (GameObject.Find("CustomizationVendor(Clone)"))
                        GameObject.Find("CustomizationVendor(Clone)").GetComponent<PickingGraffiti>().isPickingGraffiti = false;
                
                SaveManager.Instance.state.ariGraffitiSlotRight2 = graffitiRight.gameObject.name;
                SaveManager.Instance.Save();
                return;      
            }  
            }
            
            
            graffiti = graffitiRight;
            GraffitiFire();
        }

    }

        public void GraffitiLeft(InputAction.CallbackContext context)
    {
        if (PauseMenu.isPaused == true) return;
        if (context.started)
        {
            if (GameObject.Find("CustomizationVendor") || GameObject.Find ("CustomizationVendor(Clone)"))
            {
              if (GameObject.Find("CustomizationVendor").GetComponent<PickingGraffiti>().isPickingGraffiti || GameObject.Find("CustomizationVendor(Clone)").GetComponent<PickingGraffiti>().isPickingGraffiti)
            {
                WhichGraffiti();
                graffitiLeft = passingGraffiti;
                Debug.Log("Switching graffiti left to" + passingGraffiti);
                if (GameObject.Find("CustomizationVendor"))
                        GameObject.Find("CustomizationVendor").GetComponent<PickingGraffiti>().isPickingGraffiti = false;
                if (GameObject.Find("CustomizationVendor(Clone)"))
                        GameObject.Find("CustomizationVendor(Clone)").GetComponent<PickingGraffiti>().isPickingGraffiti = false;
                
                SaveManager.Instance.state.ariGraffitiSlotLeft4 = graffitiLeft.gameObject.name;
                SaveManager.Instance.Save();
              return;               
            }
              
            }
            
            graffiti = graffitiLeft;
            GraffitiFire();
        }

    }

    private void WhichGraffiti()
    {
        if(GameObject.Find("CustomizationVendor").GetComponent<ETCCustomizationVendor>().selectedGraffitiIndex == 0)
            passingGraffiti = Resources.Load("Decal_1") as GameObject;
        else if(GameObject.Find("CustomizationVendor").GetComponent<ETCCustomizationVendor>().selectedGraffitiIndex == 1)
            passingGraffiti = Resources.Load("Decal_2") as GameObject;
        else if(GameObject.Find("CustomizationVendor").GetComponent<ETCCustomizationVendor>().selectedGraffitiIndex == 2)
            passingGraffiti = Resources.Load("Decal_3") as GameObject; 
        else if(GameObject.Find("CustomizationVendor").GetComponent<ETCCustomizationVendor>().selectedGraffitiIndex == 3)
            passingGraffiti = Resources.Load("Decal_4") as GameObject;
        else if(GameObject.Find("CustomizationVendor").GetComponent<ETCCustomizationVendor>().selectedGraffitiIndex == 4)
            passingGraffiti = Resources.Load("Decal_5") as GameObject;
        else if(GameObject.Find("CustomizationVendor").GetComponent<ETCCustomizationVendor>().selectedGraffitiIndex == 5)
            passingGraffiti = Resources.Load("Decal_6") as GameObject; 
        else if(GameObject.Find("CustomizationVendor").GetComponent<ETCCustomizationVendor>().selectedGraffitiIndex == 6)
            passingGraffiti = Resources.Load("Decal_7") as GameObject;
        else if(GameObject.Find("CustomizationVendor").GetComponent<ETCCustomizationVendor>().selectedGraffitiIndex == 7)
            passingGraffiti = Resources.Load("Decal_8") as GameObject;
        else if(GameObject.Find("CustomizationVendor").GetComponent<ETCCustomizationVendor>().selectedGraffitiIndex == 8)
            passingGraffiti = Resources.Load("Decal_9") as GameObject; 
        else if(GameObject.Find("CustomizationVendor").GetComponent<ETCCustomizationVendor>().selectedGraffitiIndex == 9)
            passingGraffiti = Resources.Load("Decal_10") as GameObject;
    }

     

}
