using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    public GameObject[] Locations = new GameObject[5];
    private GameObject playerPrefab;

    void Start ()
    {
        playerPrefab = GameObject.Find("playerPrefab");
    }

    void OnTriggerEnter (Collider col)
    {
        if (col.tag == "Teleporter")
        {
            for (int i = 0 ; i < Locations.Length; i ++ )
            {
                if (col.gameObject == Locations[i])
                    playerPrefab.transform.position = Locations[i + 1].transform.position;
            }
        }
      
    }
    
}
