using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOptions : MonoBehaviour
{
    // Start is called before the first frame update
    private CinemachineFreeLook _freeLook;
    [SerializeField] private GameObject cineMachine;

    public bool InverseX;
    public bool InverseY;
    
    void Start()
    {
        _freeLook = cineMachine.GetComponent<CinemachineFreeLook>();
        _freeLook.m_XAxis.m_InvertInput = InverseX;
        _freeLook.m_YAxis.m_InvertInput = InverseY;
    }

    // Update is called once per frame
    void Update()
    {

    }
}