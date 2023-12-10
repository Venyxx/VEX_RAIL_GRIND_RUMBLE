using UnityEngine;
using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine.Serialization;

public class PlayerThrownObject : MonoBehaviour
{
    [FormerlySerializedAs("target")] public bool isTargeting;
    bool explosionRunning;
    private Transform playerCurrentAim;
    GameObject playerREF;
    //[SerializeField] GameObject explosion;
    //Kevin
    public int Damage = 10;
    [SerializeField] GameObject mesh;

    // Start is called before the first frame update
    void Start()
    {
        playerREF = GameObject.Find("GrappleDetector");
        explosionRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCurrentAim != null && playerCurrentAim.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Debug.Log($"Moving towards {playerCurrentAim}");
            transform.position = Vector3.MoveTowards(transform.position, playerCurrentAim.transform.position, 25f * Time.deltaTime);

            if (transform.position == playerCurrentAim.transform.position && explosionRunning == false && this.gameObject.tag == "DroneThrow")
            {
                StartCoroutine(Explosion());
            }
        }
    }

    void OnCollisionEnter (Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") || collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (this.gameObject.tag == "DroneThrow" && explosionRunning == false)
            {
                StartCoroutine(Explosion());
            } else {
                Destroy(gameObject);
            }
        }
    }

    IEnumerator Explosion()
    {
        explosionRunning = true;
        Debug.Log("BOOM");
        mesh.SetActive(false);
        Transform explosion = this.gameObject.transform.Find("Explosion");
        explosion.gameObject.SetActive(true);
        DroneSpawner.droneCount--;
        Debug.Log("Drone Count " + DroneSpawner.droneCount);
        yield return new WaitForSeconds(2f);
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

    public void SetCurrentPlayerAim(GameObject aimTarget)
    {
        playerCurrentAim = aimTarget.transform;
    }
}
