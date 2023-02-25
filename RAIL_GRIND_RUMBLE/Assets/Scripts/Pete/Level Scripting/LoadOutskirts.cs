using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadOutskirts : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerObject") || other.gameObject.CompareTag("Player"))

    {
            SceneManager.LoadScene("Outskirts");
        }
    }
}
