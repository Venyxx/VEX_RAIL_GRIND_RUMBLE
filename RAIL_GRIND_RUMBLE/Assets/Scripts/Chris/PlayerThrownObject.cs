using UnityEngine;
using System.Collections;

public class PlayerThrownObject : MonoBehaviour
{
    public bool target;
    Transform playerCurrentAim;
    GameObject playerREF;
    //[SerializeField] GameObject explosion;
    //Kevin
    public int Damage = 10;

    // Start is called before the first frame update
    void Start()
    {
        playerREF = GameObject.Find("GrappleDetector");
        playerCurrentAim = playerREF.gameObject.GetComponent<GrappleDetection>().currentAim;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == true && playerCurrentAim.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            transform.position = Vector3.MoveTowards(transform.position, playerCurrentAim.transform.position, 25f * Time.deltaTime);
        }
    }

    void OnCollisionEnter (Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") || collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (this.gameObject.tag == "DroneThrow")
            {
                Debug.Log("BOOM");
                StartCoroutine(Explosion());
            } else {
                Destroy(gameObject);
            }
            
        }

        
    }

    IEnumerator Explosion()
    {
        Transform explosion = this.gameObject.transform.Find("Explosion");
        explosion.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
        DroneSpawner.droneCount--;
    }


    //kevin
    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable;
        if (other.TryGetComponent<IDamageable>(out damageable))
        {
            
            if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                damageable.IsDizzy(true);
                damageable.TakeDamage(Damage);
                
                
            }
            
        }
    }
}
