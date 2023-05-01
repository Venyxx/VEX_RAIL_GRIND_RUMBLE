using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mechSounds : MonoBehaviour
{
     [SerializeField] private AudioSource Gatling;
     [SerializeField] private AudioSource Missile;
     [SerializeField] private AudioSource BossMusic;
     private bool hasPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        hasPlayed = false;
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

    private void BossMusicSFX()
    {
        StartCoroutine(BossMusicCoroutine());

    }

    IEnumerator BossMusicCoroutine()
    {
        if(hasPlayed == false)
        {
        hasPlayed = true;
        yield return new WaitForSeconds(30);
        BossMusic.Play(0);
        }
        

    }

    

}
