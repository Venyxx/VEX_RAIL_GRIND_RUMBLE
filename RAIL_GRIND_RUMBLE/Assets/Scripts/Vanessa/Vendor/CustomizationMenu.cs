using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using Unity.VisualScripting.Dependencies.NCalc;

public class CustomizationMenu : MonoBehaviour
{
    
    public static bool isOpen;
    string currentTab;
    public GameObject infoScreen;
    public GameObject topBar;
    public GameObject mapTab;
    public GameObject missionsTab;
    public GameObject progressTab;
    public GameObject graffitiTab;
    public GameObject DisplayTab;
    private ThirdPersonMovement movementScriptREF;

    AudioSource audioUI;
    public AudioClip backSound;
    public AudioClip nextSound;
    public AudioClip selectSound;

    public Button mapButton;
    public Button missionsButton;
    public Button progressButton;
    public Button graffitiButton;
    public Sprite tabSelected;
    public Sprite tabNotSelected;
    private ThirdPersonMovement ThirdPersonMovementREF;
    public InputHandler playerActions { get; private set; }


    public Vector2 moveInput;
    public Button custFirstButton, custSecButton, custThirdButton, custFourthButton;
    private GameObject previewCamera;
    private GameObject previewCamItself;

    private Graffiti graffitiREF;
    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
        audioUI = GetComponent<AudioSource>();
        mapTab.SetActive(true);
        missionsTab.SetActive(false);
        progressTab.SetActive(false);
        graffitiTab.SetActive(false);
        DisplayTab.SetActive(true);
        graffitiREF = GameObject.Find("SprayCanTransform").GetComponent<Graffiti>();
        ThirdPersonMovementREF = GameObject.Find("playerPrefab").GetComponent<ThirdPersonMovement>();

        previewCamera = GameObject.Find("CharacterPreviewBackgr");
         if (previewCamera)
            previewCamera.SetActive(false);

        previewCamItself = GameObject.Find ("CharacterPreviewCamera");
          if (previewCamItself)
            previewCamItself.SetActive(false);

