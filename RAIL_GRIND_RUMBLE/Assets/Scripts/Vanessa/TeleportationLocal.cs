using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationLocal : MonoBehaviour
{
    private Teleportation teleportationManagerREF;
    private GameObject playerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        playerPrefab = GameObject.Find("playerPrefab");
        teleportationManagerREF = GameObject.Find("TeleportManager").GetComponent<Teleportation>();
    }

    // Update is called once per frame
    void OnTriggerEnter (Collider col)
    {
        
        if (col.CompareTag("Player") || col.CompareTag("PlayerObject"))
        {
            Debug.Log("Collider!");
            for (int i = 0 ; i < teleportationManagerREF.Locations.Length; i ++ )
            {
                if (i < teleportationManagerREF.Locations.Length - 1)
                {
                    if (gameObject.name == teleportationManagerREF.Locations[i].name)
                    playerPrefab.transform.position = teleportationManagerREF.Locations[i + 1].transform.position;
                }
                
            }
        }
    }
}
