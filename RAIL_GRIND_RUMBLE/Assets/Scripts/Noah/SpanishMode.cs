using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpanishMode : MonoBehaviour
{

    public static bool spanishMode = false;
    [SerializeField] private Toggle SpanishTog = null;

    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public void SpanishToggle()
    {
        if (SpanishTog.GetComponent<Toggle>().isOn == false)
        {
            spanishMode = false;
            Debug.Log("It's not spanish time");
        }
        else
        {
            spanishMode = true;
            Debug.Log("Es hora de Espanol");
        }
    }
}
