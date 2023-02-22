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
}
