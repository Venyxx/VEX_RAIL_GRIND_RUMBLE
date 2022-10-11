using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveable : MonoBehaviour
{
    [SerializeField]
    private float speedMetersPerSecond;

    [SerializeField]
    private ThirdPersonMovement target;

    private Vector3? destination;

    private Vector3 startPosition;

    private float totalLerpDuration;

    private float elapsedLerpDuration;

    public bool canGrind;

    private Action onCompleteCallback;
    public Rigidbody rigidBody;

    private void Start()
    {
        canGrind = false;
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        speedMetersPerSecond = target.moveSpeed;
        //Debug.Log("got trigger");

        if (collider.tag == "Line")
        {
            canGrind = true;
            Debug.Log("got trigger tag");
        }
    }

    private void Update()
    {
        //Debug.Log(canGrind);
        if (Input.GetKey(KeyCode.Space))
        {
            canGrind = false;
        }

        if (canGrind == true)
        {
            
            if (destination.HasValue == false)
            {
                return;
            }

            if (elapsedLerpDuration >= totalLerpDuration && totalLerpDuration > 0)
            {
                return;
            }

            elapsedLerpDuration += Time.deltaTime;
            float percent = (elapsedLerpDuration / totalLerpDuration);

            transform.position =
                Vector3.Lerp(startPosition, destination.Value, percent);

            if (elapsedLerpDuration >= totalLerpDuration)
            {
                onCompleteCallback?.Invoke();
            }
        }
    }

    public void MoveTo(Vector3 methodDestination, Action onComplete = null)
    {
        var distanceToNextWayPoint =
            Vector3.Distance(transform.position, methodDestination);
        totalLerpDuration = distanceToNextWayPoint / speedMetersPerSecond;

        startPosition = transform.position;
        destination = methodDestination;

        elapsedLerpDuration = 0f;
        onCompleteCallback = onComplete;
    }

}
