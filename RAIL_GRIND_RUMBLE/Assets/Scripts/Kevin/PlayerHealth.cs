using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public float maxHealth = 3;
    public float currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }
    
    public void TakeDamage (float Damage)
    {
        currentHealth -= Damage;

        if (currentHealth <= 0)
        {
            //the player is dead
            Destroy(gameObject);
        }
    }
    public Transform GetTransform()
    {
        return transform;
    }
}
