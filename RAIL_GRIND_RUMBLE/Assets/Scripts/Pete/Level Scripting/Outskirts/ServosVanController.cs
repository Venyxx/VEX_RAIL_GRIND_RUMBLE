using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using UnityEngine;

public class ServosVanController : MonoBehaviour
{
    [SerializeField] Transform[] spots;
    [SerializeField] private float speed;
    [SerializeField] private float startDelay;
    [SerializeField] private GameObject enemy;
    [SerializeField] private int goonsToSpawn = 5;
    private GameObject leftDoor;
    private GameObject rightDoor;
    private Vector3 doorsPos;

    private int _currentSpot;
    private bool canMove;
    void Start()
    {
        transform.position = spots[0].transform.position;
        rightDoor = transform.Find("door1").gameObject;
        leftDoor = transform.Find("door2").gameObject;
        Invoke("Activate", startDelay);
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove) return; 
        
        if (_currentSpot == spots.Length - 1)
        {
            MainQuest1 mainQuest1 = ProgressionManager.Get().mainQuest1;
            mainQuest1.VanDestinationReached();
            if (leftDoor != null && rightDoor != null)
            {
                Destroy(rightDoor);
                Destroy(leftDoor);
                doorsPos = leftDoor.transform.position;
                if (goonsToSpawn != 0)
                {
                    Invoke("SpawnGoons", 2f);

                }
            }
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, spots[_currentSpot + 1].position, (speed/2) * Time.deltaTime);
        transform.LookAt(spots[_currentSpot + 1].position);
        
        if (transform.position.Equals(spots[_currentSpot + 1].position))
        {
            _currentSpot++;
        }
    }

    void Activate()
    {
        canMove = true;
    }

    private int goonCount = 0;
    void SpawnGoons()
    {
        if (goonCount >= goonsToSpawn) return;
        Instantiate(enemy, doorsPos, Quaternion.identity);
        Invoke("SpawnGoons", 2f);
        goonCount++;
    }
}
