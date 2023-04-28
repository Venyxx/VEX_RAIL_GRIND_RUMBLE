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
    private float maxTime = 3;
    private float currentTime;
    private ThirdPersonMovement tpMovREF;

    private string thisNPCsOptions;

    public enum Goal // your custom enumeration
    {
    A, 
    B, 
    C,
    D,
    E,
    F,
    G
    };
    public Goal goal;
    private float hitSpeed = 1;
    float sm;

    void Start() {
        thisNPCsOptions = goal.ToString();
        Debug.Log(goal.ToString());

        tpMovREF = GameObject.Find("playerPrefab").GetComponent<ThirdPersonMovement>();

        audioSource = this.GetComponent<AudioSource>();
        agent = this.GetComponent<NavMeshAgent>();
        goalLocations = GameObject.FindGameObjectsWithTag(thisNPCsOptions);
        int i = Random.Range(0, goalLocations.Length);
        agent.SetDestination(goalLocations[i].transform.position);
        anim = this.GetComponent<Animator>();
        anim.SetTrigger("isWalking");
        anim.SetFloat("wOffset", Random.Range(0.0f, 1.0f));
         sm = Random.Range(0.5f,1.15f);
        anim.SetFloat("speedMult", sm);
        



    }


    void Update() {

        agent.speed = (1 * sm * hitSpeed);
        
        if (agent.isOnNavMesh && agent.remainingDistance < 1)
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
        if (tpMovREF.moveInput.y != 0 || tpMovREF.moveInput.x != 0)
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

    
}