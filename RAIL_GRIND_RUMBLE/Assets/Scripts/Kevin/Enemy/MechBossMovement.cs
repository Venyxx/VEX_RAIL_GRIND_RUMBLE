using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MechBossMovement : MonoBehaviour , IDamageable
{
    public NavMeshAgent agent;
    public Animator Animator;
    private Transform player;
    private GameObject playerREF;
    public GameObject damageText;

    public LayerMask whatIsGround, whatIsPlayer;
    public float Health = 300;
    public int KnockDownTime = 5;
    private PlayerHealth playerhealth;

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
    public bool playerInSightRange, playerInAttackRange, MechDown, MechUp;
    public bool Dizzy;


    private void Awake()
    {
        playerREF = GameObject.Find("playerPrefab");
        player = GameObject.Find("playerPrefab").transform;
        agent = GetComponent<NavMeshAgent>();
        Speed = agent.speed;
        playerhealth = playerREF.GetComponent<PlayerHealth>();
    }
    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange && !Dizzy && !MechUp)
        { Patroling();
            transform.LookAt(player);
        }
        //if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        //if (playerInSightRange && playerInAttackRange) AttackPlayer();
      
        if (agent.baseOffset > 0 && MechDown)
        {
            agent.speed = 0;
            Animator.SetTrigger("KnockDown");
            walkPointSet = false;
            agent.baseOffset = agent.baseOffset - 15 * Time.deltaTime;
            if (!Dizzy)
            {
                StartCoroutine(KnockDown());
            }
        }
        if (agent.baseOffset < 10 && MechUp)
        {
            agent.baseOffset = agent.baseOffset + 3 * Time.deltaTime;
            agent.speed = Speed;
        }
        if (agent.baseOffset >= 10 && MechUp)
        {
            MechUp = false;
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
        float randomY = Random.Range(0, 10);


        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y + randomY, transform.position.z + randomZ);
        if (Physics.Raycast(walkPoint, -transform.up, 50f, whatIsGround) && !MechDown)
        { 
            walkPointSet = true;
    }
    }
    private void ChasePlayer()
    {
      //  agent.SetDestination(player.transform.position);
    }
    private void AttackPlayer()
    {
        //Attack code here
        //Make sure enemy doesn't move
       // agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
            if(playerInAttackRange)
            {
                playerhealth.TakeDamage(50);
                playerhealth.IsDizzy(true);

                playerREF.GetComponent<ThirdPersonMovement>().rigidBody.AddForce((player.transform.position - agent.transform.position) * 150, ForceMode.Acceleration);
            }

        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(float damage)
    {
        
        if (Dizzy)
        {
            Debug.Log("MechHitfor" + (damage));
            Health -= damage;
            DamageIndicatior indicator = Instantiate(damageText, transform.position, Quaternion.identity).GetComponent<DamageIndicatior>();
            indicator.SetDamageText(damage);
        }
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
        Dizzy = true;
        yield return new WaitForSeconds(KnockDownTime);
        MechDown = false;
        Debug.Log("MechTest");
        Animator.SetTrigger("GetUp");
        yield return new WaitForSeconds(1);
        Animator.SetTrigger("Attack");
        yield return new WaitForSeconds(1.5f);
        AttackPlayer();
        yield return new WaitForSeconds(3);
        Dizzy = false;
        
        MechUp = true;
    }
}
