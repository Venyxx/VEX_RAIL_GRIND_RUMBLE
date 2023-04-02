using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject interiorLookCam;
    [SerializeField]
    private GameObject defaultCam;
    private bool CameraTriggered;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void OnTriggerEnter(Collider Player)
    {
        interiorLookCam.SetActive(false);
        defaultCam.SetActive(true);
    }

}
