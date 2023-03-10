using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class DonovanPhase2 : MonoBehaviour, IDamageable
{
    public PathCreator path1;
    public PathCreator path2;
    float speed = 5;
    float distanceTravelled;
    GameObject playerREF;
    int phase;
    bool aoeRunning;
    bool playerInRange;
    float maxHealth = 500;
    float currentHealth;
    SphereCollider playerArea;
    BoxCollider hitArea;
    // Start is called before the first frame update
    void Start()
    {
        playerREF = GameObject.FindWithTag("PlayerObject");
        phase = 1;
        aoeRunning = false;
        currentHealth = maxHealth;
        //playerArea = GetComponent<SphereCollider>();
        //hitArea = GetComponent<BoxCollider>();
        //playerArea.enabled = true;
        //hitArea.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        float singleStep = speed * Time.deltaTime;

        //Test path switching
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (phase < 2)
            {
                phase++;
            }
        }

        //Rotate to face player
        Vector3 playerDirection = playerREF.transform.position - transform.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, playerDirection, singleStep, 0f);

        if (aoeRunning == false)
        {
            distanceTravelled += speed * Time.deltaTime;
            if (phase == 1)
            {
                transform.position = path1.path.GetPointAtDistance(distanceTravelled);
            } else if (phase == 2)
            {
                transform.position = path2.path.GetPointAtDistance(distanceTravelled);
            }
            transform.rotation = Quaternion.LookRotation(newDirection);
        } else {
            //Stay in place during attack, but in range of player's y position
            //transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, playerREF.transform.position.y + 2, transform.position.z), singleStep);
            transform.position = new Vector3(transform.position.x, playerREF.transform.position.y + 2, transform.position.z);
            //Turn to face player but don't face up or down
            newDirection = Vector3.RotateTowards(transform.forward, new Vector3(playerDirection.x, 0, playerDirection.z), singleStep, 0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }

    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("i'm gonna h*cking kill you idiot");
            playerInRange = true;
            if (aoeRunning == false)
            {
                StartCoroutine(AOE());
            }
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = false;
        }
    }

    IEnumerator AOE()
    {
        aoeRunning = true;
        //playerArea.enabled = false;
        //hitArea.enabled = true;
        yield return new WaitForSeconds(5f);
        Debug.Log("BWAAAAAAAAAAAH");
        if (playerInRange)
        {
            GameObject playerPrefab = GameObject.Find("playerPrefab");

            //Work on this
            playerPrefab.GetComponent<Rigidbody>().AddForce((playerPrefab.transform.position - this.transform.position) * 700,  ForceMode.Acceleration);
        }
        aoeRunning = false;
        /*yield return new WaitForSeconds(1f);
        playerArea.enabled = true;
        hitArea.enabled = false;*/
    }

    //Damageable Functions - Consult PlayerHealth script for help
    public void TakeDamage(float damage)
    {
        //Add variables for maxHealth and currentHealth
        currentHealth -= damage;
        Debug.Log("Donovan Health: "+currentHealth);
        if (currentHealth <= 250 && phase == 1)
        {
            phase++;
        }

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
