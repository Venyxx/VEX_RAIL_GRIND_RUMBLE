using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTeleportPls : MonoBehaviour
{
    [SerializeField] private Transform teleportTarget;
    [SerializeField] private GameObject thePlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider Player)
    {
        thePlayer.transform.position = teleportTarget.position;
    }
}
