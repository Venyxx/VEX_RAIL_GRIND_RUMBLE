using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewScene : MonoBehaviour
{
    [SerializeField] private string sceneName;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerObject") || other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
