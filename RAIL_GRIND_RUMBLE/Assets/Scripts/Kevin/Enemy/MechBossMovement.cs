using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MechBossMovement : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator Animator;
    private Transform player;

    public LayerMask whatIsGround, whatIsPlayer;
    public float Health = 100;

    //Patroling 
    public Vector3 walkPoint;
   public bool walkPointSet;
    public float walkPointRange;
    [SerializeField]
    private float Speed = 8; 

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
  //  public GameObject projectile;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange, MechDown;
    public bool Dizzy;


    private void Awake()
    {
        player = GameObject.Find("playerPrefab").transform;
        agent = GetComponent<NavMeshAgent>();
        Speed = agent.speed;
    }
    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        //if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        //if (playerInSightRange && playerInAttackRange) AttackPlayer();
      
        if (agent.baseOffset > 0 && MechDown)
        {
            agent.speed = 0;
            Animator.SetTrigger("KockDown");
            walkPointSet = false;
            agent.baseOffset = agent.baseOffset - 15 * Time.deltaTime;
        }
    }
    private void Patroling()
    {
        if (!walkPointSet && !MechDown) SearchWalkPoint();
        if (walkPointSet && !MechDown)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 20f || MechDown)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        float randomY = 8;


        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y + randomY, transform.position.z + randomZ);
        if (Physics.Raycast(walkPoint, -transform.up, 50f, whatIsGround) && !MechDown)
        { 
            walkPointSet = true;
    }
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
            //Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();

            //rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            //rb.AddForce(transform.up * 5f, ForceMode.Impulse);
            //alreadyAttacked = true;
            //Invoke(nameof(ResetAttack), timeBetweenAttacks);

        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public Transform GetTransform()
    {
        return transform;
    }
    public void GainHealth(float GainHealth)
    {

    }
    public void IsDizzy(bool Dizzy)
    {
       
       
    }
    public IEnumerator KnockDown()
    {
        yield return new WaitForSeconds(5);
    }
}
