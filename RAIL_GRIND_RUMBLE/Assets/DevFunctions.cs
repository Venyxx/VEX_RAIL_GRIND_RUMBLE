using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DevFunctions : MonoBehaviour
{

    // Update is called once per frame
    public void LoadOutskirts()
    {
        SceneManager.LoadScene("Outskirts");
    }
    public void LoadInnerRing()
    {
        SceneManager.LoadScene("InnerRingLevel");
    }
    public void LoadServosParking()
    {
        SceneManager.LoadScene("ServosParkingLot");
    }

    public void LoadServosKitLevel()
    {
        SceneManager.LoadScene("ServosKitDemo");
    }

    public void LoadArisHouse()
    {
        SceneManager.LoadScene("Ari's House");
    }
}
