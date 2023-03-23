using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketExplosion : MonoBehaviour
{
    public int Damage;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable;
        if (other.TryGetComponent<IDamageable>(out damageable))
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("player"))
            {
                damageable.TakeDamage(Damage);
            }
            else if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
              //  damageable.TakeDamage(Damage * 10);
            }
          

        }
    }
}
