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
        
    }
    void Update()
    {
    
    }
   

    public void Attack(InputAction.CallbackContext context)
    {  //Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
        if (!context.started || movementScriptREF.isWalking || GameObject.Find("AimingCam") || movementScriptREF.Grounded == false)
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
        if (!context.started || movementScriptREF.isWalking || GameObject.Find("AimingCam") || movementScriptREF.Grounded == false)
        {
          
            return;
        }
       
        if (context.started || !IsAttacking)
        {
            Debug.Log("HeavyAttack" + context.phase);  
            IsAttacking = true;
            anim.SetTrigger("HAttackStart");

        }
       
        else if (context.performed || IsAttacking)
        {
            Debug.Log("HeavyAttack" + context.phase);
            anim.SetTrigger("HAttackEnd");

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
