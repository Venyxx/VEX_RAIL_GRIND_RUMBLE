using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlugProgression : MonoBehaviour
{
    private bool phase1Complete;
    // Update is called once per frame
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
        return Phase1Plug.plugCount == 0;
    }
}
