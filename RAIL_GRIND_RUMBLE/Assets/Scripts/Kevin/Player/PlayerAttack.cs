using UnityEngine;
using UnityEngine.InputSystem;
//using Random = System.Random;

public class PlayerAttack : MonoBehaviour
{
    public Animator Anim { get; private set; }
    public int Damage;
    public LayerMask enemyLayers;
    public bool IsAttacking = false;
    public bool IsHeavyAttacking = false;
    public static PlayerAttack instance;
    public Collider Leftleg;
    public Collider Rightleg;
    public Collider Lefthand;
    public Collider Righthand;
    public Collider Weapon;
    public int atkCount;
    public ThirdPersonMovement movementScriptREF;
    float HeavyAtkTimer;
    bool TimerOn;
    public GrappleHook grappleREF;
    private WallRun wallRunREF;

    
    public GameObject spinEffect;

    [SerializeField]
    private Rigidbody ariRigidbody;

    [SerializeField]
    private int slamSpeed; //RAUL

    //buff
    public bool isBuffed;
    public int buffDamage;
   // private AudioClip[] hitSounds;
  //  private AudioSource audioSource;

   public bool misControlEnabled = true; //stop overlapping actions - Raul


    private void Awake()
    {
        instance = this;
        Leftleg.enabled = false;
        Rightleg.enabled = false;
        Lefthand.enabled = false;
        Righthand.enabled = false;
        Weapon.enabled = false;
        movementScriptREF = GetComponent<ThirdPersonMovement>();
        wallRunREF = GetComponent<WallRun>();
        
    }

    void Start()
    {
        spinEffect.SetActive(false);
        GameObject ariRig = transform.Find("AriRig").gameObject;
        Anim = ariRig.GetComponent<Animator>();
        HeavyAtkTimer = 0;
     //   hitSounds = Resources.LoadAll<AudioClip>("Sounds/DamageSounds");
       // audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Timer();
        if (HeavyAtkTimer >= 3 && IsHeavyAttacking)
        {
           // Debug.Log("BugCheck1");
            Anim.SetTrigger("HAttackEnd2");
            
            TimerOn = false;
           // IsHeavyAttacking = false;
            HeavyAtkTimer = 0;
            Damage = 50;
        
        }
        if (!IsAttacking && atkCount == 0)
        {
            if (!IsHeavyAttacking) 
            {
                Damage = 0;
            }
            
        }

        //graffiti mulltiplier
        if (isBuffed)
        {
            //Debug.Log("running buff");
            if (atkCount == 1)
                Damage += 3;
            else if (atkCount == 2)
                Damage += 6;
            else if (atkCount == 3)
                Damage += 9;
        }

    }


    public void Attack(InputAction.CallbackContext context)
    {  //Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
        if (!context.started
            || movementScriptREF.isWalking ||
            GameObject.Find("AimingCam") ||
           // movementScriptREF.Grounded == false ||
            movementScriptREF.dialogueManager.freezePlayer
            //||movementScriptREF.nearestDialogueTemplate != null
            || GetComponent<ThrowObject>().isHoldingObject == true
            )
        {
            return;
        }


        if (context.started && !IsAttacking && !IsHeavyAttacking && !grappleREF.isGrappling && !wallRunREF.isWallRunning)
        {
            IsAttacking = true;
            atkCount++;
          

        }


       


        if (atkCount == 1 && !IsHeavyAttacking && IsAttacking && !grappleREF.isGrappling)
        {
            if (movementScriptREF.isJumping == false && movementScriptREF.Grounded == true)
            {
               
                if (movementScriptREF.print < 5)
                {
                    Debug.Log("SlowAtk");
                    Anim.SetTrigger("LAttackSlow");
                }
                if (movementScriptREF.print > 5 && movementScriptREF.print < 20)
                {
                    Debug.Log("MedAtk");
                    Anim.SetTrigger("LAttackMedium");
                }
                if (movementScriptREF.print > 20)
                {
                    Debug.Log("fastAtk");
                    Anim.SetTrigger("LAttackFast");
                }

            }
            if (movementScriptREF.isJumping == true || movementScriptREF.Grounded == false)
            {
                Anim.SetTrigger("AirLight");
            }
            
        }
        

        //    foreach(Collider enemy in hitEnemies)
        //  {
        //      IDamageable damageable;
        // if (enemy.TryGetComponent<IDamageable>(out damageable))
        // {
        //     damageable.TakeDamage(Damage);
        //}
        //}
    }

