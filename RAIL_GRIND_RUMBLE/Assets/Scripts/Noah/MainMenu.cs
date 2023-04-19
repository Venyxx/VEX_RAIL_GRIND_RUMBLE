using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour
{

    public string firstLevel;
    public GameObject mainMenu;
    public GameObject mainMenuSignal;

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

    


    // Start is called before the first frame update
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(null); 
        EventSystem.current.SetSelectedGameObject(mainMenuFirstButton);
        Cursor.lockState = CursorLockMode.None;
        CheckpointController.lastCheckPointPosition = new Vector3(0, 0, 0);

        GameObject.Find("damageBuffIcon").SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null && mainMenu.activeInHierarchy)
        {
            mainMenuFirstButton.GetComponent<Button>().Select();
        }
        
        if (EventSystem.current.currentSelectedGameObject == null && SettingsMainPage.activeInHierarchy)
        {
            mainSettingsFirstButton.GetComponent<Button>().Select();
        }
        
        if (EventSystem.current.IsPointerOverGameObject())
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    public void StartGame()
    { 
        StartCoroutine(WaitStartGame());
    }

     IEnumerator WaitStartGame()
    {
        

        ariAnimator.SetBool("pressedStart" , true);
        cameraAnimator.SetBool("pressedStart" , true);
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene(firstLevel);
    }

    public void OpenSettings()
    {
        SettingsMainPage.SetActive(true);
        SettingsScreen.SetActive(true);
        mainMenu.SetActive(false);
        fastTravelButtons.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null); 
        EventSystem.current.SetSelectedGameObject(mainSettingsFirstButton);
    }

    public void CloseSettings()
    {
        SettingsMainPage.SetActive(false);
        SettingsScreen.SetActive(false);
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

