using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnLightsOn : MonoBehaviour
{

    [SerializeField] private GameObject NightLight;
    [SerializeField] private GameObject InsideLight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider player)
{
    NightLight.SetActive(false);
    InsideLight.SetActive(true);
   
}
}
