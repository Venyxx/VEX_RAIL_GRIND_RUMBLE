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
    [SerializeField] GameObject mesh;

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
                mesh.SetActive(false);
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
        DroneSpawner.droneCount--;
        Destroy(gameObject);
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
                //Destroy(gameObject);
            }
            
        }
        //Eventually make coroutine so trashcan rolls around on the ground for a little bit before disappearing (polish)
        else if (other.gameObject.tag == "Explosion" || other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            if (this.gameObject.tag != "DroneThrow")
            {
                Destroy(gameObject); 
            }
                
        }
    }
}
