using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class OnTriggerEnterScroll : MonoBehaviour
{
    // This script is applied on the the rails themselves
    public Transform target; // Player
    AutoMove AutoMoveScript;
    public GameObject pathHolder;

    public List<Transform> waypoints;
    Transform[] arrayOfWaypoints;

    void Start()
    {
        AutoMoveScript =  target.GetComponent<AutoMove>();
        waypoints = pathHolder.GetComponentsInChildren<Transform>().ToList();
        waypoints.RemoveAt(index: 0);
        arrayOfWaypoints = waypoints.ToArray();

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetDir = target.position - transform.position;
        Debug.Log(targetDir);
    }

    void OnTriggerEnter(Collider other) 
    {
        
        if(other.GetComponent<Collider>().name == "Player")
        {
                AutoMoveScript.BeginSlip(arrayOfWaypoints);
                AutoMoveScript.MoveSpeed = 8;  
                AutoMoveScript.activeAutoMoveW = true; 
                AutoMoveScript.activeAutoMoveS = false; 
        }
    }

    void OnTriggerStay(Collider other) 
    { 
        if(other.GetComponent<Collider>().name == "Player")
        {
            if(Input.GetKey(KeyCode.W)){

                AutoMoveScript.activeAutoMoveW = true; // Cambio variabile di scorrimento
                AutoMoveScript.activeAutoMoveS = false; // Cambio variabile di scorrimento

            }

            if(Input.GetKey(KeyCode.S)){

                AutoMoveScript.activeAutoMoveS = true; // Cambio variabile di scorrimento
                AutoMoveScript.activeAutoMoveW = false; // Cambio variabile di scorrimento

            }

        }

    }

    void OnTriggerExit(Collider other) 
    { // Quando il player esce nel Trigger

        if(other.GetComponent<Collider>().name == "MainCharacter")
        {

                AutoMoveScript.MoveSpeed = 8; // Cambio variabile di Velocita'
                AutoMoveScript.activeAutoMoveS = false; // Cambio variabile di scorrimento
                AutoMoveScript.activeAutoMoveW = false; // Cambio variabile di scorrimento

        }

    }
}

