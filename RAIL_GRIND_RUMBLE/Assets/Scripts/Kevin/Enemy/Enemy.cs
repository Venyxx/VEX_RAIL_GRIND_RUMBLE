using System.Collections;
using UnityEngine.AI;
using UnityEngine;
using Random = System.Random;

public class Enemy : PoolableObject, IDamageable
{
    public AttackRadius AttackRadius;
    public EnemyMovement Movement;
    public NavMeshAgent Agent;
    public EnemyScriptableObject EnemyScriptableObject;
    public float Health = 100;
    public Animator Animator;
    public Ragdollenabler Ragdoll;

    private Coroutine LookCoroutine;
    private const string ATTACK_TRIGGER = "Attack";
     private const string DEATH_TRIGGER = "Death";

    //Added for Chris - Removes enemies from Aim Points list upon death
    GameObject grappleDetectorREF;
    float DespawnTimer;
    bool TimerOn;
    private AudioClip[] hitSounds;
    private AudioSource audioSource;
    bool CanTakeDamage;

    private void Awake()
    {
        AttackRadius.OnAttack += OnAttack;
        //Added for Chris
	    grappleDetectorREF = GameObject.Find("GrappleDetector");
        
    }

    void Start()
    {
        DespawnTimer = 0;
        hitSounds = Resources.LoadAll<AudioClip>("Sounds/DamageSounds");
        audioSource = GetComponent<AudioSource>();

    }

    void Update()
    {
        
        Timer();
        if (DespawnTimer >= 3)
        {
            
            gameObject.SetActive(false);
            TimerOn = false;
        }
        
      
      
    }
    private void OnAttack(IDamageable Target)
    {
        Animator.SetTrigger(ATTACK_TRIGGER);
        if (LookCoroutine != null)
        {
            StopCoroutine(LookCoroutine);
        }
        LookCoroutine = StartCoroutine(LookAt(Target.GetTransform()));
    }

    private IEnumerator LookAt (Transform Target)
    {
        Quaternion lookRotation = Quaternion.LookRotation(Target.position - transform.position);
        float time = 0;

        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, time);
            time += Time.deltaTime * 20;
            yield return null;
        }
        transform.rotation = lookRotation;
    }

    public virtual void OnEnable()
    {
         SetupAgentFromConfiguration(); 
    }
    public override void OnDisable()
    {
        //base.OnDisable(); turn this off when placing enemy
        Agent.enabled = false;
    }
    public virtual void SetupAgentFromConfiguration()
    {
        Agent.acceleration = EnemyScriptableObject.Acceleration;
        Agent.angularSpeed = EnemyScriptableObject.AngularSpeed;
        Agent.areaMask = EnemyScriptableObject.AreaMask;
        Agent.avoidancePriority = EnemyScriptableObject.AvoidancePriority;
        Agent.baseOffset = EnemyScriptableObject.BaseOffset;
        Agent.height = EnemyScriptableObject.Height;
        Agent.obstacleAvoidanceType = EnemyScriptableObject.ObstacleAvoidanceType;
        Agent.radius = EnemyScriptableObject.Radius;
        Agent.speed = EnemyScriptableObject.Speed;
        Agent.stoppingDistance = EnemyScriptableObject.StoppingDistance;

        Movement.UpdateSpeed = EnemyScriptableObject.AIUpdateInterval;

        Health = EnemyScriptableObject.Health;

        AttackRadius.Collider.radius = EnemyScriptableObject.AttackRadius;
        AttackRadius.AttackDelay = EnemyScriptableObject.AttackDelay;
        AttackRadius.Damage = EnemyScriptableObject.Damage;
    }
    public void TakeDamage(float Damage)
    {
        
        if (Damage > 0)
        {
            Random rand = new Random();
            audioSource.PlayOneShot(hitSounds[rand.Next(0, hitSounds.Length)]);
        }
        Health -= Damage;
        if (Health <= 0)
        {

            QuestTracker tracker = FindObjectOfType<QuestTracker>();
            if (tracker.CurrentCountQuestType is CountQuestType.Enemies)
            {
                CountQuest quest = (CountQuest)tracker.CurrentQuest;
                quest.IncrementCount();
            }

            Movement.activated = false;
            if (!TimerOn)
            {
                Animator.SetTrigger("Death");
            }
            
            TimerOn = true;
            
            //set layer to ragdoll layer on death so it doesn't collide with player
            gameObject.layer = 20;
            foreach (Transform child in gameObject.transform)
            {
                child.gameObject.layer = 20;
            }

            if( !EnemyScriptableObject.IsRanged)
            {
                Ragdoll.StartRagdoll = true;
            }
            
            
            

            // StopCoroutine(LookCoroutine);

            //Added for Chris 
            //(commenting out for now since we're reworking how enemy grappling works)
            //grappleDetectorREF.gameObject.GetComponent<GrappleDetection>().RemovePoint(GetComponent<Transform>());

            // LookCoroutine = StartCoroutine(Death());
        }
    }
    public void GainHealth (float GainHealth)
    {
    
        
    }
    public Transform GetTransform()
    {
        return transform;
    }

    private IEnumerator Death()
    {
        Debug.Log("BugCheck1");
        float time = 0;
        while (time < 3)
        {
            Debug.Log("BugCheck2");
            Ragdoll.EnableRagdoll();
            time += Time.deltaTime;
            yield return null;
        }
        if (time == 3)
        {
           
            
        }
        if (time == .01)
        {
            Debug.Log("BugCheck4");
            
        }
        yield return new WaitForSeconds(4);
    }

    private void Timer()
    {
        if (TimerOn)
        {
          DespawnTimer += Time.deltaTime;
            Debug.Log(DespawnTimer);
            
        }
        if (!TimerOn)
        {
           // DespawnTimer = 0;
        }

        

    }
   
}