        playerActions = new InputHandler();
        playerActions.Player.Enable();
        
    }

    private void Update()
    {
       if (EventSystem.current.currentSelectedGameObject == null)
        {
            //Debug.Log("event system was null, setting");
            if (mapTab.activeInHierarchy)
            {
                custFirstButton.Select();
                custFirstButton.OnSelect(null);
            }

            if (missionsTab.activeInHierarchy)
            {
                custSecButton.Select();
                custSecButton.OnSelect(null);
            }

            if (progressTab.activeInHierarchy)
            {
                custThirdButton.Select();
                 custThirdButton.OnSelect(null);
            }

            if (graffitiTab.activeInHierarchy)
            {
                custFourthButton.Select();
                custFourthButton.OnSelect(null);
            }

           
        }


           
    }

    
    public void OpenCustomizationButtonPressed(InputAction.CallbackContext context)
    {
        if (!context.started ||  SceneManager.GetActiveScene().name == "MainMenu") return;

        if (context.started)
        {
            Debug.Log("checking for infosc");
            /*if (isOpen == false && PauseMenu.isPaused == false && InfoScreen.isOpen)
            {
                
                //StartCoroutine(OpenInfoScreen());
                
                
            } else */ if (isOpen){
                StartCoroutine(ReturnToInfoScreen());
            }
        }
    }

    //this version exists so it cna be attached to a button
    public void OpenCustomizationButtonPressedLoc()
    {
       

            if (isOpen == false && PauseMenu.isPaused == false && InfoScreen.isOpen)
            {
                
                StartCoroutine(OpenCustomizationScreen()); 
                
            } else {
                StartCoroutine(ReturnToInfoScreen());
                
            }

    }

    public void BackPressed (InputAction.CallbackContext context)
    {
        if (context.started && isOpen == true)
        {
            StartCoroutine(ReturnToInfoScreen());
        }
    }
    
    //for button attachment
    public void BackPressedLOC ()
    {
        if (isOpen == true)
        {
            StartCoroutine(ReturnToInfoScreen());
        }
    }

    public void CloseAllTheWay(InputAction.CallbackContext context)
    {
        StartCoroutine(CloseCustomizationScreenAllTheWay());
    }

    public void NextTabPressed(InputAction.CallbackContext context)
    {
        if (context.started && isOpen == true)
        {
            NextTab();
            //Debug.Log("Next Tab");
        }
    }

    public void PreviousTabPressed(InputAction.CallbackContext context)
    {
        if (context.started && isOpen == true)
        {
            PreviousTab();
            //Debug.Log("Next Tab");
        }
    }

     public void OpenCustomization(InputAction.CallbackContext context)
    {
        if (context.started && isOpen == false)
        {
            StartCoroutine(OpenCustomizationScreen());
            Time.timeScale = 0f;
        }
    }

    IEnumerator OpenCustomizationScreen()
    {
        //Debug.Log("ienum OIS" + InfoScreen.isOpen);
        if (InfoScreen.isOpen)
        {
            PlaySoundUI(selectSound);

            infoScreen.SetActive(true);
            mapButton.gameObject.SetActive(true);
            missionsButton.gameObject.SetActive(true);
            progressButton.gameObject.SetActive(true);
            graffitiButton.gameObject.SetActive(true);
            previewCamera.SetActive(true);
            previewCamItself.SetActive(true);

            OpenMap();
            isOpen = true;
            //Debug.Log("just ran toopen");

            

            

            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            yield return new WaitForSeconds(0.75f);

            
        } 
        else
        {
            //Debug.Log("ienum OIS discard");
            InfoScreen infoScreen = GameObject.Find("canvasPrefab").GetComponent<InfoScreen>();
            infoScreen.StartCoroutine(infoScreen.CloseInfoScreen());
        }  
                    
        
    }

    IEnumerator ReturnToInfoScreen()
    {
        //PlaySoundUI(backSound);

        mapTab.SetActive(false);
        missionsTab.SetActive(false);
        progressTab.SetActive(false);
        graffitiTab.SetActive(false);
        DisplayTab.SetActive(false);

        mapButton.gameObject.SetActive(false);
        missionsButton.gameObject.SetActive(false);
        progressButton.gameObject.SetActive(false);
        graffitiButton.gameObject.SetActive(false);
        previewCamera.SetActive(false);
        previewCamItself.SetActive(false);

        /*Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;*/
        isOpen = false;
        infoScreen.SetActive(false);

        gameObject.GetComponent<CustomizationVendor>().ResetOutfitToSaveState();
        /*movementScriptREF = GameObject.Find("playerPrefab").GetComponent<ThirdPersonMovement>();
        movementScriptREF.dialogueManager.freezePlayer = false;*/

        yield return new WaitForSeconds(0.75f);
        
    }
    
    IEnumerator CloseCustomizationScreenAllTheWay()
    {
        //PlaySoundUI(backSound);

        mapTab.SetActive(false);
        missionsTab.SetActive(false);
        progressTab.SetActive(false);
        graffitiTab.SetActive(false);
        DisplayTab.SetActive(false);

        mapButton.gameObject.SetActive(false);
        missionsButton.gameObject.SetActive(false);
        progressButton.gameObject.SetActive(false);
        graffitiButton.gameObject.SetActive(false);
        previewCamera.SetActive(false);
        previewCamItself.SetActive(false);

        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        isOpen = false;
        infoScreen.SetActive(false);

        gameObject.GetComponent<CustomizationVendor>().ResetOutfitToSaveState();
        movementScriptREF = GameObject.Find("playerPrefab").GetComponent<ThirdPersonMovement>();
        movementScriptREF.dialogueManager.freezePlayer = false;

        yield return new WaitForSeconds(0.75f);
        
    }

    //Bumper Switch
    void NextTab()
    {
        if (currentTab == "Map")
        {
            OpenMissionsTab();
            
        } else if (currentTab == "Missions")
        {
            OpenProgressTab();
           
        } else if (currentTab == "Progress")
        {
            OpenGraffitiTab();
        ;
        } else if (currentTab == "Graffiti")
        {
            OpenMapTab();
           
        }

    }

    void PreviousTab()
    {
        if (currentTab == "Map")
        {
            OpenGraffitiTab();
           
        } else if (currentTab == "Missions")
        {
            OpenMapTab();
         
        } else if (currentTab == "Progress")
        {
            OpenMissionsTab();
           
        } else if (currentTab == "Graffiti")
        {
            OpenProgressTab();
            
        }
    }

    //Click Events

    public void OpenMapTab()
    {
        PlaySoundUI(nextSound);
        OpenMap();
    }
    void OpenMap()
    {
        mapButton.GetComponent<Image>().sprite = tabSelected;
        missionsButton.GetComponent<Image>().sprite = tabNotSelected;
        progressButton.GetComponent<Image>().sprite = tabNotSelected;
        graffitiButton.GetComponent<Image>().sprite = tabNotSelected;

        mapTab.SetActive(true);
        missionsTab.SetActive(false);
        progressTab.SetActive(false);
        graffitiTab.SetActive(false);
        DisplayTab.SetActive(false);
        currentTab = "Map";
    }

    public void OpenMissionsTab()
    {
        PlaySoundUI(nextSound);
        OpenMissions();
    }

    void OpenMissions()
    {
        mapButton.GetComponent<Image>().sprite = tabNotSelected;
        missionsButton.GetComponent<Image>().sprite = tabSelected;
        progressButton.GetComponent<Image>().sprite = tabNotSelected;
        graffitiButton.GetComponent<Image>().sprite = tabNotSelected;

        
        mapTab.SetActive(false);
        missionsTab.SetActive(true);
        progressTab.SetActive(false);
        graffitiTab.SetActive(false);
        DisplayTab.SetActive(false);
        currentTab = "Missions";

        
    }

    public void OpenProgressTab()
    {
        PlaySoundUI(nextSound);
        OpenProgress();
    }
    void OpenProgress()
    {
        mapButton.GetComponent<Image>().sprite = tabNotSelected;
        missionsButton.GetComponent<Image>().sprite = tabNotSelected;
        progressButton.GetComponent<Image>().sprite = tabSelected;
        graffitiButton.GetComponent<Image>().sprite = tabNotSelected;

        

        
        mapTab.SetActive(false);
        missionsTab.SetActive(false);
        progressTab.SetActive(true);
        graffitiTab.SetActive(false);
        DisplayTab.SetActive(false);
        currentTab = "Progress";

       
    }

    public void OpenGraffitiTab()
    {
        PlaySoundUI(nextSound);
        OpenGraffiti();
        graffitiREF.RecalculateGraffitiDisplay();
    }
    void OpenGraffiti()
    {
        mapButton.GetComponent<Image>().sprite = tabNotSelected;
        missionsButton.GetComponent<Image>().sprite = tabNotSelected;
        progressButton.GetComponent<Image>().sprite = tabNotSelected;
        graffitiButton.GetComponent<Image>().sprite = tabSelected;

        Debug.Log("trying to open graffiti");

        
        mapTab.SetActive(false);
        missionsTab.SetActive(false);
        progressTab.SetActive(false);
        graffitiTab.SetActive(true);
        DisplayTab.SetActive(true);
        currentTab = "Graffiti";

       
    }

    public void PlaySoundUI(AudioClip clip)
    {
        audioUI.clip = clip;
        audioUI.Play(0);
    }

    public void TabOpened(string tabOpened)
    {
        if (tabOpened == "Map")
        {
            missionsTab.SetActive(false);
            progressTab.SetActive(false);
            
        } else if (tabOpened == "Missions")
        {
            mapTab.SetActive(false);
            progressTab.SetActive(false);
        } else if (tabOpened == "Progress")
        {
            mapTab.SetActive(false);
            missionsTab.SetActive(false);
        } 
    }
}
