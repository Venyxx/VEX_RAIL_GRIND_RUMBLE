using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject audioScreen;
    public GameObject controlsScreen;
    public GameObject graphicsScreen;
    public GameObject accessibilityScreen;
    public GameObject settingsScreen;


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
    }

    public void OpenControls()
    {
        audioScreen.SetActive(false);
        controlsScreen.SetActive(true);
        graphicsScreen.SetActive(false);
        accessibilityScreen.SetActive(false);
        settingsScreen.SetActive(false);
    }

    public void OpenGraphics()
    {
        audioScreen.SetActive(false);
        controlsScreen.SetActive(false);
        graphicsScreen.SetActive(true);
        accessibilityScreen.SetActive(false);
        settingsScreen.SetActive(false);
    }

    public void OpenAccessiblity()
    {
        audioScreen.SetActive(false);
        controlsScreen.SetActive(false);
        graphicsScreen.SetActive(false);
        accessibilityScreen.SetActive(true);
        settingsScreen.SetActive(false);
    }

    public void BackToSettings()
    {
        audioScreen.SetActive(false);
        controlsScreen.SetActive(false);
        graphicsScreen.SetActive(false);
        accessibilityScreen.SetActive(false);
        settingsScreen.SetActive(true);
    }
}
