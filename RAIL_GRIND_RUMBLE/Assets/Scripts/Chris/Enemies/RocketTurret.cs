using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketTurret : MonoBehaviour, IDamageable
{
    [SerializeField] GameObject rocket;
    Transform target;
    GameObject playerREF;
    float speed = 1f;
    public bool chargeRunning;
    public bool shootRunning;
    public bool attached;

    //Health
    float maxHealth = 300;
    float currentHealth;
    bool dead;

    // Start is called before the first frame update
    void Start()
    {
        playerREF = GameObject.FindWithTag("PlayerObject");
        target = this.gameObject.transform.Find("Target");
        currentHealth = maxHealth;
        dead = false;
        StartCoroutine(Shooting());
    }

    // Update is called once per frame
    void Update()
    {   
        //Turn turret toward player
        if (!chargeRunning && !attached)
        {
            float singleStep = speed * Time.deltaTime;
            Vector3 playerDirection = playerREF.transform.position - transform.position;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, playerDirection, singleStep, 0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }

        //Charging
        if(!chargeRunning && !shootRunning && !dead && !attached)
        {
            StartCoroutine(Charging());
        }

        //Targeting
        if (target.gameObject.activeInHierarchy == true)
        {
            target.transform.position = new Vector3(playerREF.transform.position.x, playerREF.transform.position.y, playerREF.transform.position.z);
        } else {
            target.transform.position = target.transform.position;
        }
    }

    public IEnumerator Charging()
    {
        chargeRunning = true;
        target.gameObject.SetActive(false);
        yield return new WaitForSeconds(15f);
        if (!dead)
        {
            StartCoroutine(Shooting());
        }
        chargeRunning = false;
    }

    IEnumerator Shooting()
    {
        shootRunning = true;
        target.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);

        var z = transform.rotation.eulerAngles.z;
        var y = transform.rotation.eulerAngles.y;
        var thisRot = Quaternion.Euler(new Vector3(90,y,z));
        var rot = Quaternion.Euler(new Vector3(-90, y, z));


        if (!dead)
        {
            GameObject rocketShot; 

            if (attached)
            {
                rocketShot = Instantiate(rocket, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            } else {
                rocketShot = Instantiate(rocket, new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), /*rot*/ Quaternion.identity);
            }

            Vector3 aimAt = new Vector3(target.position.x, target.position.y, target.position.z);

            if (!attached)
            {
                rocketShot.GetComponent<Rocket>().TargetSet(aimAt, true);
                StartCoroutine(Charging());
            } else {
                rocketShot.GetComponent<Rocket>().TargetSet(aimAt, false);
            }
        }

        if (!attached)
        {
            transform.rotation = thisRot;
        }
        
        shootRunning = false;
    }

    IEnumerator Regenerate()
    {
        dead = true;
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(30f);
        this.gameObject.GetComponent<MeshRenderer>().enabled = true;
        this.gameObject.GetComponent<BoxCollider>().enabled = true;
        currentHealth = maxHealth;
        dead = false;

    }

    //Damageable Functions - Consult PlayerHealth script for help
    public void TakeDamage(float damage)
    {
        if (attached) return;
        //Add variables for maxHealth and currentHealth
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            target.gameObject.SetActive(false);
            StartCoroutine(Regenerate());
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
