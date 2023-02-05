using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadOutskirts : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerObject"))
        {
            SceneManager.LoadScene("Outskirts");
        }
    }
}
