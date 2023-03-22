using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StatsVisual : MonoBehaviour
{
    public Image _bar;
    public RectTransform button;
    private PlayerHealth playerhealth;
    public float _healthValue = 0;
    // Start is called before the first frame update
    void Start()
    {
       playerhealth = GameObject.Find("playerPrefab").GetComponent<PlayerHealth>();
    
    }

    // Update is called once per frame
    void Update()
    {
        _healthValue = Mathf.Lerp(_healthValue, playerhealth.currentHealth,  .2f);
         HealthChange(_healthValue);
    }

    void HealthChange(float healthValue)
    {
        float amount = (healthValue/100.0f) * .4f + .6f;
        _bar.fillAmount = amount;
        //float buttonAngle = amount * 360;
        //button.localEulerAngles = new Vector3(0, 0, -buttonAngle);
    }
}
