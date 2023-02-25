using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour
{
    //appplied on player obj
    public bool activeAutoMoveW = false; 
    public bool activeAutoMoveS = false; 

    public int MoveSpeed = 8;  
    public bool activeSelect = false;

    public Transform[] points;
    private int currentIndex = -1;


    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        /*if(activeAutoMoveW == true)
        {
            Vector3 direction = points[i+1]-points[i];
            transform.position += direction * MoveSpeed * Time.fixedDeltaTime;

            if((transform.position - points[i+1]).sqrMagnitude < 0.1f) //check if we are at the next point
            currentIndex++;
        }

        if(activeAutoMoveS == true)
        {
            Vector3 direction = points[i+1]-points[i];
            transform.position -= direction * MoveSpeed * Time.fixedDeltaTime;

            if((transform.position - points[i]).sqrMagnitude < 0.1f) //check if we are at the next point
            currentIndex--;
        }

        if(currentIndex < 0 || currentIndex > points.Length) //check if we are at either edge of the line and then end the slip
        {
            activeAutoMoveW = false;
            activeAutoMoveS = false;
        }*/

    }

    public void BeginSlip(Transform[] points) //called from the object when you want to begin the grind
    {
        this.points = points; //get the waypoints points from the object you grind on

        //find the closest point
        float minDist = float.MaxValue;
        for(int i=0; i<points.Length; i++)
        {
            var pointHere = points[i].position;
            float currentDist = (transform.position - pointHere).sqrMagnitude;
            if(currentDist  < minDist)
            {
                minDist = currentDist;
                currentIndex = i;
            }
        }
    }
}
