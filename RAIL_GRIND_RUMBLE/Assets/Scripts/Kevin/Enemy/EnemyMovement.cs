using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMovement : MonoBehaviour
{
    public Transform Player;
    public LayerMask HidableLayers;
    public EnemyLineOfSightChecker LineOfSightChecker;
    public float UpdateSpeed = 0.1f;
    [SerializeField]
    private Animator Animator;

    [SerializeField] private float activationDistance = 600f;

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
    public Enemy enemy;
    
    public bool IsBrute = false;
    float totalDistance;
    public bool BruteIsCharging;
    public bool BruteChargingDelay = false;

    private const string IsWalking = "IsWalking";

    public bool activated = false;
    public bool isMoving = false;
    private CutscenePlayer _cutscenePlayer;

    private void Awake ()
    {
        Agent = GetComponent<NavMeshAgent>();

        LineOfSightChecker.OnGainSight += HandleGainSight;
        LineOfSightChecker.OnLoseSight += HandleLoseSight;

        //Added by Chris to prevent unassigned reference exception
        Player = GameObject.FindWithTag("Player").transform;
        _cutscenePlayer = FindObjectOfType<CutscenePlayer>();


    }
    private void Update()
    {
        totalDistance = Vector3.Distance(Player.position, Agent.transform.position);
        Animator.SetBool(IsWalking, Agent.velocity.magnitude > 0.01f);

        if (Vector3.Distance(Player.position, transform.position) < activationDistance && !activated && enemy.Health > 1 && !enemy.isDizzy)
        {
            //making sure enemies don't come after the player while a cutscene is playing, since 
            //Time.timeScale = 0 doesn't seem to be working in CutscenePlayer.cs for some reason - Pete
            if ((_cutscenePlayer != null && !_cutscenePlayer.cutscenePlaying) || _cutscenePlayer == null)
            {
                //Debug.Log("Player is within range, chasing the player");
                StartCoroutine(Activate());
                activated = true;
            }
        }

        if (Vector3.Distance(Player.position, transform.position) > activationDistance && !BruteIsCharging )
        {
            StopAllCoroutines();
            activated = false;
           
            
        }

        if (enemy.Health <=0)
        {
            //Debug.Log("BugCheck1");
           
            StopAllCoroutines ();
            activated = false;
        }
        if ( enemy.isDizzy)
        {
          
            activated = false;
            StopAllCoroutines();
        }

        if (IsBrute )
        {
            


            if (activated && totalDistance > 5 && !BruteChargingDelay && !Attack.BruteWindingUp )
            {
                StartCoroutine(Charge());
            }

          

        }
        if (Vector3.Distance(Player.position, transform.position) >= 15 && activated && !enemy.isDizzy)
        {
            
            UpdateSpeed = .01f;
            


        }

        if (Vector3.Distance(Player.position, transform.position) < 15  && Vector3.Distance(Player.position, transform.position) >= .5 && !enemy.isDizzy)
        {
            
            UpdateSpeed = .005f;
          


        }

        if (Vector3.Distance(Player.position, transform.position) <.5 && !enemy.isDizzy)
        {
            
            UpdateSpeed = .001f;
           


        }
        if (activated && Vector3.Distance(Player.position, transform.position) < 2 && !enemy.isDizzy )
        {// Agent.SetDestination(Player.transform.position);
          }

        if (IsBrute && !activated )
        {
            BruteChargingDelay = false;
        }
    }
   
    private void HandleGainSight(Transform Target)
    {
      //  if (FollowCoroutine != null)
        //{
          //  StopCoroutine(FollowCoroutine);
        //}
       // Player = Target;
        //FollowCoroutine = StartCoroutine(Hide(Target));
    }

    private void HandleLoseSight (Transform Target)
    {
   if (FollowCoroutine != null)
        {
           
           //Animator.SetBool("Dashing", false);
            HasHidden = true;
            
        }
       
       
    }

    public void StartChasing()
    {

        if (FollowCoroutine == null )
        {
            FollowCoroutine = StartCoroutine(Activate()); //change this to StartCoroutine(FollowTarget()); for spawner
        }
        else
        {
            Debug.LogWarning("Called StartChasing on Enemy that is already chasing! This is likely a bug in some calling class!");
        }
        
    }
    
    public IEnumerator Activate()
    {

        WaitForSeconds Wait =  new WaitForSeconds(UpdateSpeed); 
        while(gameObject.activeSelf)
        {
            if (Agent.enabled)
            {
                isMoving = true;
                Agent.SetDestination(Player.transform.position);
             
            }
            yield return new WaitForSeconds(UpdateSpeed); 
            isMoving = false;
        }
    }

    public IEnumerator Charge()
    {
        //if (totalDistance <= 5)
        //{ yield return null; }
        
        BruteChargingDelay = true;
        Debug.Log("Brute is charging");
        BruteIsCharging = true;
        Agent.speed = 0;
        Animator.SetTrigger("Charging");
        yield return new WaitForSeconds(2f);
        Animator.SetBool("Dashing", true);
        Attack.Damage = 10;
        Agent.speed = enemy.EnemyScriptableObject.Speed + 9;
        Agent.acceleration = enemy.EnemyScriptableObject.Acceleration + 9;
        yield return new WaitForSeconds(3f);
        Attack.Attack();
        Debug.Log("Brute isn't charging");
        Animator.SetBool("Dashing", false);
        //BruteChargingDelay = false; //putting this here to speed up testing - Raul
        Agent.speed = 3;
        Attack.Damage = 5;
        Agent.acceleration = 8;
         BruteIsCharging = false;
        
        Agent.SetDestination(Player.transform.position);
        yield return new WaitForSeconds(10f);
        BruteChargingDelay = false;
        StopCoroutine(Charge());
        Attack.Attack();
              

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
     FollowCoroutine = StartCoroutine(Activate());
    
}

}
