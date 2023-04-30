using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySounds : MonoBehaviour
{
    [SerializeField] private AudioSource Gun;
    [SerializeField] private AudioSource Skate;
    [SerializeField] private AudioSource Death1;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GunSFX()
    {
        Gun.Play(0);

    }

    private void Death1SFX()
    {
        Death1.Play(0);

    }
}
