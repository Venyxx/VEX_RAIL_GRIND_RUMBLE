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

    public static bool isXInverted = false;
    public static bool isYInverted = false;

    public Slider YSens;
    public Slider XSens;

    public TextMeshProUGUI YSensTextUI = null;
    public TextMeshProUGUI XSensTextUI = null;


    void Start()
    {
        if(cineMachine != null)
            _freeLook = cineMachine.GetComponent<CinemachineFreeLook>();
        
        XCheckbox.isOn = isXInverted;
        YCheckbox.isOn = isYInverted;

        LoadValues();
     
    }

    // Update is called once per frame
    void Update()
    {
        if (cineMachine != null)
        {
            if (XCheckbox.isOn == true)
            {
                isXInverted = true;
                _freeLook.m_XAxis.m_InvertInput = true;
            }
            else
            {
                isXInverted = false;
                _freeLook.m_XAxis.m_InvertInput = false;
            }

            if (YCheckbox.isOn == true)
            {
                isYInverted = true;
                _freeLook.m_YAxis.m_InvertInput = false;

            }
            else
            {
                isYInverted = false;
                _freeLook.m_YAxis.m_InvertInput = true;
            } 
            
            
            
            
            
            _freeLook.m_XAxis.m_MaxSpeed = XSens.value * 20 + 70;
            _freeLook.m_YAxis.m_MaxSpeed = YSens.value / 2 + 1;
       }
        

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

        float XSensValue = PlayerPrefs.GetFloat("XSensValue", 5);
        float YSensValue = PlayerPrefs.GetFloat("YSensValue", 5);

        XSens.value = XSensValue;
        YSens.value = YSensValue;
        
        SetSlider(); 
    }



}
