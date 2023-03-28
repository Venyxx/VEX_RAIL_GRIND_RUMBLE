using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    public float sceneTime;
    public string sceneName;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SceneChange());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SceneManager.LoadScene(sceneName);
    }

    IEnumerator SceneChange()
    {
        yield return new WaitForSeconds(sceneTime);
        SceneManager.LoadScene(sceneName);

    }
}
