using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PauseMenu : MonoBehaviour
{

    public string mainMenuScene;
    public GameObject pauseMenu;
    public GameObject pauseSettings;
    public GameObject settingsMainPage;
    public GameObject pauseFirstButton;
    public GameObject settingsFirstButton;
    public GameObject settingsClosedButton;

    public GameObject audioScreen;
    public GameObject controlsScreen;
    public GameObject graphicsScreen;
    public GameObject accessibilityScreen;

    public GameObject speedometerREF;

    public GameObject questWindow;
    public GameObject acceptQuestButton;
    public GameObject denyQuestButton;

    //Added to turn reticle off when paused
    GameObject grappleDetectorREF;
    Reticle reticleScript;

    //Added for videos
    public GameObject pauseVideo;
    public GameObject closePauseVideo;
    public GameObject settingsOpenVideo;
    public GameObject settingsCloseVideo;
    public GameObject wipe;

    //For audio clips
    AudioSource audioSource;
    public AudioClip pauseOpen;
    private InfoScreen infoScreen;

    //For cutscenes
    CutscenePlayer cutscenePlayerREF;
    
    

    public static bool isPaused;
    LoadingScreen loading;

    //Quest Window Check
    

    void Start()
    {
        isPaused = false;

        grappleDetectorREF = GameObject.Find("GrappleDetector");
        reticleScript = grappleDetectorREF.GetComponent<Reticle>();

        audioSource = GetComponent<AudioSource>();
        infoScreen = GetComponent<InfoScreen>();
        loading = GetComponent<LoadingScreen>();

        cutscenePlayerREF = FindObjectOfType<CutscenePlayer>();
    }

    void Update()
    {
        //Assures controllers always function on menus
        if (EventSystem.current.currentSelectedGameObject == null && isPaused == true && pauseMenu.activeInHierarchy)
        {
            pauseFirstButton.GetComponent<Button>().Select();
        }

        if (EventSystem.current.IsPointerOverGameObject())
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    public void PauseGamePressed(InputAction.CallbackContext context)
    {
        if (!context.started || InfoScreen.isOpen == true || questWindow.activeInHierarchy || SceneManager.GetActiveScene().name == "MainMenu" || cutscenePlayerREF.cutscenePlaying == true ) return;
        
        if (isPaused)
        {
            StartCoroutine(ResumeDelay());
            //reticleScript.ReticleToggle(true);
        }
        else
        {
            StartCoroutine(PauseDelay());
            //PauseGame();
        }
    }

    public void ResumeButtonClicked()
    {
        StartCoroutine(ResumeDelay());
    }

    IEnumerator PauseDelay()
    {
        wipe.SetActive(true);
        yield return new WaitForSeconds(0.10f);
        wipe.SetActive(false);
        PauseGame();
    }

    IEnumerator ResumeDelay()
    {
        ResumeGame();
        closePauseVideo.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        wipe.SetActive(true);
        wipe.GetComponent<Animator>().CrossFade("PauseOutro", 0, 0);
        yield return new WaitForSeconds(0.2f);
        wipe.SetActive(false);
        
    }

    public void PauseGame()
    {
        infoScreen.PlaySoundUI(pauseOpen);

        isPaused = true;
        pauseMenu.SetActive(true);
        pauseVideo.SetActive(true);
        speedometerREF.SetActive(false);
        reticleScript.ReticleToggle(false);
        Debug.Log("Unpause");
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        EventSystem.current.SetSelectedGameObject(null);
        if (EventSystem.current.IsPointerOverGameObject())
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(pauseFirstButton);

        }
    }

    public void ActivateQuestWindow()
    {
        isPaused = true;
        questWindow.SetActive(true);
        reticleScript.ReticleToggle(false);
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
            
        EventSystem.current.SetSelectedGameObject(null); 
        EventSystem.current.SetSelectedGameObject(acceptQuestButton);
    }

    public void ResumeGame()
    {
        infoScreen.PlaySoundUI(infoScreen.backSound);

        isPaused = false;
        pauseMenu.SetActive(false);
        pauseSettings.SetActive(false);
        audioScreen.SetActive(false);
        controlsScreen.SetActive(false);
        graphicsScreen.SetActive(false);
        accessibilityScreen.SetActive(false);
        questWindow.SetActive(false);
        speedometerREF.SetActive(true);
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    
    public void OpenPauseSettings()
    {
        pauseSettings.SetActive(true);
        pauseMenu.SetActive(false);
        settingsMainPage.SetActive(true);

        //Animation
        settingsOpenVideo.SetActive(true);

        //Sound
        infoScreen.PlaySoundUI(infoScreen.selectSound);

        EventSystem.current.SetSelectedGameObject(null);
        if (EventSystem.current.IsPointerOverGameObject())
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(settingsFirstButton);
        }

    }

    public void ClosePauseSettings()
    {
        pauseSettings.SetActive(false);
        pauseMenu.SetActive(true);

        //Animation
        settingsCloseVideo.SetActive(true);

        //EventSystem.current.SetSelectedGameObject(null); 
        //EventSystem.current.SetSelectedGameObject(settingsClosedButton);
        if (EventSystem.current.IsPointerOverGameObject())
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(settingsClosedButton);
        }

        infoScreen.PlaySoundUI(infoScreen.backSound);
    }

    public void ReturnToMain()
    {
        loading.LoadOutStart(mainMenuScene);
        //ResumeGame();
        Time.timeScale = 1f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        //SceneManager.LoadScene(mainMenuScene);
    }

    //B Button on Controller
    public void ControllerResume(InputAction.CallbackContext context)
    {
        if (!context.started || PauseMenu.isPaused == false || pauseMenu.activeInHierarchy == false) return;

        if (context.started && pauseMenu.activeInHierarchy == true && settingsCloseVideo.activeInHierarchy == false)
        {
            StartCoroutine(ResumeDelay());
        } 



    }

}

