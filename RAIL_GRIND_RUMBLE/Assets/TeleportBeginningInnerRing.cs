using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportBeginningInnerRing : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("PlayerObject"))
        {
            other.gameObject.transform.position = LoadNewScene.innerRingDefaultSpawnVector;
        }
    }
}
