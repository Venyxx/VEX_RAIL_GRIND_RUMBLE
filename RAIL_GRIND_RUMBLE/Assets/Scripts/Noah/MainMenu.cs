using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public string firstLevel;
    public GameObject mainMenu;

    public GameObject mainSettings;


    // Start is called before the first frame update
    void Start()
    {
        
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
    }

    public void CloseSettings()
    {
        mainSettings.SetActive(false);
        mainMenu.SetActive(true);

    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting");
    }



}

