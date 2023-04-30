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
    [SerializeField] private Animator copAnimator;
    [SerializeField] private Animator servosAnimator;
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
    bool CanTakeDamage = true;
    public float _takeDamageDelay = .2f;
    public bool dizzy = false;
    public bool isDizzy;
    public static Enemy instance;

    //damage indicator
    public GameObject damageText;
    private bool dead;
    public GameObject VFX;
    public bool IsDonnovan;

    private void Awake()
    {
        AttackRadius.OnAttack += OnAttack;
        //Added for Chris
	    grappleDetectorREF = GameObject.Find("GrappleDetector");
        
    }

    void Start()
    {
        if (VFX != null)
        {
            VFX.SetActive(false);
        }
        DespawnTimer = 0;
        hitSounds = Resources.LoadAll<AudioClip>("Sounds/DamageSounds");
        audioSource = GetComponent<AudioSource>();
        if(Movement.IsBrute)
        {
            instance = this;
        }
    }

    void Update()
    {
        
        Timer();
        if (DespawnTimer >= 3)
        {
            
            gameObject.SetActive(false);
            TimerOn = false;
        }
        if (Movement.BruteChargingDelay && Health > 0)
        {

            if (LookCoroutine != null)
            {
                StopCoroutine(LookCoroutine);
            }
            LookCoroutine = StartCoroutine(LookAt(Movement.Player));
        }
        if (Movement.isMoving && !isDizzy && Health >0)
        {

          
            LookCoroutine = StartCoroutine(LookAt(Movement.Player));
        }
        if(CanTakeDamage)
        {
            if (VFX != null)
            {
                VFX.SetActive(false);
            }
        }
    }
    private void OnAttack(IDamageable Target)
    {
        if (!Movement.IsBrute)
        {
            Animator.SetTrigger(ATTACK_TRIGGER);
            Animator.SetBool("isAttacking", true);
            StartCoroutine(PunchAnimCooldown());
            if (LookCoroutine != null)
            {
                StopCoroutine(LookCoroutine);
            }
            LookCoroutine = StartCoroutine(LookAt(Target.GetTransform()));
        }
        if (Movement.IsBrute)
        {
            if(!AttackRadius.BruteWindingUp)
            {
                Animator.SetTrigger(ATTACK_TRIGGER);
            }
            
               
              
            
            if (LookCoroutine != null)
            {
                 StopCoroutine(LookCoroutine);
            }
            LookCoroutine = StartCoroutine(LookAt(Target.GetTransform()));

        }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public IEnumerator PunchAnimCooldown()
    {
        Animator.SetBool("isAttacking", true);
        yield return new WaitForSeconds(1);
        Animator.SetBool("isAttacking", false);
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    private IEnumerator LookAt (Transform Target)
    {
        Quaternion lookRotation = Quaternion.LookRotation((Target.position - transform.position)) ;
        float time = 0;
        
        while (time < 1)
        {
        ;
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, time);
            time += Time.deltaTime + 5;
            yield return null;
        }
        transform.rotation = lookRotation ;
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
    public void TakeDamage(float damage)
    {
        
        if (damage > 0)
        {
            if(CanTakeDamage)
            {
                CanTakeDamage = false;
                if (dizzy)
                {
                    StartCoroutine(Dizzy());

                }
                
                
                
                StartCoroutine(TakeDamage());
                Health -= damage;

                //damage indicator
                DamageIndicatior indicator = Instantiate (damageText, transform.position, Quaternion.identity).GetComponent<DamageIndicatior>();
                indicator.SetDamageText(damage);
            }
           
        }
        
        if (Health <= 0 && !dead)
        {
            dead = true;
            ProgressionManager tracker = ProgressionManager.Get();
            Debug.Log(tracker.CurrentCountQuestType);
            if (tracker.CurrentCountQuestType is CountQuestType.Enemies)
            {
                Debug.Log("Calling IncrementCount");
                CountQuest quest = (CountQuest)tracker.currentQuest;
                quest.IncrementCount();
            }
            if (IsDonnovan)
            {
                gameObject.SetActive(false);
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

          
            
                Ragdoll.StartRagdoll = true;
                Debug.Log("START RAGDOLL");

                //Added by Chris for adaptive music
                GrappleDetection grappleDetection = GameObject.Find("GrappleDetector").GetComponent<GrappleDetection>();
                if (grappleDetection.enemiesInRange.Exists(element => element == (this.gameObject.transform)))
                {
                    grappleDetection.enemiesInRange.Remove(this.gameObject.transform);
                    //grappleDetection.adaptiveMusic.StartCoroutine(grappleDetection.adaptiveMusic.SwitchSongs("Standard"));
                }

                //Animator.SetBool("ragdollEnabled", true);
            
            
            

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
    public void IsDizzy(bool isDizzy)
    {
        dizzy = isDizzy;
    }
    public Transform GetTransform()
    {
        return transform;
    }

    private IEnumerator TakeDamage()
    {
        if (Health > 0)
        {
            Random rand = new Random();
            audioSource.PlayOneShot(hitSounds[rand.Next(0, hitSounds.Length)]);


            if(!dizzy && !isDizzy)
            {
                if (VFX != null)
                {
                    VFX.SetActive(true);
                }

                Animator.SetTrigger("TakeDamage");
                Animator.SetBool("isDamaged", true);
                //copAnimator.SetBool("isDamaged", true);
            }
                
                yield return new WaitForSeconds(_takeDamageDelay);
                CanTakeDamage = true;
                 Animator.SetBool("isDamaged", false);
                 copAnimator.SetBool("isDamaged", false);

                 if (VFX != null)
                 {
                     VFX.SetActive(false);
                 }
        }

        
    }

    public IEnumerator Dizzy()
    {
        if (Health > 0)
        {
            
            Animator.SetTrigger("DizzyStart");
            isDizzy = true;
            dizzy = false;
            Debug.Log("Dizzy Start");
            yield return new WaitForSeconds(5);
            Debug.Log("Dizzy End");
            Animator.SetTrigger("DizzyEnd");
            isDizzy = false;
          //  Movement.activated = true;


        }


    }



    private void Timer()
    {
        if (TimerOn)
        {
          DespawnTimer += Time.deltaTime;
            //Debug.Log(DespawnTimer);
            
        }
        if (!TimerOn)
        {
           // DespawnTimer = 0;
        }

        

    }

    public bool IsDead()
    {
        return dead;
    }

}