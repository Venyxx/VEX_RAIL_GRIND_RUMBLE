using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarControllerV2 : MonoBehaviour
{
    public Image _bar;
    public RectTransform button;
    public PlayerHealth playerhealth;
    public float _healthValue = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HealthChange(_healthValue);
         _healthValue = playerhealth.currentHealth;
    }
    void HealthChange(float healthValue)
    {
        float amount = (healthValue/20.0f) * 108.0f/360;
        _bar.fillAmount = amount;
        float buttonAngle = amount * 360;
        button.localEulerAngles = new Vector3(0, 0, -buttonAngle);
    }
}
