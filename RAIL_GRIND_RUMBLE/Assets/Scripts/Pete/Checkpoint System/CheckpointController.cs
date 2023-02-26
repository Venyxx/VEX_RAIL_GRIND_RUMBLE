using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointController : MonoBehaviour
{
    public static int LastScene { get; private set; }
    public static Vector3 lastCheckPointPosition;
    private GameObject ari;

    private Teleportation teleREF;


    private void Start()
    {
        ari = GameObject.Find("playerPrefab");
        if (!lastCheckPointPosition.Equals(new Vector3(0,0,0)))
            ari.transform.position = lastCheckPointPosition;
        Time.timeScale = 1;
        LastScene = SceneManager.GetActiveScene().buildIndex;

        if (GameObject.Find("TeleportManager"))
            teleREF = GameObject.Find("TeleportManager").GetComponent<Teleportation>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("PlayerObject"))
        {
            lastCheckPointPosition = this.gameObject.transform.position;
        }
    }
    
    

}
