using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    
    private GameObject currentMenuCanvas;

    // Start is called before the first frame update
    private void OnTriggerEnter (Collider col)
    {
        if (col.gameObject.tag == "HairVendor")
        {
            Debug.Log("hair prompt");

        } else if (col.gameObject.tag == "GraffitiVendor")
        {
            Debug.Log("graffiti prompt");
        }
    }
}
