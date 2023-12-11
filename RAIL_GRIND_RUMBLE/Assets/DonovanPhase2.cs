using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class DonovanPhase2 : MonoBehaviour
{
    private GameObject player;
    private GrappleHook playerGrapple;
    [SerializeField] private float speed;
    private bool activated;
    private int health = 5;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<ThirdPersonMovement>().gameObject;
        playerGrapple = player.GetComponent<GrappleHook>();
    }

    // Update is called once per frame
    void Update()
    {
        if (activated && !playerGrapple.isGrappling)
        {
            Vector3 currentPos = transform.position;
            transform.position = new Vector3(currentPos.x, currentPos.y, currentPos.z + speed * Time.deltaTime);
        }
    }

    public void Activate()
    {
        activated = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Yeeouch!");
            health--;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
