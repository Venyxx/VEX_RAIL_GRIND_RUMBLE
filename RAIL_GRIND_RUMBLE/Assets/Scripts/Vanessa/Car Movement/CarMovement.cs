using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class CarMovement : MonoBehaviour
{
    private float currentMoveSpeed;
    private float baseMoveSpeed;

    private bool canMove;
    public PathCreator pathCreator;
    public EndOfPathInstruction endOfPathInstruction;
    [SerializeField] public float distanceTravelled;
    // Start is called before the first frame update
    void Start()
    {

        baseMoveSpeed = 5;
        pathCreator = gameObject.transform.root.transform.Find("Road Creator").gameObject.GetComponent<PathCreator>();
        currentMoveSpeed = baseMoveSpeed;

    }

    // Update is called once per frame
    void Update()
    {
        if (pathCreator != null)
        {
            distanceTravelled += currentMoveSpeed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
        }


    }

    public void StopCar ()
    {
        currentMoveSpeed = 0;
    }

    public void StartCar ()
    {
        currentMoveSpeed = baseMoveSpeed;
    }
}
