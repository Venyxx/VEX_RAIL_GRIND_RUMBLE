using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColDestroy : MonoBehaviour
{
    private void OnTriggerEnter (Collider col)
    {
        if (col.tag == "PlayerObject")
        {
            Destroy(gameObject.transform.parent);
        }
    }
}
