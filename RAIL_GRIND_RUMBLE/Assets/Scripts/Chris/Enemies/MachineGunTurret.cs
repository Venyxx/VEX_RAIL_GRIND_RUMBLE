using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunTurret : MonoBehaviour, IDamageable
{
    [SerializeField] private Animator mechAnimator;
    float speed = 3f;
    GameObject playerREF;
    [SerializeField] GameObject bullet;
    [SerializeField] int bulletCount;
    int bulletInt = 0;
    [SerializeField] float cooldownSeconds;
    public bool attached;
    public bool shootRunning;
    Vector3 playerDirection;
    Vector3 newDirection;

    Hernandez hernandez;

    //Health
    float maxHealth = 300;
    float currentHealth;
    bool dead;

    // Start is called before the first frame update
    void Start()
    {
        playerREF = GameObject.FindWithTag("PlayerObject");
        dead = false;
        currentHealth = maxHealth;

        if (shootRunning == true) {
            shootRunning = false;
        }
    }

    public void SetAttached()
    {
        attached = true;
        hernandez = this.transform.parent.GetComponent<Hernandez>();
    }

    // Update is called once per frame
    void Update()
    {
            float singleStep = speed * Time.deltaTime;
            playerDirection = playerREF.transform.position - transform.position;
            newDirection = Vector3.RotateTowards(transform.forward, new Vector3(playerDirection.x, 0f, playerDirection.z), singleStep, 0f);

            if (!attached)
            {
                transform.rotation = Quaternion.LookRotation(newDirection);
            }

            if (!shootRunning && !dead && !attached && gameObject.activeInHierarchy)
            {
                Debug.Log("Starting shoot");
                StartCoroutine(Shoot());
            }
    }

    public IEnumerator Shoot()
    {
        if (attached)
        {
            mechAnimator.SetBool("shootingGun", true);
        }
        
        shootRunning = true;
        yield return new WaitForSeconds(0.8f);

        //Shoot amount of bullets specified under Bullet Count
        for (bulletInt = 0; bulletInt < bulletCount; bulletInt++)
        {
            if (!dead && gameObject.activeInHierarchy)
            {
                if (attached && hernandez.stunned == true)
                {
                    shootRunning = false;
                    yield break;
                } else {
                    GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.LookRotation(playerDirection));
                    newBullet.GetComponent<Bullet>().Rigidbody.AddForce(newBullet.transform.forward * newBullet.GetComponent<Bullet>().MoveSpeed, ForceMode.VelocityChange);
                    yield return new WaitForSeconds(0.3f);
                }
            } else if (!gameObject.activeInHierarchy)
            {
                shootRunning = false;
                yield break;
            }
        }

        //Cooldown
        if (attached)
        {
            mechAnimator.SetBool("shootingGun", false);
        }
        
        yield return new WaitForSeconds(cooldownSeconds);

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
        return;
        //if (attached) return;
        //Add variables for maxHealth and currentHealth
        //currentHealth -= damage;

        /*if (currentHealth <= 0)
        {
            currentHealth = 0;
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
