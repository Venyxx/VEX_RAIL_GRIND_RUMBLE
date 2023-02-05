using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void OpenAudio()
    {
        audioScreen.SetActive(true);
        controlsScreen.SetActive(false);
        graphicsScreen.SetActive(false);
        accessibilityScreen.SetActive(false);
        settingsScreen.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null); 
        EventSystem.current.SetSelectedGameObject(audioFirstButton);
    }

    public void OpenControls()
    {
        audioScreen.SetActive(false);
        controlsScreen.SetActive(true);
        graphicsScreen.SetActive(false);
        accessibilityScreen.SetActive(false);
        settingsScreen.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null); 
        EventSystem.current.SetSelectedGameObject(controlsFirstButton);
    }

    public void OpenGraphics()
    {
        audioScreen.SetActive(false);
        controlsScreen.SetActive(false);
        graphicsScreen.SetActive(true);
        accessibilityScreen.SetActive(false);
        settingsScreen.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null); 
        EventSystem.current.SetSelectedGameObject(graphicsFirstButton);
    }

    public void OpenAccessiblity()
    {
        audioScreen.SetActive(false);
        controlsScreen.SetActive(false);
        graphicsScreen.SetActive(false);
        accessibilityScreen.SetActive(true);
        settingsScreen.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null); 
        EventSystem.current.SetSelectedGameObject(accessibilityFirstButton);
    }

    public void BackToSettings()
    {
        audioScreen.SetActive(false);
        controlsScreen.SetActive(false);
        graphicsScreen.SetActive(false);
        accessibilityScreen.SetActive(false);
        settingsScreen.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null); 
        EventSystem.current.SetSelectedGameObject(settingsReturnButton);
    }

    //Turn on God Mode
    public void GodModeToggle()
    {
        if (godModeToggleREF.GetComponent<Toggle>().isOn == false)
        {
            godMode = false;
            Debug.Log("God Mode is Off");
        } else {
            godMode = true;
            Debug.Log("God Mode is On. May you have mercy on their souls.");
        }
    }
}
