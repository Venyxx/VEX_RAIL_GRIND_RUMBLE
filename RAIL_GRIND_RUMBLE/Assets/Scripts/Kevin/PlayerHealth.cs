using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 3;
    public float currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }
    
    public void TakeDamage (float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            //the player is dead
            Destroy(gameObject);
        }
    }
}
