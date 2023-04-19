using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMenuSounds : MonoBehaviour
{
    [SerializeField] private AudioSource Ruffle;
    [SerializeField] private AudioSource Jump;
    [SerializeField] private AudioSource Grapple;
    [SerializeField] private AudioSource Whoosh;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //sounds
    private void JumpSFX()
    {
        Jump.Play(0);

    }

    private void RuffleSFX()
    {
        Ruffle.Play(0);

    }

    private void GrappleSFX()
    {
        Grapple.Play(0);

    }

    private void WhooshSFX()
    {
        Whoosh.Play(0);

    }
}
