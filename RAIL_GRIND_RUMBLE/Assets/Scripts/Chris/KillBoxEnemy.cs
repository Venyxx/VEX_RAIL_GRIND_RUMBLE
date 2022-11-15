using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBoxEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter (Collider collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy") || collision.gameObject.tag == "Section4Enemy")
        {
            Debug.Log("Kill enemy");
            Destroy(collision.gameObject);
        }
    }
}
