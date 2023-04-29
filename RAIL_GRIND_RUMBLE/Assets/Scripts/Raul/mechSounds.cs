using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mechSounds : MonoBehaviour
{
     [SerializeField] private AudioSource Gatling;
     [SerializeField] private AudioSource Missile;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GatlingSFX()
    {
        Gatling.Play(0);

    }

     private void StopGatlingSFX()
    {
        Gatling.Stop();

    }

     private void MissileSFX()
    {
        Missile.Play(0);

    }

    

}
