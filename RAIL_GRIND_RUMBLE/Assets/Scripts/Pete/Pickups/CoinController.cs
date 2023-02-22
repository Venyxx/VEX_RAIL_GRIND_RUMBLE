using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CoinController : HealthPickupController
{
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.CompareTag("PlayerObject"))
        {
            ThirdPersonMovement playerScript = other.gameObject.GetComponentInParent(typeof(ThirdPersonMovement)) as ThirdPersonMovement;
            playerScript.AddCoin(1);
        }
    }

    protected override void OnTriggerStay(Collider other)
    {
        /*DO NOT DELETE; EMPTY FOR A REASON
         (to prevent base.OnTriggerStay() from 
         getting called and adding two coins)*/
    }
}
