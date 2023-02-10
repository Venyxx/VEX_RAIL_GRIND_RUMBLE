using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InfoScreen : MonoBehaviour
{

    public static bool isOpen;
    string currentTab;
    public GameObject infoScreen;
    public GameObject topBar;
    public GameObject mapTab;
    public GameObject missionsTab;
    public GameObject progressTab;
    public GameObject intelTab;
    public Animator topBarAnim;
    public Animator bottomBarAnim;
    public Animator backgroundAnim;
    public Animator mapAnim;
    public Animator missionsAnim;
    public Animator progressAnim;

    AudioSource audioUI;
    public AudioClip backSound;
    public AudioClip nextSound;
    public AudioClip selectSound;

    
    void Start()
    {
        isOpen = false;
        audioUI = GetComponent<AudioSource>();
    }

    public void OpenInfoButtonPressed(InputAction.CallbackContext context)
    {
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
        audioUI.clip = selectSound;
        audioUI.Play(0);

        infoScreen.SetActive(true);
        mapTab.SetActive(true);
        currentTab = "Map";
        isOpen = true;

        yield return new WaitForSeconds(0.75f);

        //Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    IEnumerator CloseInfoScreen()
    {
        audioUI.clip = backSound;
        audioUI.Play(0);

        mapTab.SetActive(false);
        missionsTab.SetActive(false);
        progressTab.SetActive(false);
        intelTab.SetActive(false);

        //Time.timeScale = 1f;
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
        audioUI.clip = nextSound;
        audioUI.Play(0);
        StartCoroutine(OpenMap());
    }
    IEnumerator OpenMap()
    {
        if (currentTab == "Missions")
        {
            missionsAnim.SetTrigger("Close");
        }

        if (currentTab == "Progress")
        {
            progressAnim.SetTrigger("Close");
        }

        yield return new WaitForSeconds(0.3f);
        mapTab.SetActive(true);
        missionsTab.SetActive(false);
        progressTab.SetActive(false);
        intelTab.SetActive(false);
        currentTab = "Map";
    }

    public void OpenMissionsTab()
    {
        audioUI.clip = nextSound;
        audioUI.Play(0);
        StartCoroutine(OpenMissions());
    }
    IEnumerator OpenMissions()
    {
        if (currentTab == "Map")
        {
            mapAnim.SetTrigger("Close");
        }

        if (currentTab == "Progress")
        {
            progressAnim.SetTrigger("Close");
        }

        yield return new WaitForSeconds(0.3f);

        mapTab.SetActive(false);
        missionsTab.SetActive(true);
        progressTab.SetActive(false);
        intelTab.SetActive(false);
        currentTab = "Missions";
    }

    public void OpenProgressTab()
    {
        audioUI.clip = nextSound;
        audioUI.Play(0);
        StartCoroutine(OpenProgress());
    }
    IEnumerator OpenProgress()
    {
        if (currentTab == "Map")
        {
            mapAnim.SetTrigger("Close");
        }

        if (currentTab == "Missions")
        {
            missionsAnim.SetTrigger("Close");
        }

        yield return new WaitForSeconds(0.3f);

        mapTab.SetActive(false);
        missionsTab.SetActive(false);
        progressTab.SetActive(true);
        intelTab.SetActive(false);
        currentTab = "Progress";
    }

    public void OpenIntelTab()
    {
        mapTab.SetActive(false);
        missionsTab.SetActive(false);
        progressTab.SetActive(false);
        intelTab.SetActive(true);
        currentTab = "Intel";
    }
}
