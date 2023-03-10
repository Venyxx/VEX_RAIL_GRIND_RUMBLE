using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 
using UnityEngine.InputSystem;

public class GraffitiVendor : MonoBehaviour
{
    private GameObject Vendor;

    public Transform graffitiPanel;
    public TextMeshProUGUI graffitiBuySetText;
    public TextMeshProUGUI moneyText;

    private ThirdPersonMovement playerREF;
    private InputHandler playerActions;


    public AudioClip clip;
    public AudioClip close;
    AudioSource audioSource;

    private int [] graffitiCost = new int [] {0,50,55,60};
    private int selectedGraffitiIndex;

    private int activeGraffitiIndex;
    private int activeGraffitiIndex1;
    private int activeGraffitiIndex2;
    private int activeGraffitiIndex3;
    private int activeGraffitiIndex4;

    public GameObject Slotting;
    private bool activeSelection;

    // Start is called before the first frame update
    void Start()
    {
        Slotting.SetActive(false);
        Time.timeScale = 1f;
        UpdateMoneyText();

        playerREF = GameObject.Find("playerPrefab").GetComponent<ThirdPersonMovement>();
        playerActions = playerREF.playerActions;

        //button on click events to shop 
        InitShop();

        
        OnGraffitiSelect(SaveManager.Instance.state.ariGraffitiSlotUp1);
        SetGraffiti1(SaveManager.Instance.state.ariGraffitiSlotUp1);
        
        OnGraffitiSelect(SaveManager.Instance.state.ariGraffitiSlotRight2);
        SetGraffiti2(SaveManager.Instance.state.ariGraffitiSlotRight2);

        OnGraffitiSelect(SaveManager.Instance.state.ariGraffitiSlotDown3);
        SetGraffiti3(SaveManager.Instance.state.ariGraffitiSlotDown3);

        OnGraffitiSelect(SaveManager.Instance.state.ariGraffitiSlotLeft4);
        SetGraffiti4(SaveManager.Instance.state.ariGraffitiSlotLeft4);

        
        graffitiPanel.GetChild(SaveManager.Instance.state.ariGraffitiSlotUp1).GetComponent<RectTransform>().localScale = Vector3.one * 1.125f;
        graffitiPanel.GetChild(SaveManager.Instance.state.ariGraffitiSlotRight2).GetComponent<RectTransform>().localScale = Vector3.one * 1.125f;
        graffitiPanel.GetChild(SaveManager.Instance.state.ariGraffitiSlotDown3).GetComponent<RectTransform>().localScale = Vector3.one * 1.125f;
        graffitiPanel.GetChild(SaveManager.Instance.state.ariGraffitiSlotLeft4).GetComponent<RectTransform>().localScale = Vector3.one * 1.125f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     private void InitShop ()
    {
        //make sure there are refs
        if (graffitiPanel == null)
        {
            Debug.Log("not assigned");
        }

        // for each child button find buton and add 
        int i = 0;
        foreach (Transform t in graffitiPanel)
        {
            int currentIndex = i;
            Button b = t.GetComponent<Button>();
            b.onClick.AddListener(() => OnGraffitiSelect(currentIndex));

            //set theme if owned
            Image img = t.GetComponent<Image>();
            img.color = SaveManager.Instance.IsGraffitiOwned(i) ? Color.white : new Color (0.7f, 0.7f, 0.7f);

            i++;
        }
    }

    private void SetGraffiti1 (int index)
    {
        //set active
        activeGraffitiIndex1 = index;
        SaveManager.Instance.state.ariGraffitiSlotUp1 = index;
        

        //change buy set text
        //themeBuySetText.text = "Current Theme!";

         SaveManager.Instance.Save();
    }
    private void SetGraffiti2 (int index)
    {
        //set active
        activeGraffitiIndex2 = index;
        SaveManager.Instance.state.ariGraffitiSlotRight2 = index;
        

        //change buy set text
        //themeBuySetText.text = "Current Theme!";

         SaveManager.Instance.Save();
    }
    private void SetGraffiti3 (int index)
    {
        //set active
        activeGraffitiIndex3 = index;
        SaveManager.Instance.state.ariGraffitiSlotDown3 = index;
        

        //change buy set text
        //themeBuySetText.text = "Current Theme!";

         SaveManager.Instance.Save();
    }
    private void SetGraffiti4 (int index)
    {
        //set active
        activeGraffitiIndex1 = index;
        SaveManager.Instance.state.ariGraffitiSlotLeft4 = index;
        

        //change buy set text
        //themeBuySetText.text = "Current Theme!";

         SaveManager.Instance.Save();
    }

    private void UpdateMoneyText ()
    {
        moneyText.text = ("$" + SaveManager.Instance.state.Money.ToString());
    }

    private void OnGraffitiSelect(int currentIndex)
    {
        Debug.Log("select graffiti button" + currentIndex);
        //if clicked is alr active
        if (selectedGraffitiIndex == currentIndex)
        {
            return;
        }


         //if not make the icon bigger
        graffitiPanel.GetChild(currentIndex).GetComponent<RectTransform>().localScale = Vector3.one * 1.125f;

        //make the old one normal sized
        if (currentIndex != activeGraffitiIndex1 ||currentIndex !=  activeGraffitiIndex2 ||currentIndex !=  activeGraffitiIndex3 || currentIndex != activeGraffitiIndex4)
            graffitiPanel.GetChild(selectedGraffitiIndex).GetComponent<RectTransform>().localScale = Vector3.one;

        //set selected theme
        selectedGraffitiIndex = currentIndex;

        // change content od buy set button
        if (SaveManager.Instance.IsGraffitiOwned(currentIndex))
        {
            //owned wallpaper color
             //is it alr current?
            if (activeGraffitiIndex == currentIndex)
            {
                graffitiBuySetText.text = "Equipped";
            }else 
            {
                graffitiBuySetText.text = "Select";
                activeSelection = true;
                //pop up 
            }

        }
        else 
        {
            //not owned
             graffitiBuySetText.text = "Buy " + graffitiCost[currentIndex].ToString();
        }
    }


    public void SlotUp1(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        if (!activeSelection) return;
            Debug.Log("setting the selected graffiti to slot 1");
            SetGraffiti1(selectedGraffitiIndex);
        activeSelection = false;
    }
    public void SlotRight2(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        if (!activeSelection) return;
            Debug.Log("setting the selected graffiti to slot 2");
            SetGraffiti2(selectedGraffitiIndex);
        activeSelection = false;
    }
    public void SlotDown3(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        if (!activeSelection) return;
            Debug.Log("setting the selected graffiti to slot 3");
            SetGraffiti3(selectedGraffitiIndex);
        activeSelection = false;
    }
    public void SlotLeft4(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        if (!activeSelection) return;
            Debug.Log("setting the selected graffiti to slot 4");
            SetGraffiti4(selectedGraffitiIndex);
        activeSelection = false;
    }


    public void OnGraffitiBuySet ()
    {
        Debug.Log("buy or set theme");
        
        //is it owned
        if (SaveManager.Instance.IsGraffitiOwned(selectedGraffitiIndex))
        {
            //waiting for input, display proc
            activeSelection = true;
            Slotting.SetActive(true);
        }
        else
        {
            //attempt to buy
            if (SaveManager.Instance.BuyGraffiti(selectedGraffitiIndex, graffitiCost[selectedGraffitiIndex]))
            {
                //success
                activeSelection = true;
                Slotting.SetActive(true);

                //wait for slot context inputs and then itll slot it right

                //change color button
                graffitiPanel.GetChild(selectedGraffitiIndex).GetComponent<Image>().color = Color.white;

                //update visual money text
                UpdateMoneyText();

            }else
            {
                Debug.Log("youre broke");
            }

        }
    }

   
}
