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
    public GameObject galleryTab;

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
    public Button galleryButton;
    public Sprite tabSelected;
    public Sprite tabNotSelected;

    //Selection Handling
    public Button MainMissionButton;
    public Button Act1Button;

    //Quest Window Check
    GameObject questWindow;

    //Customization Screen
    [SerializeField] GameObject customVendorREF;
    GameObject customVendor;




    
    void Start()
    {
        isOpen = false;
        audioUI = GetComponent<AudioSource>();
        questWindow = gameObject.transform.Find("QuestParent").transform.Find("QuestWindow").gameObject;
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

            if (galleryTab.activeInHierarchy)
            {
                if (galleryTab.GetComponent<GalleryTab>().newspapers[0].activeInHierarchy)
                {
                    galleryTab.GetComponent<GalleryTab>().newspapers[0].GetComponent<Button>().Select();
                } else {
                    galleryButton.Select();
                }
                
            }
        }

        /// DEBUG BEYOND THIS POINT ///
        //Temp
        /*if (Input.GetKeyDown(KeyCode.E))
        {
            if (isOpen)
            {
                StartCoroutine(CloseInfoScreen());
            }
        }*/

        if (Input.GetKeyDown(KeyCode.M))
        {
            SetMission(1, 5);
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
            if (!galleryTab.GetComponent<GalleryTab>().paperIsOpen)
            {
                StartCoroutine(CloseInfoScreen());
            } else {
                galleryTab.GetComponent<GalleryTab>().ClosePaper();
            }
            
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

    public IEnumerator OpenInfoScreen()
    {
        PlaySoundUI(selectSound);

        infoScreen.SetActive(true);

        //Set Language of Tabs
        if (SpanishMode.spanishMode)
        {
            missionsTab.GetComponent<NewMissionTab>().SetLanguage("Spanish");
        } else {
            missionsTab.GetComponent<NewMissionTab>().SetLanguage("English");
        }
        progressTab.GetComponent<ProgressTab>().SetButtons();
        galleryTab.GetComponent<GalleryTab>().SetTitles();

        //mapButton.gameObject.SetActive(true);
        missionsButton.gameObject.SetActive(true);
        progressButton.gameObject.SetActive(true);
        galleryButton.gameObject.SetActive(true);
        //OpenMap();
        OpenMissions();
        isOpen = true;

        yield return new WaitForSeconds(0.75f);

        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        //Spawn custom vendor
        Transform playerTransform = GameObject.FindWithTag("PlayerObject").transform;
        //customVendor = Instantiate(customVendorREF, new Vector3(playerTransform.position.x + 1, playerTransform.position.y, playerTransform.position.z), Quaternion.identity);
    }

    public IEnumerator CloseInfoScreen()
    {
        PlaySoundUI(backSound);

        progressTab.GetComponent<ProgressTab>().ActivateTab(0);

        mapTab.SetActive(false);
        missionsTab.SetActive(false);
        progressTab.SetActive(false);
        galleryTab.SetActive(false);

        mapButton.gameObject.SetActive(false);
        missionsButton.gameObject.SetActive(false);
        progressButton.gameObject.SetActive(false);
        galleryButton.gameObject.SetActive(false);

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
        /*if (currentTab == "Map")
        {
            OpenMissionsTab();
        } else*/ if (currentTab == "Missions")
        {
            OpenProgressTab();
        } else if (currentTab == "Progress")
        {
            OpenGalleryTab();
        } else if (currentTab == "Gallery")
        {
            OpenMissionsTab();
        }

    }

    void PreviousTab()
    {
        /*if (currentTab == "Map")
        {
            OpenProgressTab();
        } else*/ if (currentTab == "Missions")
        {
            OpenGalleryTab();
        } else if (currentTab == "Progress")
        {
            OpenMissionsTab();
        } else if (currentTab == "Gallery")
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
        galleryButton.GetComponent<Image>().sprite = tabNotSelected;

        if (currentTab == "Gallery")
        {
            galleryTab.SetActive(false);
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
        galleryButton.GetComponent<Image>().sprite = tabNotSelected;

        if (currentTab == "Gallery")
        {
            galleryTab.SetActive(false);
        }

        if (currentTab == "Missions")
        {
            missionsAnim.SetTrigger("Close");
        }

        
        progressTab.SetActive(true);
        currentTab = "Progress";

        Act1Button.Select();
    }

    public void OpenGalleryTab()
    {
        PlaySoundUI(nextSound);
        OpenGallery();
    }

    void OpenGallery()
    {
        mapButton.GetComponent<Image>().sprite = tabNotSelected;
        missionsButton.GetComponent<Image>().sprite = tabNotSelected;
        progressButton.GetComponent<Image>().sprite = tabNotSelected;
        galleryButton.GetComponent<Image>().sprite = tabSelected;

        if (currentTab == "Progress")
        {
            progressAnim.SetTrigger("Close");
        }

        if (currentTab == "Missions")
        {
            missionsAnim.SetTrigger("Close");
        }

        //Temp
        missionsTab.SetActive(false);
        progressTab.SetActive(false);

        
        galleryTab.SetActive(true);
        currentTab = "Gallery";
    }

    public void PlaySoundUI(AudioClip clip)
    {
        audioUI.clip = clip;
        audioUI.Play(0);
    }

    public void TabOpened(string tabOpened)
    {
        if (tabOpened == "Missions")
        {
            progressTab.SetActive(false);
            galleryTab.SetActive(false);
        } else if (tabOpened == "Progress")
        {
            missionsTab.SetActive(false);
            galleryTab.SetActive(false);
        } else if (tabOpened == "Gallery")
        {
            missionsTab.SetActive(false);
            progressTab.SetActive(false);
        }
    }

    public void NewspaperPassthrough(int paperNum)
    {
        galleryTab.GetComponent<GalleryTab>().AddNewspaper(paperNum);
    }

    public void SetMission(int actNum, int missionNum)
    {
        missionsTab.GetComponent<NewMissionTab>().SetMission(actNum, missionNum);
    }
}
