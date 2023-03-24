using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrigger : MonoBehaviour
{
    [SerializeField] GameObject[] movingStructures;
    [SerializeField] GameObject[] otherTriggers;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Donovan")
        {
            Debug.Log("MOVE THING TIME");
            ActivateStructures();
        }
    }

    void ActivateStructures()
    {
        for (int i = 0; i < movingStructures.Length; i++)
        {
            movingStructures[i].GetComponent<MovingStructure>().move = true;
            if (movingStructures[i].GetComponent<MovingStructure>().deactivate == true)
            {
                movingStructures[i].SetActive(true);
            }
        }

        for (int i = 0; i < otherTriggers.Length; i++)
        {
            otherTriggers[i].GetComponent<MoveTrigger>().DeactivateStructures();
        }
    }

    void DeactivateStructures()
    {
        for (int i = 0; i < movingStructures.Length; i++)
        {
            movingStructures[i].GetComponent<MovingStructure>().move = false;
        }
    }
}
