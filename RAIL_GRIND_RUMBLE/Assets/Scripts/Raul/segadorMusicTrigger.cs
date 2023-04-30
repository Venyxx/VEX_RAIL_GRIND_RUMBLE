using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class segadorMusicTrigger : MonoBehaviour
{
    [SerializeField] private GameObject dynamicMusic;
    [SerializeField] private GameObject BossMusic;

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
    BossMusic.SetActive(true);
    dynamicMusic.SetActive(false);
   
    }

    private void OnTriggerExit(Collider player)
    {
    BossMusic.SetActive(false);
    dynamicMusic.SetActive(true);
   
    }
}
