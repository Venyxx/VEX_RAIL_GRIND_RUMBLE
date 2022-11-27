using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    Animator anim;
    public int Damage = 100;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public float cooldownTime = 2f;
    private float nextFireTime = 0;
    public static int noOfAttacks = 0;
    float lastAttackTime = 0;
    float maxComboDelay = 1;
   
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    
    var context = new InputAction.CallbackContext();

      if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.07f && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
      {
          anim.SetBool("Hit1", false);
      }
      if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.07f && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
      {
          anim.SetBool("Hit2", false);
      }
      if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.07f && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack3"))
      {
          anim.SetBool("Hit3", false);
          noOfAttacks = 0;
      }

      if (Time.time - lastAttackTime > maxComboDelay)
      {
          noOfAttacks = 0;
      }
      if(Time.time > nextFireTime)
      {
        if (context.started)
        {
            Attack(context);
        }
      }
       
       
    }
     void Attack(InputAction.CallbackContext context)
    {  Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
        if (!context.started)
        {
            return;
        }
       // anim.SetTrigger("Attack");

       lastAttackTime = Time.time;
       noOfAttacks++;
       if(noOfAttacks == 1)
       {
           anim.SetBool("Hit1", true);
       }
    
noOfAttacks = Mathf.Clamp(noOfAttacks, 0, 3);
     if (noOfAttacks >= 2 )
     {
         anim.SetBool("Hit1", false);
         anim.SetBool("Hit2", true);
     }

     if (noOfAttacks >= 3 )
     {
         anim.SetBool("Hit2", false);
         anim.SetBool("Hit3", true);
     }

       foreach(Collider enemy in hitEnemies)
       {
             IDamageable damageable;
        if (enemy.TryGetComponent<IDamageable>(out damageable))
        {
            damageable.TakeDamage(Damage);
        }
       }
    }

void OnDrawGizmosSelected()
{
    if (attackPoint == null)
    return;

    Gizmos.DrawWireSphere(attackPoint.position, attackRange);
}


}
