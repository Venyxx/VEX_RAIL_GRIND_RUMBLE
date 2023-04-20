using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hernandez : MonoBehaviour, IDamageable
{
    

    [SerializeField] private Animator mechAnimator;
    [SerializeField] float stunSeconds;
    MachineGunTurret gatlingGunLeft;
    MachineGunTurret gatlingGunRight;
    RocketTurret rocketTurret;

    GameObject playerObjREF;

    public bool stunned;
    int stunCount;
    bool grounded;
    bool playerInRange;
    bool machineGunDelayRunning;
    string currentWeapon;
    int weaponCycle;
    float speed = 5f;

    //Health
    float maxHealth = 1750f;
    float currentHealth;
    //MechBossMovementReff
    private MechBossMovement mechmovement;
    

    void Start()
    {
        playerObjREF = GameObject.FindWithTag("PlayerObject");
        mechmovement = GetComponent<MechBossMovement>();

        gatlingGunLeft = gameObject.transform.Find("GatlingGunLeft").GetComponent<MachineGunTurret>();
        gatlingGunRight = gameObject.transform.Find("GatlingGunRight").GetComponent<MachineGunTurret>();
        rocketTurret = gameObject.transform.Find("RocketTurret").GetComponent<RocketTurret>();

        gatlingGunLeft.SetAttached();
        gatlingGunRight.SetAttached();
        rocketTurret.SetAttached();

        weaponCycle = 1;
        stunCount = 0;
        currentHealth = maxHealth;
    }

    void Update()
    {
        
        var step = speed * Time.deltaTime;

        //Float above player if not stunned
        if (!stunned)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, playerObjREF.transform.position.y + 10, transform.position.z), step);

            //Rotate to face player
            Vector3 playerDirection = playerObjREF.transform.position - transform.position;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, new Vector3(playerDirection.x, 0f, playerDirection.z), step, 0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }

        //Ranged attack if not stunned; can add additional condition to check if player is within melee attack range
        if (!stunned && !playerInRange && !mechmovement.Dizzy && !mechmovement.MechDown)
        {
            //Shoots two rockets before activating gatling guns
            if (weaponCycle <= 2 && rocketTurret.shootRunning == false && rocketTurret.chargeRunning == false && gatlingGunLeft.shootRunning == false)
            {

                rocketTurret.StartCoroutine(rocketTurret.Shooting());
                

                if (weaponCycle == 2)
                {
                    StartCoroutine(MachineGunDelay());
                }

                weaponCycle++;
                Debug.Log("Weapon Cycle:" + weaponCycle);
              //Activates gatling guns and resets weapon cycle  
            } else if (weaponCycle == 3 && gatlingGunLeft.shootRunning == false && machineGunDelayRunning == false)
            {
                
                gatlingGunLeft.StartCoroutine(gatlingGunLeft.Shoot());
                gatlingGunRight.StartCoroutine(gatlingGunRight.Shoot());
                weaponCycle = 1;
                Debug.Log("Weapon Cycle:" + weaponCycle);
            }

        //Add additional else if statement above this for when not stunned and player IS in range for melee attack
        } else if (stunned)
        {
            if (!grounded)
            {
            //grounded = (Physics.Raycast(new Vector3(transform.position.x, transform.position.y - 3.75f, transform.position.z), Vector3.down, 10f, LayerMask.NameToLayer("Ground")));
                //Debug.Log("Not Grounded");
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), step);
            }
        }
    }

    IEnumerator MachineGunDelay()
    {
        machineGunDelayRunning = true;
        yield return new WaitForSeconds(7f);
        machineGunDelayRunning = false;
    }

    //Need to test this
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "DroneThrow" && stunned == false)
        {
            //Hit the mech with two drones to stun them
            if (stunCount == 0)
            {
                stunCount++;
            } else if (stunCount == 1) {
                StartCoroutine(Stun());
                stunCount = 0;
            }
           
        }
        
        //Figure out why tf this doesn't work
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            grounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            grounded = false;
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = true;
            Debug.Log("In-Range for Melee");
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = false;
            Debug.Log("Out of Range for Melee");
        }
    }

    IEnumerator Stun()
    {
        stunned = true;
        Debug.Log("Stunning!");
        yield return new WaitForSeconds(stunSeconds + 2f);
        stunned = false;
    }

    public void TakeDamage(float damage)
    {
        if (!stunned) return;
        //Add variables for maxHealth and currentHealth
        
        currentHealth -= damage;
        Debug.Log("Hernandez Health: "+currentHealth);
        

        if (currentHealth <= 0)
        {
            currentHealth = 0;

            //Temporary
            Destroy(gameObject);
        }
    }

    public void GainHealth(float GainHealth)
    {
        //Recharge health after being destroyed for 30 seconds
        return;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void IsDizzy(bool isDizzy)
    {
        return;
    }
}
