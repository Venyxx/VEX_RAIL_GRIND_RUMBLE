using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HairVendor : MonoBehaviour
{
    private GameObject Vendor;
    public Transform hairPanel;
    public TextMeshProUGUI hairBuySetText;
    public TextMeshProUGUI moneyText;

    public AudioClip clip;
    public AudioClip close;
    AudioSource audioSource;

    private int [] hairCost = new int [] {0,50,55,60};

    private int selectedHairIndex;
    private int activeHairIndex;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        UpdateMoneyText();

        //button on click events to shop 
        InitShop();

        OnHairSelect(SaveManager.Instance.state.activeAriHair);
        SetHair(SaveManager.Instance.state.activeAriHair);

        hairPanel.GetChild(SaveManager.Instance.state.activeAriHair).GetComponent<RectTransform>().localScale = Vector3.one * 1.125f;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

     private void InitShop ()
    {
        //make sure there are refs
        if (hairPanel == null)
        {
            Debug.Log("not assigned");
        }
        int i = 0;
        foreach (Transform t in hairPanel)
            {
                int currentIndex = i;
                Button b = t.GetComponent<Button>();
                b.onClick.AddListener(() => OnHairSelect(currentIndex));

                //set theme if owned
                Image img = t.GetComponent<Image>();
                img.color = SaveManager.Instance.IsAriHairOwned(i) ? Color.white : new Color (0.7f, 0.7f, 0.7f);

                i++;
            }
    }

    private void SetHair (int index)
    {
        //set active
        activeHairIndex = index;
        SaveManager.Instance.state.activeAriHair = index;

        
        //change hair material


        //change buy set text
        hairBuySetText.text = "Current hair!";
         Debug.Log("ran set hair");

        SaveManager.Instance.Save();
    }

    private void UpdateMoneyText ()
    {
        moneyText.text = ("$" + SaveManager.Instance.state.Money.ToString());
    }

    private void OnHairSelect(int currentIndex)
    {
        Debug.Log("select hair button" + currentIndex);
        //if clicked is alr active
        if (selectedHairIndex == currentIndex)
        {
            return;
        }


         //if not make the icon bigger
        hairPanel.GetChild(currentIndex).GetComponent<RectTransform>().localScale = Vector3.one * 1.125f;

        //make the old one normal sized
        hairPanel.GetChild(selectedHairIndex).GetComponent<RectTransform>().localScale = Vector3.one;

        //set selected theme
        selectedHairIndex = currentIndex;

        // change content od buy set button
        if (SaveManager.Instance.IsAriHairOwned(currentIndex))
        {
            //owned hair color
             //is it alr current?
            if (activeHairIndex == currentIndex)
            {
                hairBuySetText.text = "Current Theme!";
            }else 
            {
                hairBuySetText.text = "Select";
            }

        }
        else 
        {
            //not owned
             hairBuySetText.text = "Buy " + hairCost[currentIndex].ToString();
        }
    }

    public void OnHairBuySet ()
    {
        Debug.Log("buy or set hair");
        
        //is it owned
        if (SaveManager.Instance.IsAriHairOwned(selectedHairIndex))
        {
            //set color hair
            
            SetHair(selectedHairIndex);
        }
        else
        {
            //attempt to buy
            if (SaveManager.Instance.BuyAriHair(selectedHairIndex, hairCost[selectedHairIndex]))
            {
                //success
                SetHair(selectedHairIndex);

                //change color button
                hairPanel.GetChild(selectedHairIndex).GetComponent<Image>().color = Color.white;

                //update visual money text
                UpdateMoneyText();

            }else
            {
                Debug.Log("youre broke");
            }

        }
    }

}
