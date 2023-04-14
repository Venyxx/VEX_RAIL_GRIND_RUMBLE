using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] GameObject loadingBackground;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = loadingBackground.GetComponent<Animator>();
    }

    void Awake()
    {
        loadingBackground.SetActive(true);
        StartCoroutine(LoadIn());
    }

    public void LoadOutStart(string sceneName)
    {
        if (ProgressionManager.Get() != null)
        {
            ProgressionManager.Get().firstLoad = true;
        }
        StartCoroutine(LoadOut(sceneName));
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
