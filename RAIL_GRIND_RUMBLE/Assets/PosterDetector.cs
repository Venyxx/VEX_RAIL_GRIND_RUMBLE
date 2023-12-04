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
            PosterActive posterActiveDetector = other.gameObject.GetComponent<PosterActive>();
            if (!posterActiveDetector.isSprayed)
            {
                posterActiveDetector.isSprayed = true;
                graffitiSprayer.ActivateBuff();
            }
            else
            {
                Debug.Log("You already sprayed this poster");
            } 
        }
    }

    public void Initialize(Graffiti graffitiSprayer)
    {
        this.graffitiSprayer = graffitiSprayer;
    }
}
