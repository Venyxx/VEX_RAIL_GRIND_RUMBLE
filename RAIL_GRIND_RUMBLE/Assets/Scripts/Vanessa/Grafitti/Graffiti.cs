using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class Graffiti : MonoBehaviour
{
   [SerializeField] private GameObject graffiti;
     GameObject graffitiDown;
     GameObject graffitiUp;
     GameObject graffitiRight;
     GameObject graffitiLeft;

    GameObject passingGraffiti;

    public GameObject graffitiParticle;
    public GameObject graffitiParticle2;

   [SerializeField] private Transform canLocation;
   private GameObject canLocationForParticle;

   private GameObject player;
   private int playerX;
   private int playerZ;
   private Camera cam;
   private ThirdPersonMovement ThirdPersonMovementREF;
   private PlayerAttack playerAttackREF;
   private CanvasGroup buffCanvasDisplay; 

   public GameObject upDisplay;
   public GameObject downDisplay;
   public GameObject rightDisplay;
   public GameObject leftDisplay;

   private CanvasGroup fadeGroup;

   public Sprite[] graffitiSprites = new Sprite[10];

   public float maxGraffitiBuffTime;
   private float currentGraffitiBuffTime;
   private bool wasThereAPoster;
   RaycastHit theHit;
    
    void Start ()
    {
        currentGraffitiBuffTime = 0;
        //Debug.Log("STARTING-------------");
        ThirdPersonMovementREF = GameObject.Find("playerPrefab").GetComponent<ThirdPersonMovement>();
        playerAttackREF = GameObject.Find("playerPrefab").GetComponent<PlayerAttack>();
        cam = Camera.main;
        player = GameObject.Find("playerPrefab");
        buffCanvasDisplay = GameObject.Find("damageBuffIcon").GetComponent<CanvasGroup>();

        //Debug.Log("buff icon: "  + buffCanvasDisplay);
        buffCanvasDisplay.gameObject.SetActive(false);


        upDisplay = GameObject.Find("Up");
        rightDisplay = GameObject.Find("Right");
        downDisplay = GameObject.Find("Down");
        leftDisplay = GameObject.Find("Left");

        //Debug.Log(upDisplay.transform.parent);
        


        graffitiUp = (GameObject)Resources.Load(SaveManager.Instance.state.ariGraffitiSlotUp1, typeof(GameObject));
        graffitiDown = (GameObject)Resources.Load(SaveManager.Instance.state.ariGraffitiSlotDown3, typeof(GameObject));
        graffitiLeft =(GameObject)Resources.Load(SaveManager.Instance.state.ariGraffitiSlotLeft4, typeof(GameObject));
        graffitiRight =(GameObject)Resources.Load(SaveManager.Instance.state.ariGraffitiSlotRight2, typeof(GameObject));

        
        //RecalculateGraffitiDisplay();

        Debug.Log("Graffiti List: " + graffitiUp + " and " + graffitiDown + " and " + graffitiLeft + " and " + graffitiRight); 
        
        if(GameObject.Find("SpriteButtons"))
            fadeGroup = GameObject.Find("SpriteButtons").GetComponent<CanvasGroup>();
        
        
        //graffitiParticle = Resources.Load("Particle_1") as GameObject;
        //graffitiParticle2 = Resources.Load("Particle_2") as GameObject;

         canLocationForParticle = GameObject.Find("SprayCanTransform");

        
         
         //Debug.Log(" tried to load " + graffitiParticle);
        
    }

    void Update ()
    {
        //upDisplay = GameObject.Find("Up");
        //rightDisplay = GameObject.Find("Right");
        //downDisplay = GameObject.Find("Down");
        //leftDisplay = GameObject.Find("Left");
        
        
        if (currentGraffitiBuffTime > 0)
        {
             currentGraffitiBuffTime -= Time.deltaTime;
             playerAttackREF.isBuffed = true;
             
        } else 
            playerAttackREF.isBuffed = false;


        //display damage buff
        if (currentGraffitiBuffTime > 0)
        {
            buffCanvasDisplay.gameObject.SetActive(true);

            if(currentGraffitiBuffTime < 3)
                buffCanvasDisplay.alpha = Mathf.PingPong(Time.time, .25f);
            else if (currentGraffitiBuffTime > 3 && currentGraffitiBuffTime < 7)
                buffCanvasDisplay.alpha = Mathf.PingPong(Time.time, 1f);
            else if (currentGraffitiBuffTime > 10)
                buffCanvasDisplay.alpha = 1;
        } else 
            buffCanvasDisplay.gameObject.SetActive(false);
            
        //HANDLE THIS SOMEWHERE ELSE
         if (GameObject.Find("CustomizationVendor"))
            {
                 if (GameObject.Find("CustomizationVendor").GetComponent<PickingGraffiti>().isPickingGraffiti == true)
                {
                    fadeGroup = GameObject.Find("SpriteButtons").GetComponent<CanvasGroup>();
                    fadeGroup.alpha = Mathf.PingPong(Time.time, .5f);
                }else
                {
                    if (fadeGroup)
                        fadeGroup.alpha = 1;
                } 

            }

        if (GameObject.Find ("CustomizationVendor(Clone)"))
        {
            if(GameObject.Find("CustomizationVendor(Clone)").GetComponent<PickingGraffiti>().isPickingGraffiti == true)
            {
                fadeGroup = GameObject.Find("SpriteButtons").GetComponent<CanvasGroup>();
                fadeGroup.alpha = Mathf.PingPong(Time.time, .5f);
            }else
            {
                if (fadeGroup)
                    fadeGroup.alpha = 1;
            } 
        }


        
            
                
    }


    void GraffitiFire()
    {
        
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.forward, 5.0f);
        Debug.DrawRay(transform.position, transform.forward, Color.green, 2.0f);

        

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            Debug.Log("we hit " + hits.Length + " objects in the graffiti method");
            Debug.Log("hit object " + hit.collider.gameObject);

            if (hit.collider.gameObject.tag == "Poster" || hit.collider.gameObject.layer == 19)
            {
                wasThereAPoster = true;
                theHit = hits[i];
                break;
            }
                
            if (i == hits.Length)
                theHit = hits[0];
        }

        GameObject madeGraffiti;
        GameObject particle;
        GameObject particle2;   
        //if poster
        if (wasThereAPoster)
            {
                if (theHit.collider.gameObject.GetComponent<PosterActive>().isSprayed)
                {
                    Debug.Log("this poster was sprayed");
                    
                    Debug.Log(theHit.normal);
                    Vector3 reverseHit = new Vector3(-theHit.normal.x, -theHit.normal.y, -theHit.normal.z);
                    madeGraffiti = Instantiate (graffiti, canLocationForParticle.transform.position, Quaternion.LookRotation(reverseHit));
                    particle = Instantiate (graffitiParticle, player.transform.position, player.transform.rotation);
                    Vector3 newPos = new Vector3 (player.transform.position.x, madeGraffiti.transform.position.y, player.transform.position.z);
                    madeGraffiti.transform.position = newPos;
                    
                    return;
                }
                    

                Debug.Log("detected poster, player would rec boost");
                GameObject posterInfo = theHit.collider.gameObject;
                var spawnLoc = posterInfo.transform.Find("DecalSpawnLoc");
                //particle = Instantiate (graffitiParticle, canLocationForParticle.transform.position, canLocation.transform.rotation);
                madeGraffiti = Instantiate (graffiti, spawnLoc.transform.position,spawnLoc.transform.rotation); 
                particle2 = Instantiate (graffitiParticle2, player.transform.position, player.transform.rotation);
                
                //moved to graffitichecking cs for quest sake
                ProgressionManager manager = ProgressionManager.Get();
                if (manager.currentQuest is CountQuest countQuest && countQuest.GetCountQuestType() is CountQuestType.Graffiti)
                {
                    countQuest.IncrementCount();
                }

                //buff
                //maxGraffitiBuffTimer = 30f;
                currentGraffitiBuffTime = maxGraffitiBuffTime;
                playerAttackREF.isBuffed = true;
                theHit.collider.gameObject.GetComponent<PosterActive>().isSprayed = true;
                return;
                
            } 
                
                
        if (!wasThereAPoster) 
        {
            Debug.Log("detected no poster");
            Debug.Log(theHit.normal);
            Vector3 reverseHit = new Vector3(-theHit.normal.x, -theHit.normal.y, -theHit.normal.z);
            madeGraffiti = Instantiate (graffiti, canLocationForParticle.transform.position, Quaternion.LookRotation(reverseHit));
            particle = Instantiate (graffitiParticle, player.transform.position, player.transform.rotation);
            Vector3 newPos = new Vector3 (player.transform.position.x, madeGraffiti.transform.position.y, player.transform.position.z);
            madeGraffiti.transform.position = newPos;
            return;
        }

            wasThereAPoster = false;
            


    }



     public void GraffitiDown(InputAction.CallbackContext context)
    {
        if (PauseMenu.isPaused == true) return;

        if (context.started)
        {
            if (GameObject.Find("CustomizationVendor"))
            {
                 if (GameObject.Find("CustomizationVendor").GetComponent<PickingGraffiti>().isPickingGraffiti == true)
                {
                    WhichGraffiti();
                    graffitiDown = passingGraffiti;
                    Debug.Log("Switching graffiti down to" + passingGraffiti);
                    if (GameObject.Find("CustomizationVendor"))
                            GameObject.Find("CustomizationVendor").GetComponent<PickingGraffiti>().isPickingGraffiti = false;


                    SaveManager.Instance.state.ariGraffitiSlotDown3 = graffitiDown.gameObject.name;
                    
                    SaveManager.Instance.Save();
                    RecalculateGraffitiDisplay();
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
            if (GameObject.Find("CustomizationVendor"))
            {
                if (GameObject.Find("CustomizationVendor").GetComponent<PickingGraffiti>().isPickingGraffiti== true)
                {
                    WhichGraffiti();
                    graffitiUp = passingGraffiti;
                    Debug.Log("Switching graffiti up to" + passingGraffiti);
                    if (GameObject.Find("CustomizationVendor"))
                            GameObject.Find("CustomizationVendor").GetComponent<PickingGraffiti>().isPickingGraffiti = false;
                    
                    SaveManager.Instance.state.ariGraffitiSlotUp1 = graffitiUp.gameObject.name;
                    SaveManager.Instance.Save();
                    RecalculateGraffitiDisplay();
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
            if (GameObject.Find("CustomizationVendor"))
            {
                if (GameObject.Find("CustomizationVendor").GetComponent<PickingGraffiti>().isPickingGraffiti== true)
                {
                    WhichGraffiti();
                    graffitiRight = passingGraffiti;
                    Debug.Log("Switching graffiti right to" + passingGraffiti);
                    if (GameObject.Find("CustomizationVendor"))
                            GameObject.Find("CustomizationVendor").GetComponent<PickingGraffiti>().isPickingGraffiti = false;

                    
                    SaveManager.Instance.state.ariGraffitiSlotRight2 = graffitiRight.gameObject.name;
                    SaveManager.Instance.Save();
                    RecalculateGraffitiDisplay();
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
            if (GameObject.Find("CustomizationVendor"))
            {
                if (GameObject.Find("CustomizationVendor").GetComponent<PickingGraffiti>().isPickingGraffiti== true)
                {
                    WhichGraffiti();
                    graffitiLeft = passingGraffiti;
                    Debug.Log("Switching graffiti left to" + passingGraffiti);
                    if (GameObject.Find("CustomizationVendor"))
                            GameObject.Find("CustomizationVendor").GetComponent<PickingGraffiti>().isPickingGraffiti = false;

                    
                    SaveManager.Instance.state.ariGraffitiSlotLeft4 = graffitiLeft.gameObject.name;
                    Debug.Log("the save state string for left is" + SaveManager.Instance.state.ariGraffitiSlotLeft4);
                    SaveManager.Instance.Save();
                    RecalculateGraffitiDisplay();
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

     
    public void RecalculateGraffitiDisplay ()
    {
        Debug.Log("recalculating sprite display");
        upDisplay = GameObject.Find("Up");
        rightDisplay = GameObject.Find("Right");
        downDisplay = GameObject.Find("Down");
        leftDisplay = GameObject.Find("Left");

        RecalcSprite(SaveManager.Instance.state.ariGraffitiSlotLeft4, leftDisplay);
        RecalcSprite(SaveManager.Instance.state.ariGraffitiSlotUp1, upDisplay);
        RecalcSprite(SaveManager.Instance.state.ariGraffitiSlotDown3, downDisplay);
        RecalcSprite(SaveManager.Instance.state.ariGraffitiSlotRight2, rightDisplay);

        if(GameObject.Find("SpriteButtons"))
            fadeGroup = GameObject.Find("SpriteButtons").GetComponent<CanvasGroup>();
  
    }

    private void RecalcSprite(string saveStateInstance, GameObject displaySprite)
    {


        Debug.Log("the save string was " + saveStateInstance + " and the display sprite was " + displaySprite);
        if(saveStateInstance == "Decal_1")
            displaySprite.GetComponent<Image>().sprite = graffitiSprites[0];
        else if(saveStateInstance == "Decal_2")
            displaySprite.GetComponent<Image>().sprite = graffitiSprites[1];
        else if(saveStateInstance == "Decal_3")
            displaySprite.GetComponent<Image>().sprite = graffitiSprites[2];
        else if(saveStateInstance == "Decal_4")
            displaySprite.GetComponent<Image>().sprite = graffitiSprites[3];
        else if(saveStateInstance == "Decal_5")
            displaySprite.GetComponent<Image>().sprite = graffitiSprites[4];
        else if(saveStateInstance == "Decal_6")
            displaySprite.GetComponent<Image>().sprite = graffitiSprites[5];
        else if(saveStateInstance == "Decal_7")
            displaySprite.GetComponent<Image>().sprite = graffitiSprites[6];
        else if(saveStateInstance == "Decal_8")
            displaySprite.GetComponent<Image>().sprite = graffitiSprites[7];
        else if(saveStateInstance == "Decal_9")
            displaySprite.GetComponent<Image>().sprite = graffitiSprites[8];
        else if(saveStateInstance == "Decal_10")
            displaySprite.GetComponent<Image>().sprite = graffitiSprites[9];


            Debug.Log(displaySprite);
    }

}