    public void HeavyAttack(InputAction.CallbackContext context)
    {  //Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
        if (!context.started ||
            movementScriptREF.isWalking ||
            GameObject.Find("AimingCam") ||
           // movementScriptREF.Grounded == false ||
            movementScriptREF.dialogueManager.freezePlayer
            || GetComponent<ThrowObject>().isHoldingObject == true
            )
        {

            return;
        }

       


        if (context.ReadValueAsButton() == true && !IsHeavyAttacking && atkCount == 0 && !grappleREF.isGrappling && !wallRunREF.isWallRunning)
        {
        //    Debug.Log("Key Press");
        
           

            if (movementScriptREF.Grounded == true && !movementScriptREF.isJumping)
            {
                Anim.SetTrigger("HAttackStart");
                TimerOn = true;
                IsHeavyAttacking = true;
                HeavyAtkTimer = 0;
                atkCount++;
            }
            if (movementScriptREF.isJumping == true || movementScriptREF.Grounded == false)
            {
                Anim.SetTrigger("AirHeavy");
                
                IsHeavyAttacking = true;
                atkCount++;
                ariRigidbody.AddForce(0, -slamSpeed, 0,  ForceMode.Acceleration); //RAUL

            }
        }

        if (context.ReadValueAsButton() == false && IsHeavyAttacking && movementScriptREF.Grounded == true)
        {
           
           // Debug.Log("Key Release");
           

            if (HeavyAtkTimer >= 1 && HeavyAtkTimer <= 1.9 && movementScriptREF.Grounded == true)
            {
               
                //   Debug.Log("BugCheck2");
                Anim.SetTrigger("HAttackEnd2");
                //    IsHeavyAttacking = false;
                HeavyAtkTimer = 0;
                spinEffect.SetActive(true);

                Damage = 35;
               
                
            }
            if (HeavyAtkTimer < 1 && !movementScriptREF.isJumping)
            {
             //   Debug.Log("BugCheck3");
                Anim.SetTrigger("HAttackEnd1");
                spinEffect.SetActive(true);
                //   IsHeavyAttacking = false;
                HeavyAtkTimer = 0;

                Damage = 30;
                
            }
            if (HeavyAtkTimer >= 2)
            {
            //    Debug.Log("BugCheck4");
                Anim.SetTrigger("HAttackEnd3");
                //  IsHeavyAttacking = false;
                HeavyAtkTimer = 0;
                spinEffect.SetActive(true);

                Damage = 100;
                
            }

            TimerOn = false;


        

        }

   
            

        //    foreach(Collider enemy in hitEnemies)
        //  {
        //      IDamageable damageable;
        // if (enemy.TryGetComponent<IDamageable>(out damageable))
        // {
        //     damageable.TakeDamage(Damage);
        //}
        //}
    }

    private void Timer()
    {
        if (TimerOn && IsHeavyAttacking)
        {
            HeavyAtkTimer += Time.deltaTime;
        }
        if (!TimerOn)
        {
            HeavyAtkTimer = 0;
            
        }

       // Debug.Log(HeavyAtkTimer);

    }


    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable;
       



        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Donovan" || other.gameObject.tag == "Hernandez")
        {

            if (Leftleg.enabled && other.TryGetComponent<IDamageable>(out damageable))
            {
                
                
                    Debug.Log("ATKtest1");
                damageable.TakeDamage(Damage);


            }
            if (Rightleg.enabled && other.TryGetComponent<IDamageable>(out damageable))
            {
                
                
                
                    Debug.Log("ATKtest1");
                damageable.TakeDamage(Damage);

            }

            if (Lefthand.enabled && other.TryGetComponent<IDamageable>(out damageable))
            {


                Debug.Log("ATKtest1");
                damageable.TakeDamage(Damage);


            }
            if (Righthand.enabled && other.TryGetComponent<IDamageable>(out damageable))
            {



                Debug.Log("ATKtest1");
                damageable.TakeDamage(Damage);

            }

            if (Weapon.enabled && other.TryGetComponent<IDamageable>(out damageable))
            {



                Debug.Log("ATKtest1");
                damageable.TakeDamage(Damage);

            }
        }
    }

    

    public void EnableControl()
    {
        misControlEnabled = true;
    }

    public void DisableControl()
    {
        misControlEnabled = false;
    }


    

    //void OnDrawGizmosSelected()
    //{
    //   if (attackPoint == null)
    //  return;

    //  Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    //}


}
