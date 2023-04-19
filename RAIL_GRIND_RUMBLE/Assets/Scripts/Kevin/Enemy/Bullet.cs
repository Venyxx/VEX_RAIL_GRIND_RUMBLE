using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : PoolableObject
{
    public float AutoDestroyTime = 5f;
    public float MoveSpeed = 2f;
    public int Damage = 5;
    public Rigidbody Rigidbody;

    private const string DISABLE_METHOD_NAME = "Disable";

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        
    }

    private void OnEnabled()
    {
        CancelInvoke(DISABLE_METHOD_NAME);
        Invoke(DISABLE_METHOD_NAME, AutoDestroyTime);
        
        
    }

     private void Update()
 {
	

	  //Count down Lifetime
	  AutoDestroyTime -= Time.deltaTime;
	  if (AutoDestroyTime <= 0) Disable();
 }


    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable;
        if (other.TryGetComponent<IDamageable>(out damageable))
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("player"))
            {
                if (other.gameObject.GetComponent<ThirdPersonMovement>().isStunned == true)
                {
                    return;
                }
            } else if (other.gameObject.CompareTag("Donovan"))
            {
                return;
            } 

            damageable.TakeDamage(Damage);
        }
        Disable();
    }
    private void Disable()
    {
        CancelInvoke(DISABLE_METHOD_NAME);
        Rigidbody.velocity = Vector3.zero;
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
   
 
}
