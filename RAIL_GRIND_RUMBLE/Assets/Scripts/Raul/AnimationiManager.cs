using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationiManager : MonoBehaviour
{

    public bool misControlEnabled = true; //stop overlapping actions - Raul
    public bool heavyResetSpeed = false;
    public ThirdPersonMovement script;
    [SerializeField]
    private Rigidbody ariRigidbody;
    [SerializeField]
    private int slamSpeed; //RAUL

    void Update()
    {
        
    }

    public void EnableControl()
    {
        misControlEnabled = true;
        
    }

    public void DisableControl()
    {
        misControlEnabled = false;
    }

    public void ResetSpeed()
    {
        heavyResetSpeed = true;
        Debug.Log("resetspeed");
        script.currentSpeed = 0;
        
    }

    private void heavyAerialImpulse()
    {
        ariRigidbody.AddForce(0, -slamSpeed, 0, ForceMode.Acceleration); //raul
    }


    
}
