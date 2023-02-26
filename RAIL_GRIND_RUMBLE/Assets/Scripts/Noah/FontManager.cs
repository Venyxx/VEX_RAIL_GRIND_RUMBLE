using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FontManager : MonoBehaviour
{


    public List<TMP_Text> UrbanHeroes = new List<TMP_Text>();
    public List<TMP_Text> Rebound = new List<TMP_Text>();
    public List<TMP_Text> JackArmstrong = new List<TMP_Text>();
    public List<TMP_Text> Liberation = new List<TMP_Text>();
    public TMP_Text[] Texts;
    public TMP_FontAsset urbanHeroesFont;
    public TMP_FontAsset reboundFont;
    public TMP_FontAsset jackArmstrongFont;
    public TMP_FontAsset liberationFont;
    public TMP_FontAsset openDyslexicFont;
    [SerializeField] private Toggle DyslexiaTog = null;




    void Start()
    {
        Texts = FindObjectsOfType<TMP_Text>(true);

        for(int i = 0; i < Texts.Length; i++)
        {
            if (Texts[i].font == urbanHeroesFont)
            {
                UrbanHeroes.Add(Texts[i]);
            }

            if (Texts[i].font == reboundFont)
            {
                Rebound.Add(Texts[i]);
            }

            if (Texts[i].font == jackArmstrongFont)
            {
                JackArmstrong.Add(Texts[i]);
            }

            if (Texts[i].font == liberationFont)
            {
                Liberation.Add(Texts[i]);
            }
        }
       
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if (DyslexiaTog.isOn == true)
        {
            ActivateDyslexiaFriendlyFont();
        }
        if (DyslexiaTog.isOn == false)
        {
            DeactivateDyslexiaFriendlyFont();
        }
        */
    }

    public void DylexiaToggle()
    {
        if (DyslexiaTog.GetComponent<Toggle>().isOn == false)
        {
            DeactivateDyslexiaFriendlyFont();
        }
        else
        {
            ActivateDyslexiaFriendlyFont();
        }
    }

    public void ActivateDyslexiaFriendlyFont()
    {
        for (int i = 0; i < UrbanHeroes.Count; i++)
        {
            UrbanHeroes[i].font = openDyslexicFont;
            // UrbanHeroes[i].autoSize.isOn = true;
        }

        for (int i = 0; i < Rebound.Count; i++)
        {
            Rebound[i].font = openDyslexicFont;
        }

        for (int i = 0; i < JackArmstrong.Count; i++)
        {
            JackArmstrong[i].font = openDyslexicFont;
        }

        for (int i = 0; i < Liberation.Count; i++)
        {
            Liberation[i].font = openDyslexicFont;
        }
    }

    public void DeactivateDyslexiaFriendlyFont()
    {
        for (int i = 0; i < UrbanHeroes.Count; i++)
        {
            UrbanHeroes[i].font = urbanHeroesFont;
        }

        for (int i = 0; i < Rebound.Count; i++)
        {
            Rebound[i].font = reboundFont;
        }

        for (int i = 0; i < JackArmstrong.Count; i++)
        {
            JackArmstrong[i].font = jackArmstrongFont;
        }

        for (int i = 0; i < Liberation.Count; i++)
        {
            Liberation[i].font = liberationFont;
        }
    }
}
