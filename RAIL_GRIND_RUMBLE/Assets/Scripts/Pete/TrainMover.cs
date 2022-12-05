using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class TrainMover : MonoBehaviour
{


    [Tooltip("TWO SPOTS MAX")][SerializeField] GameObject[] spots;
    [SerializeField] private float speed;
    [SerializeField] private float timeInTheStation;
    [SerializeField] private int damageValue = 100;

    private GameObject lastSpotVisited;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = spots[0].transform.position;
        lastSpotVisited = spots[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (lastSpotVisited == spots[1])
        {
            Debug.Log("moving towards spot 0");
            transform.position = Vector3.MoveTowards(transform.position, spots[0].transform.position, speed * Time.deltaTime);
            transform.LookAt(spots[0].transform.position);
        }
        else if(lastSpotVisited == spots[0])
        {
            Debug.Log("moving towards spot 1");
            transform.position = Vector3.MoveTowards(transform.position, spots[1].transform.position, speed * Time.deltaTime);
            transform.LookAt(spots[1].transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.gameObject == spots[1])
        {
           StartCoroutine(WaitInTheStation(1));
        }

        if (other.gameObject == spots[0])
        {
            StartCoroutine(WaitInTheStation(0));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        PlayerHealth playerHealthRef = collision.gameObject.GetComponent<PlayerHealth>();
        if (playerHealthRef != null)
        {
            playerHealthRef.TakeDamage(damageValue);
        }
    }

    IEnumerator WaitInTheStation(int spotNumber)
    {
        yield return new WaitForSeconds(timeInTheStation);
        lastSpotVisited = spots[spotNumber];
    }

}
