using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickupController : MonoBehaviour
{
    [SerializeField] protected float speed;
    public int health = 5;
    public bool canBePickedUp { get; set; } = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, speed * Time.deltaTime, 0f, Space.Self);
    }
    
    protected virtual void OnTriggerEnter(Collider other)
    {
        IDamageable damageable;
        if (other.CompareTag("PlayerObject") && canBePickedUp)
        {
            if (other.TryGetComponent<IDamageable>(out damageable))
            {
                damageable.GainHealth(health);
            }
            Destroy(gameObject);
        }
    }

    protected virtual void OnTriggerStay(Collider other)
    {
        OnTriggerEnter(other);
    }
}
