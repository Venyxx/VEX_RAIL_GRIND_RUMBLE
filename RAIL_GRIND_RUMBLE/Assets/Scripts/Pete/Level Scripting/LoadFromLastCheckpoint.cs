using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LoadFromLastCheckpoint : MonoBehaviour
{
    public void ReloadFromCheckpoint(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        Debug.Log(CheckpointController.LastScene);
        SceneManager.LoadScene(CheckpointController.LastScene);
    }
    
    public void ReturnToMainMenu(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        CheckpointController.lastCheckPointPosition = new Vector3(0, 0, 0);
        SceneManager.LoadScene("MainMenu");
    }

    public void OnApplicationQuit()
    {
        CheckpointController.lastCheckPointPosition = new Vector3(0, 0, 0);
    }
}
