using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public Animator anim;
    public int Damage;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public bool IsAttacking = false;
    public static PlayerAttack instance;
    public Collider Weapon; 


    private void Awake()
    {
        instance = this;
        Weapon.enabled = false;

    }
   
    void Start()
    {
        anim = GetComponent<Animator>();
        
    }
    void Update()
    {
      
    }
   

    public void Attack(InputAction.CallbackContext context)
    {  //Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
        if (!context.started)
        {
            return;
        }
       

        if (context.started && !IsAttacking)
        {
            IsAttacking = true;
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
