using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class CustomizationMEnu : MonoBehaviour
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

    public Button custFirstButton, custSecButton, custThirdButton, custFourthButton;

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
    }


    
    public void OpenInfoButtonPressed(InputAction.CallbackContext context)
    {
        if (!context.started ||  SceneManager.GetActiveScene().name == "MainMenu") return;

        if (context.started)
        {
            if (isOpen == false && PauseMenu.isPaused == false)
            {
                StartCoroutine(OpenInfoScreen());
                custFirstButton.Select();
            } else {
                StartCoroutine(CloseInfoScreen());
            }
        }
    }

    public void BackPressed (InputAction.CallbackContext context)
    {
        if (context.started && isOpen == true)
        {
            StartCoroutine(CloseInfoScreen());
        }
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

    IEnumerator OpenInfoScreen()
    {
        PlaySoundUI(selectSound);

        infoScreen.SetActive(true);
        mapButton.gameObject.SetActive(true);
        missionsButton.gameObject.SetActive(true);
        progressButton.gameObject.SetActive(true);
        graffitiButton.gameObject.SetActive(true);

        OpenMap();
        isOpen = true;

        yield return new WaitForSeconds(0.75f);

        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    IEnumerator CloseInfoScreen()
    {
        PlaySoundUI(backSound);

        mapTab.SetActive(false);
        missionsTab.SetActive(false);
        progressTab.SetActive(false);
        graffitiTab.SetActive(false);
        DisplayTab.SetActive(false);

        mapButton.gameObject.SetActive(false);
        missionsButton.gameObject.SetActive(false);
        progressButton.gameObject.SetActive(false);
        graffitiButton.gameObject.SetActive(false);

        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        //topBarAnim.SetTrigger("Close");
        //bottomBarAnim.SetTrigger("Close");
        //backgroundAnim.SetTrigger("Close");

        yield return new WaitForSeconds(0.75f);

        infoScreen.SetActive(false);
        isOpen = false;
    }

    //Bumper Switch
    void NextTab()
    {
        if (currentTab == "Map")
        {
            OpenMissionsTab();
            custFirstButton.Select();
        } else if (currentTab == "Missions")
        {
            OpenProgressTab();
            custSecButton.Select();
        } else if (currentTab == "Progress")
        {
            OpenGraffitiTab();
            custThirdButton.Select();
        } else if (currentTab == "Graffiti")
        {
            OpenMapTab();
            custFourthButton.Select();
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
