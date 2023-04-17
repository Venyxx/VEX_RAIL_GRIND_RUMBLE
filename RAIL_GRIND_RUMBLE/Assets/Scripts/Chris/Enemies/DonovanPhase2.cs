using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using UnityEngine.SceneManagement;

public class DonovanPhase2 : MonoBehaviour, IDamageable
{
    public PathCreator path1;
    [SerializeField]float speed;
    float distanceTravelled;
    GameObject playerREF;
    int phase;
    bool aoeRunning;
    bool playerInRange;
    bool aoeDelay;
    float maxHealth = 500;
    float currentHealth;
    SphereCollider playerArea;
    BoxCollider hitArea;
    [SerializeField]GameObject damageText;
    private AudioClip[] hitSounds;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        playerREF = GameObject.FindWithTag("PlayerObject");
        phase = 1;
        aoeRunning = false;
        currentHealth = maxHealth;
        audioSource = GetComponent<AudioSource>();
        hitSounds = Resources.LoadAll<AudioClip>("Sounds/DamageSounds");
        playerInRange = false;
    }

    // Update is called once per frame
    void Update()
    {

        float singleStep = speed * Time.deltaTime;
        float playerDistance = Vector3.Distance(transform.position, playerREF.transform.position);

        //Rotate to face player
        Vector3 playerDirection = playerREF.transform.position - transform.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, playerDirection, singleStep, 0f);

        if (playerDistance < 6)
        {
            playerInRange = true;
        } else {
            playerInRange = false;
        }


        //Activate AOE
        if (!aoeRunning && playerInRange)
        {
            StartCoroutine(AOE());
        }

        if (!aoeDelay && playerDistance < 30)
        {
            distanceTravelled += singleStep;
            transform.position = path1.path.GetPointAtDistance(distanceTravelled);
            transform.rotation = Quaternion.LookRotation(newDirection);
        } else if (playerInRange && aoeDelay) {
            //Stay in place during attack, but in range of player's y position
            //transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, playerREF.transform.position.y + 2, transform.position.z), singleStep);
            transform.position = new Vector3(transform.position.x, playerREF.transform.position.y + 2, transform.position.z);
            //Turn to face player but don't face up or down
            newDirection = Vector3.RotateTowards(transform.forward, new Vector3(playerDirection.x, 0, playerDirection.z), singleStep, 0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
        } else if (playerDistance > 30){
            return;
        }

    }

    IEnumerator AOE()
    {
        aoeRunning = true;
        aoeDelay = true;
        Debug.Log("i'm gonna h*cking kill you idiot");
        yield return new WaitForSeconds(5f);
        Debug.Log("BWAAAAAAAAAAAH");
        if (playerInRange)
        {
            GameObject playerPrefab = GameObject.Find("playerPrefab");

            //Work on this
            //playerPrefab.GetComponent<Rigidbody>().AddForce(playerPrefab.transform.up * 55, ForceMode.Impulse);
            playerPrefab.GetComponent<Rigidbody>().AddForce((playerPrefab.transform.position - this.transform.position) * 150,  ForceMode.Acceleration);
            playerPrefab.GetComponent<PlayerHealth>().IsDizzy(true);
        }
        aoeDelay = false;
        yield return new WaitForSeconds(3f);
        aoeRunning = false;
    }

    //Damageable Functions - Consult PlayerHealth script for help
    public void TakeDamage(float damage)
    {
        //Add variables for maxHealth and currentHealth
        currentHealth -= damage;

        //Damage Numbers
        DamageIndicatior indicator = Instantiate (damageText, transform.position, Quaternion.identity).GetComponent<DamageIndicatior>();
        indicator.SetDamageText(damage);

        //Damage Sounds
        int rand = Random.Range(0, hitSounds.Length);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(hitSounds[rand]);
        }

        Debug.Log("Donovan Health: "+currentHealth);
        /*if (currentHealth <= 250 && phase == 1)
        {
            phase++;
        }*/

        if (currentHealth <= 0)
        {
            currentHealth = 0;

            //Temporary
            //Destroy(gameObject);
            SceneManager.LoadScene("EndCut");
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
