using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewspaperPickup : MonoBehaviour
{
    float speed = 100;
    [SerializeField] int act;
    [SerializeField] int entry;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, speed * Time.deltaTime, 0f, Space.Self);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<ThirdPersonMovement>().PlaySound(0);
            AddToDataLog();
        }
    }

    void AddToDataLog()
    {
        Destroy(gameObject);
    }
}
