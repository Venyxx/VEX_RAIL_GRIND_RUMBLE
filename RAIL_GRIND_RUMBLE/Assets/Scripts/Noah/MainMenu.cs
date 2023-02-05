using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{

    public string firstLevel;
    public GameObject mainMenu;

    public GameObject mainSettings;

    public GameObject mainMenuFirstButton;
    public GameObject mainSettingsFirstButton;
    public GameObject mainSettingsClosedButton;
    public GameObject fastTravelButtons;


    // Start is called before the first frame update
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(null); 
        EventSystem.current.SetSelectedGameObject(mainMenuFirstButton);
        Cursor.lockState = CursorLockMode.None;
        CheckpointController.lastCheckPointPosition = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    { 
        SceneManager.LoadScene(firstLevel);
    }

    public void OpenSettings()
    {
        mainSettings.SetActive(true);
        mainMenu.SetActive(false);
        fastTravelButtons.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null); 
        EventSystem.current.SetSelectedGameObject(mainSettingsFirstButton);
    }

    public void CloseSettings()
    {
        mainSettings.SetActive(false);
        mainMenu.SetActive(true);
        fastTravelButtons.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null); 
        EventSystem.current.SetSelectedGameObject(mainSettingsClosedButton);

    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting");
    }



}

