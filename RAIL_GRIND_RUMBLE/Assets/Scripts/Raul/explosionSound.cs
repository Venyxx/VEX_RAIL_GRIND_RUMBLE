using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionSound : MonoBehaviour
{
    [SerializeField] private AudioSource Explosion;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ExplosionSFX()
    {
        Explosion.Play(0);

    }


}
