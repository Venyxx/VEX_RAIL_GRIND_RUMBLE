using UnityEngine;

public class MeleeEnemyAIController : MonoBehaviour,  IDamageable
{

public UnityEngine.AI.NavMeshAgent agent;

private Transform player;
private GameObject playerREF;

private PlayerHealth playerhealth;

public LayerMask whatIsGround, whatIsPlayer;
public float Health = 100;

//Patroling 
public Vector3 walkPoint;
bool walkPointSet;
public float walkPointRange;

//Attacking
public float timeBetweenAttacks;
bool alreadyAttacked;

//States
public float sightRange, attackRange;
public bool playerInSightRange, playerInAttackRange;

//Added by Chris - Removes enemies from Aim Points list upon death
GameObject grappleDetectorREF;

private void Awake()
{
	playerREF = GameObject.Find("playerPrefab");
	player = GameObject.Find("playerPrefab").transform;
	playerhealth = playerREF.GetComponent<PlayerHealth>();
	agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

	//Added by Chris
	grappleDetectorREF = GameObject.Find("GrappleDetector");
}

private void Update()
{
	//Check for sight and attack range
	playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
	playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

	if (!playerInSightRange && !playerInAttackRange) Patroling();
	if (playerInSightRange && !playerInAttackRange) ChasePlayer();
	if (playerInSightRange && playerInAttackRange) AttackPlayer();
}
private void Patroling()
{
	if (!walkPointSet) SearchWalkPoint();
	if (walkPointSet)
		agent.SetDestination(walkPoint);
	Vector3 distanceToWalkPoint = transform.position - walkPoint;

	//Walkpoint reached
	if (distanceToWalkPoint.magnitude < 1f)
	walkPointSet = false;
}
private void SearchWalkPoint()
{
//calculate random point in range
float randomZ = Random.Range(-walkPointRange, walkPointRange);
float randomX = Random.Range(-walkPointRange, walkPointRange);

walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
walkPointSet = true;
}
private void ChasePlayer()
{
agent.SetDestination(player.transform.position);
}
private void AttackPlayer()
{
//Attack code here
//Make sure enemy doesn't move
agent.SetDestination(transform.position);
transform.LookAt(player);

if (!alreadyAttacked)
{
	playerhealth.TakeDamage(1);
	alreadyAttacked = true;
	Invoke(nameof(ResetAttack), timeBetweenAttacks);

}
}
private void ResetAttack()
{
	alreadyAttacked = false;
}

  public void TakeDamage(float Damage)
    {
        Health -= Damage;
        if (Health <= 0)
        {
			//Added by Chris
			grappleDetectorREF.gameObject.GetComponent<GrappleDetection>().RemovePoint(GetComponent<Transform>());
			//

            Destroy(gameObject);
        }
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
