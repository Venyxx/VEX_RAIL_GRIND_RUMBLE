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




    public bool isPaused;



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
        }
        else
        {
            isPaused = true;
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            
            EventSystem.current.SetSelectedGameObject(null); 
            EventSystem.current.SetSelectedGameObject(pauseFirstButton);
            
        }
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

