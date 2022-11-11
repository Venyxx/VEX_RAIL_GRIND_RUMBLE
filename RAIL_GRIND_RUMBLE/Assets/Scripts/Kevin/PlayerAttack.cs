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
   
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    public void Attack(InputAction.CallbackContext context)
    {  Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
        if (!context.started)
        {
            return;
        }
        anim.SetTrigger("Attack");
     

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
