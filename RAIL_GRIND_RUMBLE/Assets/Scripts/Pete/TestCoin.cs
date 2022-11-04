using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCoin : MonoBehaviour
{
    [SerializeField] private float speed;
    
    void Update()
    {
        transform.Rotate(0f, speed * Time.deltaTime, 0f, Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerObject"))
        {
            ThirdPersonMovement playerScript = other.gameObject.GetComponentInParent(typeof(ThirdPersonMovement)) as ThirdPersonMovement;
            playerScript.AddCoin(1);
            Destroy(gameObject);
        }
    }
}
