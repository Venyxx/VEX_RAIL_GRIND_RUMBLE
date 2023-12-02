using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosterDetector : MonoBehaviour
{
    private Graffiti graffitiSprayer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Poster"))
        {
            Debug.Log("I am a decal projector and I collided with a poster.");
            graffitiSprayer.ActivateBuff();
        }
    }

    public void Initialize(Graffiti graffitiSprayer)
    {
        this.graffitiSprayer = graffitiSprayer;
    }
}
