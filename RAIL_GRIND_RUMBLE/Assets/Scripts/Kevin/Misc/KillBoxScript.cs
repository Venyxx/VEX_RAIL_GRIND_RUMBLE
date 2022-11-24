using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillBoxScript : MonoBehaviour
{
  private void OnCollisionEnter (Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            int y = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(y);
        }
    }
}
