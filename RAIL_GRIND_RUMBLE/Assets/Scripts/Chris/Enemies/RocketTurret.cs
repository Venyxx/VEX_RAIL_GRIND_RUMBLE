using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketTurret : MonoBehaviour, IDamageable
{
    [SerializeField] private Animator mechAnimator;
    [SerializeField] GameObject rocket;
    [SerializeField] GameObject targetObject;
    GameObject target;
    GameObject playerREF;
    float speed = 3f;
    public bool chargeRunning;
    public bool shootRunning;
    bool attached;

    //Reload Values
    public float rechargeTime;
    public float shootTime;

    Hernandez hernandez;

    //Health
    float maxHealth = 300;
    float currentHealth;
    bool dead;

    // Start is called before the first frame update
    void Start()
    {
        playerREF = GameObject.FindWithTag("PlayerObject");
        currentHealth = maxHealth;
        dead = false;
        target = null;

        if (shootRunning == true) {
            shootRunning = false;
        }

        //StartCoroutine(Shooting());
    }

    public void SetAttached()
    {
        attached = true;
        hernandez = this.transform.parent.GetComponent<Hernandez>();
    }

    // Update is called once per frame
    void Update()
    {   
        //Turn turret toward player
        if (!chargeRunning && !attached)
        {
            float singleStep = speed * Time.deltaTime;
            Vector3 playerDirection = playerREF.transform.position - transform.position;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, new Vector3(playerDirection.x, 0f, playerDirection.z), singleStep, 0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }

        //Charging
        if(!chargeRunning && !shootRunning && !dead && !attached)
        {
            StartCoroutine(Charging());
        }

        //Targeting
        if (target != null)
        {
            target.transform.position = new Vector3(playerREF.transform.position.x, playerREF.transform.position.y+0.1f, playerREF.transform.position.z);
        } /*else {
            target.transform.position = target.transform.position;
        }*/
    }

    public IEnumerator Charging()
    {
        

        chargeRunning = true;

        if (attached)
        {
            yield return new WaitForSeconds(15f);
        } else {
            yield return new WaitForSeconds(rechargeTime);
        }
        
        if (!dead)
        {
            StartCoroutine(Shooting());
        }

        chargeRunning = false;
    }

    public IEnumerator Shooting()
    {

        shootRunning = true;
        //target.gameObject.SetActive(true);
        

        target = Instantiate(targetObject, playerREF.transform.position, Quaternion.identity);

        if (attached)
        {
            yield return new WaitForSeconds(5f);

        } else {
        //mechAnimator.SetBool("shootingMissile", false);
            yield return new WaitForSeconds(shootTime);

        }

        

        var z = transform.rotation.eulerAngles.z;
        var y = transform.rotation.eulerAngles.y;
        var thisRot = Quaternion.Euler(new Vector3(90,y,z));
        var rot = Quaternion.Euler(new Vector3(-90, y, z));




        StartCoroutine(shootMissileWait());


        /*if (!attached)
        {
            transform.rotation = thisRot;
        }*/
        
        //target.gameObject.SetActive(false);
    }

    IEnumerator shootMissileWait()
    {
        if (!dead)
        {
            if (attached && hernandez.stunned == true)
            {
                shootRunning = false;
                //target.gameObject.SetActive(false);
                Destroy(target.gameObject);
                yield break;

            } else 
            
            {
                if (attached)
                {
                    mechAnimator.SetTrigger("shootingMissile");
                }
                yield return new WaitForSeconds(0.9f);
                GameObject rocketShot; 
                

                if (attached)
                {
                    rocketShot = Instantiate(rocket, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
                    
                } else {
                    rocketShot = Instantiate(rocket, new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), /*rot*/ Quaternion.identity);
                }

                Vector3 aimAt = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z);

                if (!attached)
                {
                    // mechAnimator.SetBool("shootingMissile", true);
                    // Debug.Log("StartMissile");
                    rocketShot.GetComponent<Rocket>().TargetSet(aimAt, true);
                    StartCoroutine(Charging());
                } else {
                    rocketShot.GetComponent<Rocket>().TargetSet(aimAt, false);
                }

                Destroy(target.gameObject);
                shootRunning = false;
            }
        }
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
        return;
        /*if (attached) return;
        //Add variables for maxHealth and currentHealth
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            //target.gameObject.SetActive(false);
            Destroy(target.gameObject);
            StartCoroutine(Regenerate());
        }*/
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
