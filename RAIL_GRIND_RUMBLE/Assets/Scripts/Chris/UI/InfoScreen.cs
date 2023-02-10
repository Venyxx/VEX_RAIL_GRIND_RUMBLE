using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InfoScreen : MonoBehaviour
{

    bool isOpen;
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

    
    void Start()
    {
        isOpen = false;
    }

    public void OpenInfoButtonPressed(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (isOpen == false)
            {
                StartCoroutine(OpenInfoScreen());
            } else {
                StartCoroutine(CloseInfoScreen());
            }
        }
    }

    public void NextTabPressed(InputAction.CallbackContext context)
    {
        if (context.started && isOpen == true)
        {
            NextTab();
        }
    }

    IEnumerator OpenInfoScreen()
    {
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
            mapTab.SetActive(false);
            missionsTab.SetActive(true);
            mapAnim.SetTrigger("Close");
            currentTab = "Missions";
        }

        if (currentTab == "Missions")
        {
            missionsTab.SetActive(false);
            progressTab.SetActive(true);
            currentTab = "Progress";
        }

        if (currentTab == "Progress")
        {
            progressTab.SetActive(false);
            intelTab.SetActive(true);
            currentTab = "Intel";
        }

        if (currentTab == "Intel")
        {
            intelTab.SetActive(false);
            mapTab.SetActive(true);
            currentTab = "Map";
        }
    }

    //Click Events

    public void OpenMapTab()
    {
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
