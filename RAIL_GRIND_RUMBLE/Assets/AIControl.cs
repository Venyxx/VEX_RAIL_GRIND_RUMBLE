using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIControl : MonoBehaviour {

    public GameObject [] goalLocations;
    NavMeshAgent agent;
    Animator anim;
    private AudioSource audioSource;
    public AudioClip AudioClip;
    private bool isEvading;
    private float maxTime = 2;
    private float currentTime;

    private float hitSpeed = 1;

    void Start() {

        
        audioSource = this.GetComponent<AudioSource>();
        agent = this.GetComponent<NavMeshAgent>();
        goalLocations = GameObject.FindGameObjectsWithTag("walkingGoal");
        int i = Random.Range(0, goalLocations.Length);
        agent.SetDestination(goalLocations[i].transform.position);
        anim = this.GetComponent<Animator>();
        anim.SetTrigger("isWalking");
        anim.SetFloat("wOffset", Random.Range(0.0f, 1.0f));
        float sm = Random.Range(0.5f,2);
        anim.SetFloat("speedMult", sm);
        agent.speed *= (sm * hitSpeed);

    }


    void Update() {
        if (agent.remainingDistance < 1)
        {
            //Debug.Log("changing direction   " + gameObject.name);
            int i = Random.Range(0, goalLocations.Length);
            agent.SetDestination(goalLocations[i].transform.position);
        }

        if (currentTime > 0 && isEvading)
            hitSpeed = 0;
        else
        {
            isEvading = false;
            hitSpeed = 1;
            currentTime -= Time.time;
        }
            


    }

    void OnTriggerEnter (Collider col)
    {
        if (col.gameObject.name == "playerPrefab")
        {
            anim.SetTrigger("evade");
            isEvading = true;
            currentTime = maxTime;
            Debug.Log("fall time");

            float sound = Random.Range(0,5);

            if (sound < 1)
            {
                audioSource.PlayOneShot(AudioClip);
            }

        }
    }
}