using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSounds : MonoBehaviour
{
    [SerializeField] private AudioSource Crash;
    [SerializeField] private AudioSource Skate;
    [SerializeField] private AudioSource Skate2;
    [SerializeField] private AudioSource Dive;
    [SerializeField] private AudioSource Brake;
    [SerializeField] private AudioSource Land;
    [SerializeField] private AudioSource airHeavySwoosh;
    [SerializeField] private AudioSource footStep1;
    [SerializeField] private AudioSource footStep2;
    [SerializeField] private AudioSource Grunt1;
    [SerializeField] private AudioSource Grunt2;
    [SerializeField] private AudioSource Grunt3;
    [SerializeField] private AudioSource Grunt4;
    [SerializeField] private AudioSource Grunt5;
    [SerializeField] private AudioSource Grunt6;
    [SerializeField] private AudioSource Gliding;
    [SerializeField] private AudioSource JumpGrunt;

    public ThirdPersonMovement movementRef;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //sounds
    private void CrashSFX()
    {
        Crash.Play(0);

    }

    private void SkateSFX()
    {
        Skate.Play(0);

    }

    private void Skate2SFX()
    {
        Skate2.Play(0);

    }

    private void DiveSFX()
    {
        Dive.Play(0);

    }

    private void BrakeSFX()
    {
        Brake.Play(0);

    }

    private void LandSFX()
    {
        Land.Play(0);

    }

    private void airHeavySwooshSFX()
    {
       airHeavySwoosh.Play(0);

    }

    public void stopSkateSFX()
    {
       Skate.Stop();
       Skate2.Stop();

    }

    public void footStep1SFX()
    {
        footStep1.Play(0);

    }

    public void footStep2SFX()
    {
       
         footStep2.Play(0);
    }

    
    private void Grunt1SFX()
    {
       Grunt1.Play(0);

    }

    
    private void Grunt2SFX()
    {
       Grunt2.Play(0);

    }

    
    private void Grunt3SFX()
    {
       Grunt3.Play(0);

    }

    private void Grunt4SFX()
    {
       Grunt4.Play(0);

    }

    private void Grunt5SFX()
    {
       Grunt5.Play(0);

    }

    private void Grunt6SFX()
    {
       Grunt6.Play(0);

    }

    private void GlidingSFX()
    {
       Gliding.Play(0);

    }

    public void stopGlidingSFX()
    {
       Gliding.Stop();

    }

    public void JumpGruntSFX()
    {
       JumpGrunt.Play(0);

    }
}
