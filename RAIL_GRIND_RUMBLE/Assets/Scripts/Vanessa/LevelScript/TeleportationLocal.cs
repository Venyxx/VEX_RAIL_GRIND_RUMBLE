using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportationLocal : MonoBehaviour
{
    private Teleportation teleportationManagerREF;
    private TotalWaypointController totalREF;
    private GameObject playerPrefab;

    private bool added;
    // Start is called before the first frame update
    void Start()
    {
        playerPrefab = GameObject.Find("playerPrefab");
        teleportationManagerREF = FindObjectOfType<Teleportation>();
        totalREF = FindObjectOfType<TotalWaypointController>();
    }

    // Update is called once per frame
    void OnTriggerEnter (Collider col)
    {
        if (gameObject.tag == "WayPoint")
        {
 
            if (col.CompareTag("Player") || col.CompareTag("PlayerObject"))
            {
                //Debug.Log("Collider!");
                
                for (int i = 0 ; i < teleportationManagerREF.Locations.Length; i ++ )
                {
                    if (i < teleportationManagerREF.Locations.Length)
                    {
                        //Debug.Log(teleportationManagerREF.Locations[i + 1]);

                        if (gameObject.name == teleportationManagerREF.Locations[i].name)
                        {
                            StartCoroutine(DialogueManager.DialogueWipe());
                            playerPrefab.transform.position = teleportationManagerREF.Locations[i + 1].transform.position + new Vector3 (0.5f, 0, 0.5f);
                        }

                                
                        
                        if (!added)
                        {
                          totalREF.currentIndex++;  
                          added = true;
                        }
                        
                        
                    }
                    
                }
            }
        }
    }
}
