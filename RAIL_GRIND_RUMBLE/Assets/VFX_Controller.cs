using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX_Controller : MonoBehaviour
{

    public Camera m_Camera;

    private void Awake()
    {
        m_Camera = GameObject.Find("camerasPrefab").GetComponent<Camera>(); ;
    }
    //Orient the camera after all movement is completed this frame to avoid jittering
    void LateUpdate()
    {
        transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward,
            m_Camera.transform.rotation * Vector3.up);
    }
    public float ShrinkDuration = 5f;

    // The target scale
    public Vector3 TargetScale = Vector3.one * .5f;

    // The starting scale
    Vector3 startScale;

    // T is our interpolant for our linear interpolation.
    float t = 0;

    void OnEnable()
    {
        // initialize stuff in OnEnable
        startScale = Vector3.one;
        t = 0;
    }

    void Update()
    {
        // Divide deltaTime by the duration to stretch out the time it takes for t to go from 0 to 1.
        t += Time.deltaTime / ShrinkDuration;

        // Lerp wants the third parameter to go from 0 to 1 over time. 't' will do that for us.
        Vector3 newScale = Vector3.Lerp(startScale, TargetScale, t);
        transform.localScale = newScale;

        // We're done! We can disable this component to save resources.
        if (t > 1)
        {
            enabled = false;
            
        }
    }
}
