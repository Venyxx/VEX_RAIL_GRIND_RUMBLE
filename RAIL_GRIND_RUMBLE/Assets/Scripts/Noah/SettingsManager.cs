using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class SettingsManager : MonoBehaviour
{
    public GameObject audioScreen;
    public GameObject controlsScreen;
    public GameObject graphicsScreen;
    public GameObject accessibilityScreen;
    public GameObject settingsScreen;

    public GameObject audioFirstButton;
    public GameObject controlsFirstButton;
    public GameObject graphicsFirstButton;
    public GameObject accessibilityFirstButton;
    public GameObject settingsReturnButton;

    //God Mode
    public static bool godMode = false;
    public GameObject godModeToggleREF;

    //For Sounds
    InfoScreen infoScreen;

    //For Back to Pause
    PauseMenu pauseMenu;

    //For Back to Pause
    MainMenu mainMenu;
    

    void Start()
    {
        infoScreen = GetComponent<InfoScreen>();
        pauseMenu = GetComponent<PauseMenu>();
        mainMenu = GetComponent<MainMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        //Assures controllers always function on menus
       if (EventSystem.current.currentSelectedGameObject == null && PauseMenu.isPaused == true)
       {
            if (settingsScreen.activeInHierarchy)
            {
                settingsReturnButton.GetComponent<Button>().Select();
            }
       }
       
       if (EventSystem.current.IsPointerOverGameObject())
       {
           EventSystem.current.SetSelectedGameObject(null);
       }

       if (godMode == true)
       {
           godModeToggleREF.GetComponent<Toggle>().isOn = true;
       }
       
       if (godMode == false)
       {
           godModeToggleREF.GetComponent<Toggle>().isOn = false;
       }
       
    }

    public void OpenAudio()
    {
        audioScreen.SetActive(true);
        controlsScreen.SetActive(false);
        graphicsScreen.SetActive(false);
        accessibilityScreen.SetActive(false);
        settingsScreen.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
        if (EventSystem.current.IsPointerOverGameObject())
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(audioFirstButton);
        }


        infoScreen.PlaySoundUI(infoScreen.nextSound);
    }

    public void OpenControls()
    {
        audioScreen.SetActive(false);
        controlsScreen.SetActive(true);
        graphicsScreen.SetActive(false);
        accessibilityScreen.SetActive(false);
        settingsScreen.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
        if (EventSystem.current.IsPointerOverGameObject())
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(controlsFirstButton);
        }

        infoScreen.PlaySoundUI(infoScreen.nextSound);
    }

    public void OpenGraphics()
    {
        audioScreen.SetActive(false);
        controlsScreen.SetActive(false);
        graphicsScreen.SetActive(true);
        accessibilityScreen.SetActive(false);
        settingsScreen.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
        if (EventSystem.current.IsPointerOverGameObject())
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(graphicsFirstButton);
        }

        infoScreen.PlaySoundUI(infoScreen.nextSound);
    }

    public void OpenAccessiblity()
    {
        audioScreen.SetActive(false);
        controlsScreen.SetActive(false);
        graphicsScreen.SetActive(false);
        accessibilityScreen.SetActive(true);
        settingsScreen.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);
        if (EventSystem.current.IsPointerOverGameObject())
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(accessibilityFirstButton);
        }

        infoScreen.PlaySoundUI(infoScreen.nextSound);
    }

    public void BackToSettings()
    {
        audioScreen.SetActive(false);
        controlsScreen.SetActive(false);
        graphicsScreen.SetActive(false);
        accessibilityScreen.SetActive(false);
        settingsScreen.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        if (EventSystem.current.IsPointerOverGameObject())
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(settingsReturnButton);
        }
        

        infoScreen.PlaySoundUI(infoScreen.backSound);
    }

    //Turn on God Mode
    public void GodModeToggle()
    {
        infoScreen.PlaySoundUI(infoScreen.selectSound);
        if (godModeToggleREF.GetComponent<Toggle>().isOn == false)
        {
            godMode = false;
            Debug.Log("God Mode is Off");
        } else {
            godMode = true;
            Debug.Log("God Mode is On. May you have mercy on their souls.");
        }
    }

    //B Button Back
    public void ControllerBackButton(InputAction.CallbackContext context)
    {
        if (!context.started  || pauseMenu.pauseSettings.activeInHierarchy == false) return;

        //Troubleshoot why this isn't going back to main pause screen instead of resuming
        if (context.started && PauseMenu.isPaused == true)
        {
            if (settingsScreen.activeInHierarchy == false)
            {
                BackToSettings();
            }  else {
                pauseMenu.ClosePauseSettings();
            }
        }
        
        if (context.started && mainMenu.mainMenuSignal.activeInHierarchy == true)
        {
            if (settingsScreen.activeInHierarchy == false)
            {
                BackToSettings();
            }  else {
                mainMenu.CloseSettings();
            }
        }
        
    }
}
