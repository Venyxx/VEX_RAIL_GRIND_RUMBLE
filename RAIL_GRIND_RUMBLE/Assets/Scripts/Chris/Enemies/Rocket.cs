using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Vector3 target;
    Transform explosion;
    float speed = 20f;
    bool spawnDelayRunning;
    void Start()
    {
        explosion = this.gameObject.transform.Find("Explosion");
    }

    // Update is called once per frame
    void Update()
    {
        var step = speed * Time.deltaTime;

        //float singleStep = speed * Time.deltaTime;
        

        if (spawnDelayRunning == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, step);
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, target, step, 0f);
            //transform.rotation = Quaternion.LookRotation(newDirection);
        } else {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), step * 2);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") || collision.gameObject.tag == "Player")
        {
            StartCoroutine(Explosion());
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
        explosion.gameObject.SetActive(true);
        GetComponent<MeshRenderer>().enabled = false;
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
