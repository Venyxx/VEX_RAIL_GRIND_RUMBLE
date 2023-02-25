using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadOut()
    {
        loadingBackground.SetActive(true);
        anim.CrossFade("LoadOut", 0);
    }

    IEnumerator LoadIn()
    {
        yield return new WaitForSeconds(1f);
        loadingBackground.SetActive(false);
    }
}
