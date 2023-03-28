using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpanishMode : MonoBehaviour
{

    public static bool spanishMode = false;
    [SerializeField] private Toggle SpanishTog = null;

    private GameObject[] UITexts;
    public List<GameObject> englishUI = new List<GameObject>();
    public List<GameObject> spanishUI = new List<GameObject>();

    void Start()
    {
        UITexts = FindObjectsOfType<GameObject>(true);
        for (int i = 0; i < UITexts.Length; i++)
        {
            if (UITexts[i].tag == "EnglishUI")
            {
                englishUI.Add(UITexts[i]);
            }
            if (UITexts[i].tag == "SpanishUI")
            {
                spanishUI.Add(UITexts[i]);
            }
        }
        
    
    }

    // Update is called once per frame
    void Update()
    {
        if (spanishMode == true)
        {
            SpanishTog.GetComponent<Toggle>().isOn = true;
            ActivateSpanishMode();
        }
        else
        {
            SpanishTog.GetComponent<Toggle>().isOn = false;
            DeactivateSpanishMode();
        }
        
    }

    public void SpanishToggle()
    {
        if (SpanishTog.GetComponent<Toggle>().isOn == false)
        {
            spanishMode = false;
            DeactivateSpanishMode();
            Debug.Log("It's not spanish time");
            
        }
        else
        {
            spanishMode = true;
            ActivateSpanishMode();
            Debug.Log("Es hora de Espanol");
        }
    }

    public void ActivateSpanishMode()
    {
        for (int i = 0; i < englishUI.Count; i++)
        {
            englishUI[i].SetActive(false);
        }

        for (int i = 0; i < spanishUI.Count; i++)
        {
            spanishUI[i].SetActive(true);

        }
    }

    public void DeactivateSpanishMode()
    {
        for (int i = 0; i < englishUI.Count; i++)
        {
            englishUI[i].SetActive(true);
        }

        for (int i = 0; i < spanishUI.Count; i++)
        {
            spanishUI[i].SetActive(false);

        }
    }
}
