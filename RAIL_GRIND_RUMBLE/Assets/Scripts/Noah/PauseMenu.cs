using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseMenu : MonoBehaviour
{

    public string mainMenuScene;
    public GameObject pauseMenu;
    public GameObject pauseSettings;
    public GameObject pauseFirstButton;
    public GameObject settingsFirstButton;
    public GameObject settingsClosedButton;

    public GameObject audioScreen;
    public GameObject controlsScreen;
    public GameObject graphicsScreen;
    public GameObject accessibilityScreen;
    public GameObject questWindow;
    public GameObject acceptQuestButton;
    public GameObject denyQuestButton;

    //Added to turn reticle off when paused
    GameObject grappleDetectorREF;
    Reticle reticleScript;



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

    public void PauseGame(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        
        if (isPaused)
        {
            ResumeGame();
            reticleScript.ReticleToggle(true);
        }
        else
        {
            isPaused = true;
            pauseMenu.SetActive(true);
            reticleScript.ReticleToggle(false);
            Debug.Log("Unpause");
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            
            EventSystem.current.SetSelectedGameObject(null); 
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
        isPaused = false;
        pauseMenu.SetActive(false);
        pauseSettings.SetActive(false);
        audioScreen.SetActive(false);
        controlsScreen.SetActive(false);
        graphicsScreen.SetActive(false);
        accessibilityScreen.SetActive(false);
        questWindow.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    
    public void OpenPauseSettings()
    {
        pauseSettings.SetActive(true);
        pauseMenu.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null); 
        EventSystem.current.SetSelectedGameObject(settingsFirstButton);

    }

    public void ClosePauseSettings()
    {
        pauseSettings.SetActive(false);
        pauseMenu.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null); 
        EventSystem.current.SetSelectedGameObject(settingsClosedButton);
    }

    public void ReturnToMain()
    {
        SceneManager.LoadScene(mainMenuScene);
        Time.timeScale = 1f;
    }

}

