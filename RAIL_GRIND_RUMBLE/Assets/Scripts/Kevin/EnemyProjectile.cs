
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
//Asignables
 public Rigidbody rb;
 public GameObject explosion;
 public LayerMask player;


 //Stats
 [Range(0f,1f)]
 public float bounciness;
 public bool useGravity;

 //Damage
 public int explosionDamage;
 public float explosionRange;

 //Lifetime
 public int maxCollisions;
 public float maxLifetime;
 public bool explodeOnTouch = true;

 int collisions;
 PhysicMaterial physics_mat;

 private void Start()
 {
	  Setup();
 }

 private void Update()
 {
	  //When to explode:
	  if (collisions > maxCollisions) Explode();

	  //Count down Lifetime
	  maxLifetime -= Time.deltaTime;
	  if (maxLifetime <= 0) Explode();
 }

 private void Explode()
 {
 //Instantiate explosion
 if (explosion != null) Instantiate(explosion, transform.position, Quaternion.identity);
 //check for player
 Collider[] players = Physics.OverlapSphere(transform.position, explosionRange, player);
 for (int i = 0; i <players.Length; i++)
 {
	  //this is where you get the player script and call the take damage function
 }
 Invoke("Delay", 0.05f);
 }

 private void Delay()
 {
	  Destroy(gameObject);
	 
 }

 private void OnTriggerEnter(Collider collision)
 {
	
	  //Count up collisions
	  collisions++;
	  if (collision.gameObject.tag == "Obstacle")
	  {
		  Explode();
	  }
	  
	  //Explode if bullet hits an enemy directly and explodeOnTouch is activated
	  //if (collision.collider.CompareTag("Player") && explodeOnTouch) Explode(); -- Unity refers to this line as "obsolete" when used with OnTriggerEnter/Collider component
	  if (collision.gameObject.tag == "Player" && explodeOnTouch)
	  {
	  Explode();
	  var healthComponent = collision.GetComponent<PlayerHealth>();
	  if(healthComponent != null)
	  {
			healthComponent.TakeDamage(1);
	  }
	 
	  }
 }

 private void Setup()
 {
	  //Create a new Physic material
	  physics_mat = new PhysicMaterial();
	  physics_mat.bounciness = bounciness;
	  physics_mat.frictionCombine = PhysicMaterialCombine.Minimum;
	  physics_mat.bounceCombine = PhysicMaterialCombine.Maximum;
	  //Assign material to collider
	  GetComponent<SphereCollider>().material = physics_mat;
	  //Set gravity
	  rb.useGravity = useGravity;
 } 

}
