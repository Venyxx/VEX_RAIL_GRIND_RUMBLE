using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class camZoomInTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject interiorLookCam;
    [SerializeField]
    private GameObject defaultCam;
    private bool CameraTriggered;
    
    
    // void OnTriggerEnter(Collider Player)
    // {
    //     if(CameraTriggered == false)
    //     {
    //     StartCoroutine(WaitToMoveTriggerA());
    //     }
        

    // }

    // void OnTriggerExit(Collider Player)
    // {
    //     if(CameraTriggered == false)
    //     {
    //     StartCoroutine(WaitToMoveTriggerB());
    //     }

    // }

    void OnTriggerStay(Collider Player)
    {
       interiorLookCam.SetActive(true);
        defaultCam.SetActive(false);

    }

     void OnTriggerExit(Collider Player)
    {
        interiorLookCam.SetActive(false);
        defaultCam.SetActive(true);

    }

    
    // IEnumerator WaitToMoveTriggerA()
    // {
    //     interiorLookCam.SetActive(true);
    //     defaultCam.SetActive(false);
    //     CameraTriggered = true;
    //     yield return new WaitForSeconds(1);
    //     CameraTriggered = false;
    // }

    // IEnumerator WaitToMoveTriggerB()
    // {
    //     interiorLookCam.SetActive(false);
    //     defaultCam.SetActive(true);
    //     CameraTriggered = true;
    //     yield return new WaitForSeconds(1);
    //     CameraTriggered = false;

        
    // }
}
