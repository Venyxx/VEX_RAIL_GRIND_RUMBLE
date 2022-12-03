using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class CameraOptions : MonoBehaviour
{
    // Start is called before the first frame update
    private CinemachineFreeLook _freeLook;
    [SerializeField] private GameObject cineMachine;

    public Toggle YCheckbox;
    public Toggle XCheckbox;

    public Slider YSens;
    public Slider XSens;


    void Start()
    {
        _freeLook = cineMachine.GetComponent<CinemachineFreeLook>();
     
    }

    // Update is called once per frame
    void Update()
    {
        _freeLook.m_XAxis.m_InvertInput = XCheckbox.isOn;
        _freeLook.m_YAxis.m_InvertInput = !YCheckbox.isOn;

        _freeLook.m_XAxis.m_MaxSpeed = XSens.value * 75 + 25;
        _freeLook.m_YAxis.m_MaxSpeed = YSens.value + 1;

    }

    public void SetCameraSens(float sliderValue)
    {
        

    }

    
}
