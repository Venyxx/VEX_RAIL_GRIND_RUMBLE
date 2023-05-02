using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class PlayerHealth : MonoBehaviour, IDamageable
{
     

    public float maxHealth;
    public float currentHealth;
    private ThirdPersonMovement thirdPersonMovementREF;
    public bool Dizzy;

     [SerializeField] private Animator _animator;
    private float hurtDelayAnimNumber = 0.005f;
  
    

    [SerializeField] private bool debugKill;

    private bool outOfBounds = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
        
        thirdPersonMovementREF = GetComponent<ThirdPersonMovement>();
        //thirdPersonMovementREF.RecalculateStats();
        currentHealth = maxHealth;
        
        

    }

    void Update()
    {
        if (debugKill || outOfBounds)
        {
            TakeDamage(maxHealth);
        }
        if( Input.GetKeyDown(KeyCode.L) )
        {
            SceneManager.LoadScene(10);
        }
    }


   
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("KillBox"))
        {
            outOfBounds = true;
        }
    }

    public void TakeDamage (float damage)
    {
        //ensures ari can not take damage from enemies while stuck in dialogue
        if (thirdPersonMovementREF.dialogueBox.activeInHierarchy || SettingsManager.godMode) return; 

        StartCoroutine(HurtAnimDelay());
        Debug.Log("damage");

        currentHealth -= damage;


        if (currentHealth <= 0)
        {
            currentHealth = 0;
            MainQuest3 mq3 = ProgressionManager.Get().mainQuest3;

            if (mq3 != null && mq3.isComplete && !ProgressionManager.Get().deathCutscenePlayed)
            {
                ProgressionManager.Get().mainQuest3Death = true;
                SceneManager.LoadScene("Ari's House");
            }
            else
            {
                SceneManager.LoadScene("LoseScene");

            }
        }
        
    }


       IEnumerator HurtAnimDelay()
    {

        _animator.SetBool("isHurting", true);
        yield return new WaitForSeconds(hurtDelayAnimNumber);
        _animator.SetBool("isHurting", false);

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

    public void IsDizzy(bool isDizzy)
    {
        Dizzy = isDizzy;
    }
}
