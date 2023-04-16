using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class shakeUnityEvent : MonoBehaviour
{
    public UnityEvent shake;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ShockwaveEvent", 3f, 4f);
    }

    private void ShakeEvent()
    {
        shake.Invoke();
    }
}
