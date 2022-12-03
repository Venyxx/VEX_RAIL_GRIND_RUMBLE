using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using TMPro;

public class CameraOptions : MonoBehaviour
{
    // Start is called before the first frame update
    private CinemachineFreeLook _freeLook;
    [SerializeField] private GameObject cineMachine;

    public Toggle YCheckbox;
    public Toggle XCheckbox;

    public Slider YSens;
    public Slider XSens;

    public TextMeshProUGUI YSensTextUI = null;
    public TextMeshProUGUI XSensTextUI = null;


    void Start()
    {
        _freeLook = cineMachine.GetComponent<CinemachineFreeLook>();

        LoadValues();
     
    }

    // Update is called once per frame
    void Update()
    {
        _freeLook.m_XAxis.m_InvertInput = XCheckbox.isOn;
        _freeLook.m_YAxis.m_InvertInput = !YCheckbox.isOn;

        _freeLook.m_XAxis.m_MaxSpeed = XSens.value * 75 + 25;
        _freeLook.m_YAxis.m_MaxSpeed = YSens.value + 1;

    }


    public void SetSlider()
    {
        XSensTextUI.text = XSens.value.ToString();
        YSensTextUI.text = YSens.value.ToString();
    }
    
    
    
    
    public void SaveOptionsButton()
    {
        float XSensValue = XSens.value;
        float YSensValue = YSens.value;

        PlayerPrefs.SetFloat("XSensValue", XSensValue);
        PlayerPrefs.SetFloat("YSensValue", YSensValue);

        LoadValues();
    }

    public void LoadValues()
    {

        float XSensValue = PlayerPrefs.GetFloat("XSensValue");
        float YSensValue = PlayerPrefs.GetFloat("YSensValue");

        XSens.value = XSensValue;
        YSens.value = YSensValue;
    }



}
