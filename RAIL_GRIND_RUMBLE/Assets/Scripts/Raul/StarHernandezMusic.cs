using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarHernandezMusic : MonoBehaviour
{
    [SerializeField] private GameObject HernandezMusic;
    [SerializeField] private GameObject DynamicMusic;
    
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
        HernandezMusic.SetActive(true);
        DynamicMusic.SetActive(false);
    }
}
