using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossStartTrigger : MonoBehaviour
{
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GetComponent<FinalBossGroundSpawner>().Activate();
            FindObjectOfType<DonovanPhase2>().Activate();
        }
    }
}
