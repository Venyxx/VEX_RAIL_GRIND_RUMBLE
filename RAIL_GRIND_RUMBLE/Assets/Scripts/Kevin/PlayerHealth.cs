using UnityEngine;
using UnityEngine.SceneManagement;

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
            int y = SceneManager.GetActiveScene().buildIndex;
            //the player is dead
            SceneManager.LoadScene(y);
        }
    }
    public Transform GetTransform()
    {
        return transform;
    }
}
