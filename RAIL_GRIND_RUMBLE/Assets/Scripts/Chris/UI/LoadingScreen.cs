using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using Cinemachine;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] GameObject loadingBackground;
    Animator anim;
    [SerializeField] private Animator ariAnimator;
    [SerializeField] private Animator cameraAnimator;
    [SerializeField] private Animator grappleAnimator;
    [SerializeField] GameObject gameTitle;
    [SerializeField] GameObject startButton;
    [SerializeField] GameObject settingsButton;
    [SerializeField] GameObject quitButton;
    private int level;

    
    // Start is called before the first frame update
    void Start()
    {
        anim = loadingBackground.GetComponent<Animator>();
        level = SaveManager.Instance.state.completedLevel;
    }

    void Awake()
    {
        loadingBackground.SetActive(true);
        StartCoroutine(LoadIn());
    }

    public void LoadOutStart()
    {
        // if (ProgressionManager.Get() != null)
        // {
        //     ProgressionManager.Get().firstLoad = true;
        // }
        // StartCoroutine(LoadOut(sceneName));

        StartCoroutine(WaitStartGame());
    }


     IEnumerator WaitStartGame()
    {
        ariAnimator.SetBool("pressedStart" , true);
        cameraAnimator.SetBool("pressedStart" , true);
        grappleAnimator.SetBool("pressedStart" , true);
        startButton.SetActive(false);
        settingsButton.SetActive(false);
        quitButton.SetActive(false);
        gameTitle.SetActive(false);
        yield return new WaitForSeconds(4);

         if (ProgressionManager.Get() != null)
        {
            ProgressionManager.Get().firstLoad = true;
        }

         string scene;
         if (level == 1)
         {
             Debug.Log("LOADING INNER RING");
             scene = "InnerRingLevel";
         }
         else if (level == 2)
         {
             Debug.Log("LOADING SERVOS");
             scene = "Servos HQ";
         }
         else
         {
             Debug.Log("LOADING OPENING CUTSCENE");
             scene = "Ari's House";
         }
        StartCoroutine(LoadOut(scene));
    }


    IEnumerator LoadOut(string scene)
    {
        loadingBackground.SetActive(true);
        anim.CrossFade("LoadOut", 0);
        yield return new WaitForSeconds(0.75f);
        SceneManager.LoadScene(scene);
    }

    IEnumerator LoadIn()
    {
        Debug.Log("Load In Running");
        //anim.CrossFade("LoadIn", 0);
        yield return new WaitForSeconds(1f);
        loadingBackground.SetActive(false);
    }

     
}
