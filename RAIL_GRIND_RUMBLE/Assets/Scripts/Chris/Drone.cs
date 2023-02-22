using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    GameObject playerREF;
    Vector3 playerPos;
    float speed = 5f;
    bool moving;
    // Start is called before the first frame update
    void Start()
    {
        playerREF = GameObject.FindWithTag("PlayerObject");
        moving = false;
    }

    // Update is called once per frame
    void Update()
    {
        var step = speed * Time.deltaTime;
        playerPos = new Vector3(playerREF.transform.position.x, playerREF.transform.position.y + 15, playerREF.transform.position.z);

        if (transform.position.x != playerPos.x && transform.position.z != playerPos.z)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerPos, step);
        }       
    }

    /*IEnumerator Move()
    {
        
        moving = true;
        while (transform.position.x != playerPos.x && transform.position.z != playerPos.z)
        {
            
        }
        moving = false;
        yield return null;
    }*/
}
