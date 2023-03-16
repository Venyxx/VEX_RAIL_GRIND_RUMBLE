using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShaderVendor : MonoBehaviour
{
    private GameObject Vendor;
    public Transform materialPanel;
    public TextMeshProUGUI materialBuySetText;

    public TextMeshProUGUI moneyText;

    public AudioClip clip;
    public AudioClip close;
    private AudioSource audioSource;

    private int [] materialCost = new int [] {0,15,20,25,30,50,55,60,65};
    private int selectedMaterialIndex;
    private int activeMaterialIndex;



    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        UpdateMoneyText();

        //button on click events to shop 
        InitShop();

        //player pref
        OnAriMaterialSelect(SaveManager.Instance.state.activeAriMaterial);
        SetAriMaterial(SaveManager.Instance.state.activeAriMaterial);

        materialPanel.GetChild(SaveManager.Instance.state.activeAriMaterial).GetComponent<RectTransform>().localScale = Vector3.one * 1.125f;
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    private void InitShop ()
    {
        //make sure there are refs
        if (materialPanel == null)
        {
            Debug.Log("not assigned");
        }

        // for each child button find buton and add 
        int i = 0;
        foreach (Transform t in materialPanel)
        {
            int currentIndex = i;
            Button b = t.GetComponent<Button>();
            b.onClick.AddListener(() => OnAriMaterialSelect(currentIndex));

            //set color if owned
            Image img = t.GetComponent<Image>();
            img.color = SaveManager.Instance.IsAriMatOwned(i) ? Color.white : new Color (0.7f, 0.7f, 0.7f);

            i++;
        }
    }

    private void SetAriMaterial (int index)
    {
        //set active
        activeMaterialIndex = index;
        SaveManager.Instance.state.activeAriMaterial = index;

        
        //change room material


        //change buy set text
        materialBuySetText.text = "Current Material!";
         Debug.Log("ran set paper");

        SaveManager.Instance.Save();
    }

    private void UpdateMoneyText ()
    {
        moneyText.text = ("$" + SaveManager.Instance.state.Money.ToString());
    }

    private void OnAriMaterialSelect(int currentIndex)
    {
        //if clicked is alr active
        if (selectedMaterialIndex == currentIndex)
        {
            Debug.Log("it was the current one");
            return;  
        }

        //if not make the icon bigger
        materialPanel.GetChild(currentIndex).GetComponent<RectTransform>().localScale = Vector3.one * 1.125f;

        //make the old one normal sized
        materialPanel.GetChild(selectedMaterialIndex).GetComponent<RectTransform>().localScale = Vector3.one;

        Debug.Log("select ari mat button" + currentIndex);
        
        //set selected paper
        selectedMaterialIndex = currentIndex;

        // change content od buy set button
        if (SaveManager.Instance.IsAriMatOwned(currentIndex))
        {
            //owned colorway color
            //is it alr current?
            if (activeMaterialIndex == currentIndex)
            {
                materialBuySetText.text = "Current AriSkin!";
            }else 
            {
                materialBuySetText.text = "Select";
            }  
        }
        else 
        {
            //not owned
             materialBuySetText.text = "Buy " + materialCost[currentIndex].ToString();
        }

    }

    public void OnMaterialBuySet ()
    {
        Debug.Log("buy or set ari shader");
        //is it owned
        if (SaveManager.Instance.IsAriMatOwned(selectedMaterialIndex))
        {
            //set color
            SetAriMaterial(selectedMaterialIndex);
        }
        else
        {
            //attempt to buy
            if (SaveManager.Instance.BuyAriMaterial(selectedMaterialIndex, materialCost[selectedMaterialIndex]))
            {
                //success
                SetAriMaterial(selectedMaterialIndex);
                //change color button
                materialPanel.GetChild(selectedMaterialIndex).GetComponent<Image>().color = Color.white;

                //update visual money text
                UpdateMoneyText();
            }else
            {
                Debug.Log("youre broke");
                
            }

        }
    }

    

}
