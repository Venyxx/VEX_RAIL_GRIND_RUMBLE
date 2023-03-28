using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlugProgression : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        if (CheckAllUnplugged())
        {
            SceneManager.LoadScene("EndCut");
        }
    }

    private bool CheckAllUnplugged()
    {
        return transform.childCount == 0;
    }
}
