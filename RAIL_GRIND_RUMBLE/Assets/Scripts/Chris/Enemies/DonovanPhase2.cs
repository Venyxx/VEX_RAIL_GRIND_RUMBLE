using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class DonovanPhase2 : MonoBehaviour
{
    public PathCreator path1;
    public PathCreator path2;
    float speed = 5;
    float distanceTravelled;
    GameObject playerREF;
    int phase;
    bool aoeRunning;
    // Start is called before the first frame update
    void Start()
    {
        playerREF = GameObject.FindWithTag("PlayerObject");
        phase = 1;
        aoeRunning = false;
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
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, playerREF.transform.position.y + 2, transform.position.z), singleStep);

            //Turn to face player but don't face up or down
            newDirection = Vector3.RotateTowards(transform.forward, new Vector3(playerDirection.x, 0, playerDirection.z), singleStep, 0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }

    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("i'm gonna fucking kill you idiot");

            if (aoeRunning == false)
            {
                StartCoroutine(AOE());
            }
        }
    }

    IEnumerator AOE()
    {
        aoeRunning = true;
        yield return new WaitForSeconds(5f);
        Debug.Log("BWAAAAAAAAAAAH");
        //other.attachedRigidbody.AddForce((Movement.Player.transform.position - enemy.Agent.transform.position) * 150,  ForceMode.Acceleration);
        aoeRunning = false;
    }
}
