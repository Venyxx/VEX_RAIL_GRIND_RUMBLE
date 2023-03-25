using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CustomScreen : MonoBehaviour
{
   public static bool isOpen;
    string currentTab;
    public GameObject customsScreen;
    public GameObject topBar;
    public GameObject topAndBottomTab;
    public GameObject socksAndAccessoryTab;
    public GameObject skateTab;

    //Animators
    public Animator topBarAnim;
    public Animator bottomBarAnim;
    public Animator backgroundAnim;
    public Animator topBottomAnim;
    public Animator sockAccAnim;
    public Animator skateAnim;

    //Sounds
    AudioSource audioUI;
    public AudioClip backSound;
    public AudioClip nextSound;
    public AudioClip selectSound;

    //Top Tabs
    public Button topAndBottomButton;
    public Button sockAndAccessoryButton;
    public Button skateButton;
    public Sprite tabSelected;
    public Sprite tabNotSelected;

    //Selection Handling
    public Button MainMissionButton;
    public Button Act1Button;

    //Quest Window Check
    //GameObject questWindow;



    
    void Start()
    {
        isOpen = false;
        audioUI = GetComponent<AudioSource>();
        //questWindow = gameObject.transform.Find("QuestParent").transform.Find("QuestWindow").gameObject;
    }

    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            if (socksAndAccessoryTab.activeInHierarchy)
            {
                MainMissionButton.Select();
            }

            if (skateTab.activeInHierarchy)
            {
                Act1Button.Select();
            }
        }
    }

    public void OpenCustomButtonPressed(InputAction.CallbackContext context)
    {
        if (!context.started || SceneManager.GetActiveScene().name == "MainMenu") return;

        if (context.started)
        {
            if (isOpen == false && PauseMenu.isPaused == false)
            {
                StartCoroutine(OpenCustomScreen());
            } else {
                StartCoroutine(CloseCustomScreen());
            }
        }
    }

    public void BackCustomPressed (InputAction.CallbackContext context)
    {
        if (context.started && isOpen == true)
        {
            StartCoroutine(CloseCustomScreen());
        }
    }

    public void CustomNextTabPressed(InputAction.CallbackContext context)
    {
        if (context.started && isOpen == true)
        {
            NextTab();
            //Debug.Log("Next Tab");
        }
    }

    public void CustomPreviousTabPressed(InputAction.CallbackContext context)
    {
        if (context.started && isOpen == true)
        {
            PreviousTab();
            //Debug.Log("Next Tab");
        }
    }

    IEnumerator OpenCustomScreen()
    {
        PlaySoundUI(selectSound);

        customsScreen.SetActive(true);
        topAndBottomButton.gameObject.SetActive(true);
        sockAndAccessoryButton.gameObject.SetActive(true);
        skateButton.gameObject.SetActive(true);
        OpenTB();
        isOpen = true;

        yield return new WaitForSeconds(0.75f);

        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    IEnumerator CloseCustomScreen()
    {
        PlaySoundUI(backSound);

        topAndBottomTab.SetActive(false);
        socksAndAccessoryTab.SetActive(false);
        skateTab.SetActive(false);

        topAndBottomButton.gameObject.SetActive(false);
        sockAndAccessoryButton.gameObject.SetActive(false);
        skateButton.gameObject.SetActive(false);

        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        topBarAnim.SetTrigger("Close");
        bottomBarAnim.SetTrigger("Close");
        backgroundAnim.SetTrigger("Close");

        yield return new WaitForSeconds(0.75f);

        customsScreen.SetActive(false);
        isOpen = false;
    }

    //Bumper Switch
    void NextTab()
    {
        if (currentTab == "TB")
        {
            OpenSoAccTab();
        } else if (currentTab == "SoAc")
        {
            OpenSkateTab();
        } else if (currentTab == "Sk")
        {
            OpenTBTab();
        } 

    }

    void PreviousTab()
    {
        if (currentTab == "TB")
        {
            OpenSkateTab();
        } else if (currentTab == "SoAc")
        {
            OpenTBTab();
        } else if (currentTab == "Sk")
        {
            OpenSoAccTab();
        } 
    }

    //Click Events

    public void OpenTBTab()
    {
        PlaySoundUI(nextSound);
        OpenTB();
    }
    void OpenTB()
    {
        topAndBottomButton.GetComponent<Image>().sprite = tabSelected;
        sockAndAccessoryButton.GetComponent<Image>().sprite = tabNotSelected;
        skateButton.GetComponent<Image>().sprite = tabNotSelected;

        if (currentTab == "SoAc")
        {
            sockAccAnim.SetTrigger("Close");
        }

        if (currentTab == "Sk")
        {
            skateAnim.SetTrigger("Close");
        }

        topAndBottomTab.SetActive(true);
        currentTab = "TB";
    }

    public void OpenSoAccTab()
    {
        PlaySoundUI(nextSound);
        OpenSoAcc();
    }
    void OpenSoAcc()
    {
        topAndBottomButton.GetComponent<Image>().sprite = tabNotSelected;
        sockAndAccessoryButton.GetComponent<Image>().sprite = tabSelected;
        skateButton.GetComponent<Image>().sprite = tabNotSelected;

        if (currentTab == "TB")
        {
            topBottomAnim.SetTrigger("Close");
        }

        if (currentTab == "Sk")
        {
            skateAnim.SetTrigger("Close");
        }

        socksAndAccessoryTab.SetActive(true);
        currentTab = "SoAc";

        MainMissionButton.Select();
    }

    public void OpenSkateTab()
    {
        PlaySoundUI(nextSound);
        OpenSkate();
    }
    void OpenSkate()
    {
        topAndBottomButton.GetComponent<Image>().sprite = tabNotSelected;
        sockAndAccessoryButton.GetComponent<Image>().sprite = tabNotSelected;
        skateButton.GetComponent<Image>().sprite = tabSelected;

        if (currentTab == "TB")
        {
            topBottomAnim.SetTrigger("Close");
        }

        if (currentTab == "SoAc")
        {
            sockAccAnim.SetTrigger("Close");
        }

        
        skateTab.SetActive(true);
        currentTab = "Sk";

        Act1Button.Select();
    }

    public void PlaySoundUI(AudioClip clip)
    {
        audioUI.clip = clip;
        audioUI.Play(0);
    }

    public void TabOpened(string tabOpened)
    {
        if (tabOpened == "TB")
        {
            socksAndAccessoryTab.SetActive(false);
            skateTab.SetActive(false);
        } else if (tabOpened == "SoAc")
        {
            topAndBottomTab.SetActive(false);
            skateTab.SetActive(false);
        } else if (tabOpened == "Sk")
        {
            topAndBottomTab.SetActive(false);
            socksAndAccessoryTab.SetActive(false);
        }
    }
}
