using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (Input.GetKeyDown(KeyCode.X))
        {
            Attack();
        }
     
    }
    void Attack()
    {
        anim.SetTrigger("Attack");
       Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

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
