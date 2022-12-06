using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    public Transform Player;
    public LayerMask HidableLayers;
    public EnemyLineOfSightChecker LineOfSightChecker;
    public float UpdateSpeed = 0.1f;
    [SerializeField]
    private Animator Animator;

    private NavMeshAgent Agent;
    [Range(-1, 1)]
    [Tooltip("Lower is a better hiding spot")]
    public float HideSensitivity = 0;
    [Range(1, 20)]
    public float MinPlayerDistance = 5f;
    [Range (0, 5f)]
    public float MinObstacleHeight = 1.25f;
    [Range(0.01f, 1f)]
    public float UpdateFrequency = 0.25f; 
    public Coroutine FollowCoroutine;
    private Collider[] Colliders = new Collider[10]; // more is less performant, but more options
    public bool HasHidden = false;
    public AttackRadius Attack;

    private const string IsWalking = "IsWalking";
    

    private void Awake ()
    {
        Agent = GetComponent<NavMeshAgent>();

        LineOfSightChecker.OnGainSight += HandleGainSight;
        LineOfSightChecker.OnLoseSight += HandleLoseSight;
       
    }
    private void Update()
    {
        Animator.SetBool(IsWalking, Agent.velocity.magnitude > 0.01f);
    }

    private void HandleGainSight(Transform Target)
    {
      //  if (FollowCoroutine != null)
        //{
          //  StopCoroutine(FollowCoroutine);
        //}
        Player = Target;
        //FollowCoroutine = StartCoroutine(Hide(Target));
    }

    private void HandleLoseSight (Transform Target)
    {
   if (FollowCoroutine != null)
        {
           
            
            HasHidden = true;
            
        }
       
       
    }

    public void StartChasing()
    {
        if (FollowCoroutine == null)
        {
            FollowCoroutine = StartCoroutine(Start()); //change this to StartCoroutine(FollowTarget()); for spawner
        }
        else
        {
            Debug.LogWarning("Called StartChasing on Enemy that is already chasing! This is likely a bug in some calling class!");
        }
        
    }

    private IEnumerator Start() //change this to FollowTarget for spawner 
    {
        yield return new WaitForSeconds(60);
      WaitForSeconds Wait = new WaitForSeconds(UpdateSpeed);
        while(gameObject.activeSelf)
       {
            if (Agent.enabled)
          {
            Agent.SetDestination(Player.transform.position);
            }
          yield return Wait;
        }
   

      
    }
  
    private IEnumerator Hide(Transform Target)
    {
        WaitForSeconds Wait = new WaitForSeconds(UpdateFrequency);
        while (true)
        {
            for (int i = 0; i < Colliders.Length; i++)
            {
               Colliders[i] = null;
            }
            int hits = Physics.OverlapSphereNonAlloc(Agent.transform.position, LineOfSightChecker.Collider.radius, Colliders, HidableLayers);
            
            int hitReduction = 0;

            for (int i = 0; i < hits; i++)
            {
                if (Vector3.Distance(Colliders[i].transform.position, Target.position) < MinPlayerDistance || Colliders[i].bounds.size.y < MinObstacleHeight)
                {
                    Colliders[i] = null;
                    hitReduction++;
                }
            }
            hits -= hitReduction;
            System.Array.Sort(Colliders, ColliderArraySortComparer);
            for (int i = 0; i < hits; i++)
            {
                if (NavMesh.SamplePosition(Colliders[i].transform.position, out NavMeshHit hit, 20f, Agent.areaMask))
                {
                    if (!NavMesh.FindClosestEdge(hit.position, out hit, Agent.areaMask))
                    {
                        Debug.LogError($"Unable to find edge close to {hit.position}");
                    }

                    if (Vector3.Dot(hit.normal, (Target.position - hit.position).normalized) < HideSensitivity)
                    {
                        Agent.SetDestination(hit.position);
                       
                        break;
                    }
                    else
                    {
                        if (NavMesh.SamplePosition(Colliders[i].transform.position -(Target.position - hit.position).normalized * 2, out NavMeshHit hit2, 20f, Agent.areaMask))
                        {
                            if (!NavMesh.FindClosestEdge(hit2.position, out hit2, Agent.areaMask))
                            {
                                Debug.LogError($"Unable to find edge close to {hit2.position} (second attempt)");
                            }

                            if (Vector3.Dot(hit2.normal, (Target.position - hit2.position).normalized) < HideSensitivity)
                            {
                                Agent.SetDestination(hit2.position);
                                
                                
                                
                                break;
                            }
                        }
                    }
                }
                else
                {
                    Debug.LogError($"Unable to find NavMesh near object {Colliders[i].name} at {Colliders[i].transform.position}");
                }
            }
            
            yield return Wait;
            
           
        }
    }
    public int ColliderArraySortComparer(Collider A, Collider B)
    {
        if (A == null && B != null)
        {
            return 1;
        }
        else if (A != null && B == null)
        {
            return -1;
        }
        else if (A == null && B == null)
        {
            return 0;
        }
        else
        {
            return Vector3.Distance(Agent.transform.position, A.transform.position).CompareTo(Vector3.Distance(Agent.transform.position, B.transform.position));
        }
    }

public void startHiding (Transform Target)
{
     StopCoroutine(FollowCoroutine);
    FollowCoroutine = StartCoroutine(Hide(Target));
    
    
}
public void startChasing (Transform Target)
{
     
     StopCoroutine(FollowCoroutine);
    // FollowCoroutine = StartCoroutine(FollowTarget());
    
}

}
