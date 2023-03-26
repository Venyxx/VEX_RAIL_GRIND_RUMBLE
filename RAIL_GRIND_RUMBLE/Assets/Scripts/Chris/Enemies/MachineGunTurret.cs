using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunTurret : MonoBehaviour, IDamageable
{
    float speed = 1f;
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
            newDirection = Vector3.RotateTowards(transform.forward, playerDirection, singleStep, 0f);

            if (!attached)
            {
                transform.rotation = Quaternion.LookRotation(newDirection);
            }

            if (!shootRunning && !dead && !attached)
            {
                StartCoroutine(Shoot());
            }
    }

    public IEnumerator Shoot()
    {
        shootRunning = true;

        //Shoot amount of bullets specified under Bullet Count
        for (bulletInt = 0; bulletInt < bulletCount; bulletInt++)
        {
            if (!dead)
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
            }
        }

        //Cooldown
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
        if (attached) return;
        //Add variables for maxHealth and currentHealth
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
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
