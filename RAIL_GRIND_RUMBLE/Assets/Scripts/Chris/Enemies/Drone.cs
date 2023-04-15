using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    GameObject playerREF;
    [SerializeField] GameObject bullet;
    Vector3 playerPos;
    float speed = 5f;
    float degrees = 45f;
    bool moving;
    bool shootDelayRunning;
    public bool bossDrone;
    Hernandez hernandez;
    GrappleDetection grappleDetection;
    // Start is called before the first frame update
    void Start()
    {
        playerREF = GameObject.FindWithTag("PlayerObject");
        moving = false;
        shootDelayRunning = false;

        grappleDetection = GameObject.Find("GrappleDetector").GetComponent<GrappleDetection>();
    }

    // Update is called once per frame
    void Update()
    {
        var step = speed * Time.deltaTime;
        playerPos = new Vector3(playerREF.transform.position.x, playerREF.transform.position.y + 15, playerREF.transform.position.z);
        float distance = Vector3.Distance(playerPos, transform.position);
        //Debug.Log("Distance = "+distance);

        if (distance > 20f)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerPos, step);
        } else {
            transform.RotateAround(playerPos, Vector3.up, degrees*Time.deltaTime);
            if (shootDelayRunning == false)
            {
                StartCoroutine(ShootDelay());
            }
        }

        if (bossDrone && !hernandez && GameObject.FindWithTag("Hernandez"))
        {
            hernandez = GameObject.FindWithTag("Hernandez").GetComponent<Hernandez>();
        }

        if (bossDrone && !GameObject.FindWithTag("Hernandez"))
        {
            RemoveFromAimList();
        }

        if (hernandez)
        {
            if (bossDrone && hernandez.stunned)
            {
                RemoveFromAimList();
            }
        }
        
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Explosion")
        {
            RemoveFromAimList();
        }
    }

    IEnumerator ShootDelay()
    {
        shootDelayRunning = true;
        var lookPos = playerREF.transform.position - transform.position;

        /*if (bossDrone == true && hernandez.stunned == true)
        {
            yield break;
        } else*/{
            GameObject shootBullet = Instantiate(bullet, transform.position, Quaternion.LookRotation(lookPos));
            shootBullet.GetComponent<Bullet>().Rigidbody.AddForce(shootBullet.transform.forward * shootBullet.GetComponent<Bullet>().MoveSpeed, ForceMode.VelocityChange);
        }
        yield return new WaitForSeconds(10f);
        shootDelayRunning = false;
    }

    void RemoveFromAimList()
    {
        DroneSpawner.droneCount--;
        grappleDetection.aimPoints.Remove(this.transform);
        Destroy(gameObject);
    }

}
