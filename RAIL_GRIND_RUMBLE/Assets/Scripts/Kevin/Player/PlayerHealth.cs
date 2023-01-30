using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public float maxHealth = 3;
    public float currentHealth;
    private ThirdPersonMovement thirdPersonMovementREF;

    [SerializeField] private bool debugKill;

    private bool outOfBounds = false;
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        thirdPersonMovementREF = GetComponent<ThirdPersonMovement>();

    }

    void Update()
    {
        if (debugKill || outOfBounds)
        {
            TakeDamage(100);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("KillBox"))
        {
            outOfBounds = true;
        }
    }

    public void TakeDamage (float Damage)
    {
        //ensures ari can not take damage from enemies while stuck in dialogue
        if (thirdPersonMovementREF.dialogueBox.activeInHierarchy) return; 
        
        currentHealth -= Damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            SceneManager.LoadScene("LoseScene");
        }
    }

    public void GainHealth (float GainHealth)
    {
        currentHealth += GainHealth;

        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
            
        }
    }


    public Transform GetTransform()
    {
        return transform;
    }
}
