using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class AttackRadius : MonoBehaviour
{
    public SphereCollider Collider;
    protected List<IDamageable> Damageables = new List<IDamageable>();
    public int Damage = 10;
    public float AttackDelay = 0.5f;
    public delegate void AttackEvent(IDamageable Target);
    public AttackEvent OnAttack;
    protected Coroutine AttackCoroutine;
    public bool IsSharpShooter = false;
    public EnemyMovement Movement;
    public Enemy enemy;

    protected virtual void Awake()
    {
        Collider = GetComponent<SphereCollider>();
    }

    protected virtual void OnTriggerEnter(Collider other)
    { 
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {

            Damageables.Add(damageable);
            if (enemy.Health > 0)
            {
                if (AttackCoroutine == null)
                {
                    // if (IsSharpShooter == false)
                    // {
                    AttackCoroutine = StartCoroutine(Attack());
                    //}
                    //if (IsSharpShooter == true)
                    // { 
                    //    if (Movement.HasHidden == false)
                    //  {
                    //     
                    //    Movement.startHiding(other.transform);
                    //AttackCoroutine = Movement.FollowCoroutine;
                    //}
                    //if (Movement.HasHidden == true)
                    //{
                    //   Movement.FollowCoroutine = AttackCoroutine ;


                    // AttackCoroutine = StartCoroutine(Attack());
                    //}
                    // }
                }
            }
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            Damageables.Remove(damageable);
            if (Damageables.Count == 0)
            {
                StopCoroutine(AttackCoroutine);
                AttackCoroutine = null;
            }
        }
    }

    protected virtual IEnumerator Attack()
    {
        WaitForSeconds Wait = new WaitForSeconds(AttackDelay);
        
        yield return Wait;

        IDamageable closestDamageable = null;
        float closestDistance = float.MaxValue;

        while (Damageables.Count > 0)
        {
            for (int i = 0; i < Damageables.Count; i++)
            {
                Transform damageableTransform = Damageables[i].GetTransform();
                float distance = Vector3.Distance(transform.position, damageableTransform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestDamageable = Damageables[i];
                }
            }

            if (closestDamageable != null)
            {
                OnAttack?.Invoke(closestDamageable);
                closestDamageable.TakeDamage(Damage);
            }
            closestDamageable = null;
            closestDistance = float.MaxValue;

            yield return Wait;

            Damageables.RemoveAll(DisabledDamageables);
        }
        AttackCoroutine = null;
        
    }

    protected bool DisabledDamageables (IDamageable Damageable)
    {
        return Damageable != null && !Damageable.GetTransform().gameObject.activeSelf;
    }
    
   
   
   
}
