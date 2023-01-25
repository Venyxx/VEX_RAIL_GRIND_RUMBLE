using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public Animator anim;
    public int Damage;
    public LayerMask enemyLayers;
    public bool IsAttacking = false;
    public static PlayerAttack instance;
    public Collider Weapon; 
    public Collider Knee;
    public int atkCount;
    private ThirdPersonMovement movementScriptREF;
    float HeavyAtkTimer;
    bool TimerOn;
    


    private void Awake()
    {
        instance = this;
        Weapon.enabled = false;
        Knee.enabled = false;
        movementScriptREF = GetComponent<ThirdPersonMovement>();
        
        
    }
   
    void Start()
    {
        //  anim = GetComponent<Animator>();
        HeavyAtkTimer = 0;
        
    }
    void Update()
    {
        Timer();
        if (HeavyAtkTimer >= 3 && IsAttacking)
        {
            anim.SetTrigger("HAttackEnd");
            TimerOn = false;
            IsAttacking = false;
            HeavyAtkTimer = 0;
            Damage = 50;

        }
        if (!IsAttacking)
        {
            Damage = 0;
        }

    }
   

    public void Attack(InputAction.CallbackContext context)
    {  //Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
        if (!context.started 
            || movementScriptREF.isWalking || 
            GameObject.Find("AimingCam") || 
            movementScriptREF.Grounded == false || 
            movementScriptREF.DialogueBox.activeInHierarchy 
            //||movementScriptREF.nearestDialogueTemplate != null
            )
        {
            return;
        }
       

        if (context.started && !IsAttacking)
        {
            IsAttacking = true;
            atkCount ++;
             
          
        }
    
       
        if (atkCount == 1)
        {
           anim.SetTrigger("LAttack");
        }

   //    foreach(Collider enemy in hitEnemies)
     //  {
       //      IDamageable damageable;
       // if (enemy.TryGetComponent<IDamageable>(out damageable))
       // {
       //     damageable.TakeDamage(Damage);
        //}
       //}
    }

        public void HeavyAttack(InputAction.CallbackContext context)
    {  //Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
        if (!context.started || 
            movementScriptREF.isWalking || 
            GameObject.Find("AimingCam") || 
            movementScriptREF.Grounded == false || 
            movementScriptREF.DialogueBox.activeInHierarchy)
        {
          
            return;
        }
       
        if (context.started || !IsAttacking)
        {
           // Debug.Log("HeavyAttack1" + context.phase);  
            
          

        }


        if (context.ReadValueAsButton() == true && !IsAttacking)
        {
            Debug.Log("Key Press");
            anim.SetTrigger("HAttackStart");
            TimerOn = true;
            IsAttacking = true;
            HeavyAtkTimer = 0;
            

        }

        if (context.ReadValueAsButton() == false && IsAttacking)
        {
           Debug.Log("Key Release");
          if(HeavyAtkTimer >=1)
            {
                anim.SetTrigger("HAttackEnd");
                IsAttacking = false;
                HeavyAtkTimer = 0;
                Damage = 35;
            }
            if (HeavyAtkTimer < 1)
            {
                anim.SetTrigger("HAttackEnd");
                IsAttacking = false;
                HeavyAtkTimer = 0;
                Damage = 30;
            }
            if (HeavyAtkTimer >= 2)
            {
                anim.SetTrigger("HAttackEnd");
                IsAttacking = false;
                HeavyAtkTimer = 0;
                Damage = 100;
            }

           

        }



        //    foreach(Collider enemy in hitEnemies)
        //  {
        //      IDamageable damageable;
        // if (enemy.TryGetComponent<IDamageable>(out damageable))
        // {
        //     damageable.TakeDamage(Damage);
        //}
        //}
    }

    private void Timer()
    {
        if(TimerOn && IsAttacking)
        {
            HeavyAtkTimer += Time.deltaTime;
        }
        if(!TimerOn)
        {
            HeavyAtkTimer = 0;
        }

      
        //Debug.Log(HeavyAtkTimer);
    }


    private void OnTriggerEnter(Collider other)
    {
            IDamageable damageable;
        if (other.TryGetComponent<IDamageable>(out damageable))
        {
            damageable.TakeDamage(Damage);
        }
    }

//void OnDrawGizmosSelected()
//{
 //   if (attackPoint == null)
  //  return;

  //  Gizmos.DrawWireSphere(attackPoint.position, attackRange);
//}


}
