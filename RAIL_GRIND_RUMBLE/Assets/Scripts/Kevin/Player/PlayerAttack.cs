using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public Animator anim;
    public int Damage;
    public LayerMask enemyLayers;
    public bool IsAttacking = false;
    public bool IsHeavyAttacking = false;
    public static PlayerAttack instance;
    public Collider Weapon;
    public Collider Knee;
    public int atkCount;
    private ThirdPersonMovement movementScriptREF;
    float HeavyAtkTimer;
    bool TimerOn;


    private void Awake()
    {
        instance = this;
        Weapon.enabled = false;
        Knee.enabled = false;
        movementScriptREF = GetComponent<ThirdPersonMovement>();
    }

    void Start()
    {
        //  anim = GetComponent<Animator>();
        HeavyAtkTimer = 0;
       

    }

    void Update()
    {
        Timer();
        if (HeavyAtkTimer >= 3 && IsHeavyAttacking)
        {
            Debug.Log("BugCheck1");
            anim.SetTrigger("HAttackEnd2");
            TimerOn = false;
           // IsHeavyAttacking = false;
            HeavyAtkTimer = 0;
            Damage = 50;
        
        }
        if (!IsAttacking)
        {
            if (!IsHeavyAttacking) 
            {
                Damage = 0;
            }
            
        }

    }


    public void Attack(InputAction.CallbackContext context)
    {  //Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);
        if (!context.started
            || movementScriptREF.isWalking ||
            GameObject.Find("AimingCam") ||
            movementScriptREF.Grounded == false ||
            movementScriptREF.dialogueBox.activeInHierarchy
            //||movementScriptREF.nearestDialogueTemplate != null
            )
        {
            return;
        }


        if (context.started && !IsAttacking && !IsHeavyAttacking)
        {
            IsAttacking = true;
            atkCount++;


        }


        if (atkCount == 1 && !IsHeavyAttacking)
        {
            anim.SetTrigger("LAttack");
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
            movementScriptREF.Grounded == false ||
            movementScriptREF.dialogueBox.activeInHierarchy)
        {

            return;
        }

        if (context.started || !IsAttacking)
        {
            // Debug.Log("HeavyAttack1" + context.phase);  



        }


        if (context.ReadValueAsButton() == true && !IsHeavyAttacking && atkCount == 0)
        {
            Debug.Log("Key Press");
            anim.SetTrigger("HAttackStart");
            TimerOn = true;
            IsHeavyAttacking = true;
            HeavyAtkTimer = 0;
            atkCount++;


        }

        if (context.ReadValueAsButton() == false && IsHeavyAttacking)
        {
           
            Debug.Log("Key Release");
            if (HeavyAtkTimer >= 1 && HeavyAtkTimer <= 1.9)
            {
                Debug.Log("BugCheck2");
                anim.SetTrigger("HAttackEnd2");
            //    IsHeavyAttacking = false;
                HeavyAtkTimer = 0;
                Damage = 35;
                
            }
            if (HeavyAtkTimer < 1)
            {
                Debug.Log("BugCheck3");
                anim.SetTrigger("HAttackEnd1");
             //   IsHeavyAttacking = false;
                HeavyAtkTimer = 0;
                Damage = 30;
                
            }
            if (HeavyAtkTimer >= 2)
            {
                Debug.Log("BugCheck4");
                anim.SetTrigger("HAttackEnd3");
              //  IsHeavyAttacking = false;
                HeavyAtkTimer = 0;
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
        if (other.TryGetComponent<IDamageable>(out damageable))
        {
            damageable.TakeDamage(Damage);
        }
    }

    //void OnDrawGizmosSelected()
    //{
    //   if (attackPoint == null)
    //  return;

    //  Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    //}


}
