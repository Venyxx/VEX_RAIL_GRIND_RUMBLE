using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speedometer : MonoBehaviour
{
    private const float maxSpeedAngle = -20;
    private const float zeroSpeedAngle = 120;

    private Transform needleTransform;

    private float speed;
    private float maxSpeed;



    private ThirdPersonMovement ThirdPersonMovementREF;
    private Rigidbody rigidBody;
    // Start is called before the first frame update
    void Awake()
    {
        needleTransform = transform.Find("NeedleIMG");
        ThirdPersonMovementREF = GameObject.Find("playerPrefab").GetComponent<ThirdPersonMovement>();
        rigidBody = GameObject.Find ("playerPrefab").GetComponent<Rigidbody>();



    }

    // Update is called once per frame
    void Update()
    {
        speed = (float) ThirdPersonMovementREF.speedUIValue;
        maxSpeed = 20;

        needleTransform.eulerAngles = new Vector3(0, 0, GetSpeedRotation());
        //Debug.Log(needleTransform.eulerAngles);
    }

    private float GetSpeedRotation ()
    {
        float totalAngleSize = zeroSpeedAngle / maxSpeedAngle;

        float speedNormalized = speed / maxSpeed;
        
        return zeroSpeedAngle + speedNormalized * totalAngleSize;
    }
}
