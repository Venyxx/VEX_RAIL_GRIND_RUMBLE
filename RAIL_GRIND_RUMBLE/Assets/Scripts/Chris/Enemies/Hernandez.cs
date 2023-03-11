using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hernandez : MonoBehaviour
{
    [SerializeField] float stunSeconds;
    MachineGunTurret gatlingGunLeft;
    MachineGunTurret gatlingGunRight;
    RocketTurret rocketTurret;

    GameObject playerObjREF;

    bool stunned;
    bool grounded;
    bool playerInRange;
    bool machineGunDelayRunning;
    string currentWeapon;
    int weaponCycle;
    float speed = 5f;
    

    void Start()
    {
        playerObjREF = GameObject.FindWithTag("PlayerObject");

        gatlingGunLeft = gameObject.transform.Find("GatlingGunLeft").GetComponent<MachineGunTurret>();
        gatlingGunRight = gameObject.transform.Find("GatlingGunRight").GetComponent<MachineGunTurret>();
        rocketTurret = gameObject.transform.Find("RocketTurret").GetComponent<RocketTurret>();

        gatlingGunLeft.attached = true;
        gatlingGunRight.attached = true;
        rocketTurret.attached = true;

        weaponCycle = 1;
    }

    void Update()
    {
        
        var step = speed * Time.deltaTime;

        //Float above player if not stunned
        if (!stunned)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, playerObjREF.transform.position.y + 10, transform.position.z), step);
        }

        //Ranged attack if not stunned; can add additional condition to check if player is within melee attack range
        if (!stunned && !playerInRange)
        {
            //Shoots two rockets before activating gatling guns
            if (weaponCycle <= 2 && rocketTurret.shootRunning == false && rocketTurret.chargeRunning == false && gatlingGunLeft.shootRunning == false)
            {
                rocketTurret.StartCoroutine(rocketTurret.Charging());

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
            //grounded = (Physics.Raycast(new Vector3(transform.position.x, transform.position.y - 3.75f, transform.position.z), Vector3.down, 10f, LayerMask.NameToLayer("Ground")));
            if (grounded == false)
            {
                Debug.Log("Not Grounded");
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), step);
            }
        }
    }

    IEnumerator MachineGunDelay()
    {
        machineGunDelayRunning = true;
        yield return new WaitForSeconds(10f);
        machineGunDelayRunning = false;
    }

    //Need to test this
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "DroneThrow")
        {
            StartCoroutine(Stun());
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
        yield return new WaitForSeconds(stunSeconds);
        stunned = false;
    }
}
