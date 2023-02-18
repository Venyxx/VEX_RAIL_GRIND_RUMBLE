using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthProxy : MonoBehaviour, IDamageable
{
    public Enemy enemy;
     
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.Health <=0)
        {
            gameObject.layer = LayerMask.NameToLayer("Ragdoll");
        }
        
    }
    public void TakeDamage(float Damage)
    {
     
        enemy.TakeDamage(Damage);
       
    }

    public void GainHealth(float GainHealth)
    {


    }
    public Transform GetTransform()
    {
       
        return enemy.transform;
    }

    public void IsDizzy(bool isDizzy)
    {
        
    }
   
}
