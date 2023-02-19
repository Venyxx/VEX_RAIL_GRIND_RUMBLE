using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class InfoScreen : MonoBehaviour
{

    public static bool isOpen;
    string currentTab;
    public GameObject infoScreen;
    public GameObject topBar;
    public GameObject mapTab;
    public GameObject missionsTab;
    public GameObject progressTab;

    //Animators
    public Animator topBarAnim;
    public Animator bottomBarAnim;
    public Animator backgroundAnim;
    public Animator mapAnim;
    public Animator missionsAnim;
    public Animator progressAnim;

    //Sounds
    AudioSource audioUI;
    public AudioClip backSound;
    public AudioClip nextSound;
    public AudioClip selectSound;

    //Top Tabs
    public Button mapButton;
    public Button missionsButton;
    public Button progressButton;
    public Sprite tabSelected;
    public Sprite tabNotSelected;

    //Selection Handling
    public Button MainMissionButton;
    public Button Act1Button;

    //Quest Window Check
    GameObject questWindow;



    
    void Start()
    {
        isOpen = false;
        audioUI = GetComponent<AudioSource>();
        questWindow = GameObject.Find("QuestWindow");
    }

    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            if (missionsTab.activeInHierarchy)
            {
                MainMissionButton.Select();
            }

            if (progressTab.activeInHierarchy)
            {
                Act1Button.Select();
            }
        }
    }

    public void OpenInfoButtonPressed(InputAction.CallbackContext context)
    {
        if (!context.started || questWindow.activeInHierarchy || SceneManager.GetActiveScene().name == "MainMenu") return;

        if (context.started)
        {
            if (isOpen == false && PauseMenu.isPaused == false)
            {
                StartCoroutine(OpenInfoScreen());
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

        mapButton.gameObject.SetActive(false);
        missionsButton.gameObject.SetActive(false);
        progressButton.gameObject.SetActive(false);

        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        topBarAnim.SetTrigger("Close");
        bottomBarAnim.SetTrigger("Close");
        backgroundAnim.SetTrigger("Close");

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
        } else if (currentTab == "Missions")
        {
            OpenProgressTab();
        } else if (currentTab == "Progress")
        {
            OpenMapTab();
        } 

    }

    void PreviousTab()
    {
        if (currentTab == "Map")
        {
            OpenProgressTab();
        } else if (currentTab == "Missions")
        {
            OpenMapTab();
        } else if (currentTab == "Progress")
        {
            OpenMissionsTab();
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

        if (currentTab == "Missions")
        {
            missionsAnim.SetTrigger("Close");
        }

        if (currentTab == "Progress")
        {
            progressAnim.SetTrigger("Close");
        }

        mapTab.SetActive(true);
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

        if (currentTab == "Map")
        {
            mapAnim.SetTrigger("Close");
        }

        if (currentTab == "Progress")
        {
            progressAnim.SetTrigger("Close");
        }

        missionsTab.SetActive(true);
        currentTab = "Missions";

        MainMissionButton.Select();
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

        if (currentTab == "Map")
        {
            mapAnim.SetTrigger("Close");
        }

        if (currentTab == "Missions")
        {
            missionsAnim.SetTrigger("Close");
        }

        
        progressTab.SetActive(true);
        currentTab = "Progress";

        Act1Button.Select();
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
