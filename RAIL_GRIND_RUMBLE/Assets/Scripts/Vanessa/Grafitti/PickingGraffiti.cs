using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickingGraffiti : MonoBehaviour
{
    public bool isPickingGraffiti;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void WaitingGraffiti()
    {
        isPickingGraffiti = true;
        Debug.Log("Waiting for input");

    }
}
