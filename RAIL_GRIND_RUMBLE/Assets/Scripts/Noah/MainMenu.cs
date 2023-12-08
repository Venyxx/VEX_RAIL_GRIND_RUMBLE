using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : ControllerMenu
{

    public string firstLevel;
    public GameObject mainMenu;
    public GameObject mainMenuSignal;
    public GameObject resetSaveButton;

    public GameObject SettingsMainPage;
    public GameObject SettingsScreen;

    [SerializeField] private Animator ariAnimator;
    [SerializeField] private Animator cameraAnimator;
    
    

    public GameObject mainMenuFirstButton;
    public GameObject mainSettingsFirstButton;
    public GameObject mainSettingsClosedButton;
    public GameObject fastTravelButtons;
    
    public GameObject audioScreen;
    public GameObject controlsScreen;
    public GameObject graphicsScreen;
    public GameObject accessibilityScreen;
    private Scene scene;




    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        EventSystem.current.SetSelectedGameObject(null); 
        EventSystem.current.SetSelectedGameObject(mainMenuFirstButton);
        Cursor.lockState = CursorLockMode.None;
        CheckpointController.lastCheckPointPosition = new Vector3(0, 0, 0);
        //GameObject.Find("damageBuffIcon").SetActive(false);
        scene = SceneManager.GetActiveScene();
        
        
    }
    // Update is called once per frame
    new void Update()
    {
        
        base.Update();
    }
    
    public void OpenSettings()
    {
        SettingsMainPage.SetActive(true);
        SettingsScreen.SetActive(true);
        mainMenu.SetActive(false);
        fastTravelButtons.SetActive(false);

         if (scene.name == "MainMenu");
            resetSaveButton.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null); 
        EventSystem.current.SetSelectedGameObject(mainSettingsFirstButton);
    }

    public void CloseSettings()
    {
        SettingsMainPage.SetActive(false);
        SettingsScreen.SetActive(false);
        mainMenu.SetActive(true);
        fastTravelButtons.SetActive(true);

        if (scene.name == "MainMenu");
            resetSaveButton.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null); 
        EventSystem.current.SetSelectedGameObject(mainSettingsClosedButton);

    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting");
    }
}

