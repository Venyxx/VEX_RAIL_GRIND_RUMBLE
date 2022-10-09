using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactRailDetection : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public GameObject objectOne;
    public GameObject objectTwo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, new Vector3(objectOne.transform.position.x,objectOne.transform.position.y,objectOne.transform.position.z));
        lineRenderer.SetPosition(1, new Vector3(objectTwo.transform.position.x,objectTwo.transform.position.y,objectTwo.transform.position.z));

    }
}
