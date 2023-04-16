using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class AnimationiManager : MonoBehaviour
{

    public bool misControlEnabled = true; //stop overlapping actions - Raul
    public bool heavyResetSpeed = false;
    public ThirdPersonMovement script;
     [SerializeField]
    private Rigidbody ariRigidbody;
    [SerializeField]
    private int slamSpeed; //RAUL
    
    public float waitForShockwave = 12f;

     [SerializeField]
    private GameObject shockwaveEffect;

    public UnityEvent shake;
    

   

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
        //Debug.Log("resetspeed");
        script.currentSpeed = 0;
        
    }

    private void heavyAerialImpulse()
    {
        ariRigidbody.AddForce(0, -slamSpeed, 0, ForceMode.Acceleration); //raul
    }

    private void spawnShockwavePls()
    {
        StartCoroutine(shockwaveBuffer());
        shake.Invoke();
    }

     IEnumerator shockwaveBuffer()
    {
        
        shockwaveEffect.SetActive(true);
        Debug.Log("spawnshockwave");
        yield return new WaitForSeconds(1);
        shockwaveEffect.SetActive(false);
        Debug.Log("Despawnshockwave");
    }


    
}
