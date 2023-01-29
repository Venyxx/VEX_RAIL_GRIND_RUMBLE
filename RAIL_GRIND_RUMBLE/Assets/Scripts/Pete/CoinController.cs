using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CoinController : MonoBehaviour
{
    [SerializeField] private float speed;
     [FormerlySerializedAs("Health")] public int health = 5;
    
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
            damageable.GainHealth(health);
        }
            Destroy(gameObject);
        }
    }
}
