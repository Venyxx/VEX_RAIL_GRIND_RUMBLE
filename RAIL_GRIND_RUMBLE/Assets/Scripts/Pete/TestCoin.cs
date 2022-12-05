using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCoin : MonoBehaviour
{
    [SerializeField] private float speed;
     public int Health = 5;
    
    void Update()
    {
        transform.Rotate(0f, speed * Time.deltaTime, 0f, Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
          IDamageable damageable;
        if (other.CompareTag("PlayerObject"))
        {
            ThirdPersonMovement playerScript = other.gameObject.GetComponentInParent(typeof(ThirdPersonMovement)) as ThirdPersonMovement;
            playerScript.AddCoin(1);


        if (other.TryGetComponent<IDamageable>(out damageable))
        {
            damageable.GainHealth(Health);
        }
            Destroy(gameObject);
        }
    }
}
