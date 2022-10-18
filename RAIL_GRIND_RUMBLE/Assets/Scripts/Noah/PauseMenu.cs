using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public string mainMenuScene;
    public GameObject pauseMenu;
    public GameObject pauseSettings;
    public bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p"))
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
        }
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        pauseSettings.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false;
    }
    
    
    public void OpenPauseSettings()
    {
        pauseSettings.SetActive(true);
        pauseMenu.SetActive(false);

    }

    public void ClosePauseSettings()
    {
        pauseSettings.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void ReturnToMain()
    {
        SceneManager.LoadScene(mainMenuScene);
        Time.timeScale = 1f;
    }












}

