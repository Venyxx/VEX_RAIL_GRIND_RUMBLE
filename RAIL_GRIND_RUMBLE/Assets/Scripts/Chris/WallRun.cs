using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRun : MonoBehaviour
{
    public GameObject playerREF;
    private ThirdPersonController playerScript;
    private float setY;
    
    void Start()
    {
       playerScript = player.gameObject.GetComponent<ThirdPersonController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "WallRun")
        {
            if (playerScript.Grounded == false)
            {
                setY = transform.position.y;
            }
        }
    }

    void OnTriggerStay (Collider collider)
    {
        if (collider.gameObject.tag == "WallRun")
        {
            transform.position = new Vector3(transform.position.x, setY, transform.position.z);
        }
    }
}
