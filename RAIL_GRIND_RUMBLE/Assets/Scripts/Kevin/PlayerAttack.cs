using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Animator anim;
    public int Damage = 100;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
            anim.SetBool("Attacking", true);
        else if (Input.GetKeyUp(KeyCode.X))
            anim.SetBool("Attacking", false);
    }
      private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable;
        if (anim.GetBool("Attacking") == true)
        {
        if (other.TryGetComponent<IDamageable>(out damageable))
        {
            damageable.TakeDamage(Damage);
        }
        }
    }
}
