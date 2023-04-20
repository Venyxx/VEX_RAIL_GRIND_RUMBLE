using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Vector3 target;
    Transform explosion;
    bool explosionRunning;
    float speed = 20f;
    bool spawnDelayRunning;
    GameObject rocket;
    void Start()
    {
        explosion = this.gameObject.transform.Find("Explosion");
        explosionRunning = false;
        rocket = this.gameObject.transform.Find("rocketModel").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        var step = speed * Time.deltaTime;

        //float singleStep = speed * Time.deltaTime;
        

        if (spawnDelayRunning == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, step * 2);
            //Vector3 newDirection = Vector3.RotateTowards(transform.forward, target, step * 2, 0f);
            //transform.rotation = Quaternion.LookRotation(newDirection);
        } else {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), step * 2);
        }

        if (transform.position == target && explosionRunning == false)
        {
            StartCoroutine(Explosion());
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") || collision.gameObject.tag == "Player" || collision.gameObject.layer == LayerMask.NameToLayer("wallrun"))
        {
            if (explosionRunning == false)
            {
                StartCoroutine(Explosion());
            }
        }
    }
    
    public void TargetSet(Vector3 targetPos, bool launchUp)
    {
        target = targetPos;
        if (launchUp)
        {
            StartCoroutine(SpawnDelay());
        }
    }

    IEnumerator Explosion()
    {
        explosionRunning = true;
        explosion.gameObject.SetActive(true);
        //GetComponent<MeshRenderer>().enabled = false;
        rocket.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    IEnumerator SpawnDelay()
    {
        spawnDelayRunning = true;
        yield return new WaitForSeconds(0.5f);
        spawnDelayRunning = false;
    }
}
