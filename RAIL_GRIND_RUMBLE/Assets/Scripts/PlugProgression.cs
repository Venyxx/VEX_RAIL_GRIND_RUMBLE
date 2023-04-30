using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlugProgression : MonoBehaviour
{
    private bool phase1Complete;

    private Phase1Plug[] plugs;
    // Update is called once per frame

    private void Start()
    {
        plugs = FindObjectsOfType<Phase1Plug>();
    }

    void Update()
    {
        if (CheckAllUnplugged() && !phase1Complete)
        {
            ProgressionManager.Get().PlayCutscene(9);
            DialogueManager.DialogueWipe();
            FindObjectOfType<ThirdPersonMovement>().gameObject.transform.position =
                GameObject.Find("Phase2Teleport").transform.position;
            phase1Complete = true;
        }

 
    }

    private bool CheckAllUnplugged()
    {
        int completionCount = plugs.Length;
        int actualCount = 0;
        foreach (var plug in plugs)
        {
            if (plug.unplugged)
            {
                actualCount += 1;
            }
        }
        return completionCount == actualCount;
    }
}
