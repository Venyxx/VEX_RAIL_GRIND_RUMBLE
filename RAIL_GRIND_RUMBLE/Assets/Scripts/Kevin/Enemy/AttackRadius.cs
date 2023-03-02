using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

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
    public float thrust = 10;

    protected virtual void Awake()
    {
        Collider = GetComponent<SphereCollider>();
    }

    protected virtual void OnTriggerEnter(Collider other)
    { 
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
           
            if ( Movement.BruteIsCharging && enemy.Agent.speed > 0)
            {
                if (other.gameObject.tag == "Player")
                {
                    AttackCoroutine = StartCoroutine(Attack());
                    Vector3 knockbackVector =(Movement.Player.transform.position - enemy.Agent.transform.position) * 500;
                    damageable.IsDizzy(true);

                    other.attachedRigidbody.AddForce((Movement.Player.transform.position - enemy.Agent.transform.position) * 150,  ForceMode.Acceleration);
                    Debug.Log("KnockBackTest");
                }
                    damageable.TakeDamage(Damage);
               
            }
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
        while (enemy.isDizzy) yield return null;

        IDamageable closestDamageable = null;
        float closestDistance = float.MaxValue;

        if (!Movement.IsBrute)
        {
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
        if(Movement.IsBrute)
        {
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
        
    }

    protected bool DisabledDamageables (IDamageable Damageable)
    {
        return Damageable != null && !Damageable.GetTransform().gameObject.activeSelf;
    }
    
   
   
   
}
