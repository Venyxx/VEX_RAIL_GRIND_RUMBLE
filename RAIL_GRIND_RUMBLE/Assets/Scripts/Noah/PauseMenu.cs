using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
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



    public bool isPaused;

    void Start()
    {
        grappleDetectorREF = GameObject.Find("GrappleDetector");
        reticleScript = grappleDetectorREF.GetComponent<Reticle>();
    }

    void Update()
    {
        /*if (Input.GetKeyDown("p"))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                isPaused = true;
                pauseMenu.SetActive(true);
                Time.timeScale = 0f;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }*/
    }

    public void PauseGamePressed(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        
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
            EventSystem.current.SetSelectedGameObject(pauseFirstButton);
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

        EventSystem.current.SetSelectedGameObject(null); 
        EventSystem.current.SetSelectedGameObject(settingsFirstButton);

    }

    public void ClosePauseSettings()
    {
        pauseSettings.SetActive(false);
        pauseMenu.SetActive(true);

        //Animation
        settingsCloseVideo.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null); 
        EventSystem.current.SetSelectedGameObject(settingsClosedButton);
    }

    public void ReturnToMain()
    {
        SceneManager.LoadScene(mainMenuScene);
        Time.timeScale = 1f;
    }

}

