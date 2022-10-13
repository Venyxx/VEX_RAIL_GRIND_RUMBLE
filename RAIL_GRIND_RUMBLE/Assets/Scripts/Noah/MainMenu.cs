using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public string firstLevel;
    public string mainMenu;

    public string mainSettings;

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
        SceneManager.LoadScene(mainSettings);
    }

    public void CloseSettings()
    {
        SceneManager.LoadScene(mainMenu);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting");
    }



}

