using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;
using TMPro;

public class VendorInteraction : MonoBehaviour
{
    
    private GameObject Vendor;


    public Transform materialPanel;
    public Transform graffitiPanel;
    public Transform hairPanel;
    public Transform questPanel;
    public RectTransform menuContainer;

    public TextMeshProUGUI materialBuySetText;
    public TextMeshProUGUI graffitiBuySetText;
    public TextMeshProUGUI hairBuySetText;

    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI highscoreText;

    public AudioClip clip;
    public AudioClip close;
    AudioSource audioSource;

    private int [] materialCost = new int [] {0,15,20,25,30,50,55,60,65};
    private int [] graffitiCost = new int [] {0,50,55,60};
    private int [] hairCost = new int [] {0,50,55,60};

    private int selectedMaterialIndex;
    private int selectedGraffitiIndex;
    private int selectedHairIndex;

    private int activeMaterialIndex;

    private int activeGraffitiIndex;
    private int activeGraffitiIndex1;
    private int activeGraffitiIndex2;
    private int activeGraffitiIndex3;
    private int activeGraffitiIndex4;
    private int activeHairIndex;



    private Vector3 desiredMenuPosition;
    public GameObject helpScreen;

    private void Start()
    {
 
        Time.timeScale = 1f;
        
        
        //temp starting money
        

        //pos camera on focus menu 
        SetCameraTo(Manager.Instance.menuFocus);

        ///current money
        UpdateMoneyText();

        //button on click events to shop 
        InitShop();

        //Add buttons on click to levels
        InitLevel();

        //player pref
        OnAriMaterialSelect(SaveManager.Instance.state.activeAriMaterial);
        SetAriMaterial(SaveManager.Instance.state.activeAriMaterial);

        //just one for now
        OnGraffitiSelect(SaveManager.Instance.state.ariGraffitiSlotUp1);
        SetGraffiti1(SaveManager.Instance.state.ariGraffitiSlotUp1);

        OnHairSelect(SaveManager.Instance.state.activeAriHair);
        SetHair(SaveManager.Instance.state.activeAriHair);

        //visual selected item
        materialPanel.GetChild(SaveManager.Instance.state.activeAriMaterial).GetComponent<RectTransform>().localScale = Vector3.one * 1.125f;
        graffitiPanel.GetChild(SaveManager.Instance.state.ariGraffitiSlotUp1).GetComponent<RectTransform>().localScale = Vector3.one * 1.125f;
        hairPanel.GetChild(SaveManager.Instance.state.activeAriHair).GetComponent<RectTransform>().localScale = Vector3.one * 1.125f;
    } 

    private void Update ()
    {

        /*if (skyCamera.gameObject.activeInHierarchy == true)
        {
            menuContainer.anchoredPosition3D = Vector3.Lerp(menuContainer.anchoredPosition3D, desiredMenuPosition, 0.05f);
        } else 
        {
            menuContainer.anchoredPosition3D = Vector3.Lerp(menuContainer.anchoredPosition3D, desiredMenuPosition, 0.1f);
        }*/
        
    }

    private void InitShop ()
    {
        //make sure there are refs
        if (materialPanel == null || graffitiPanel == null || hairPanel == null)
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
        i = 0;

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

    private void InitLevel()
    {
        //make sure there are refs
        if (questPanel == null)
        {
            Debug.Log("not assigned");
        }

        // for each child button find buton and add 
        int i = 0;
        foreach (Transform t in questPanel)
        {
            int currentIndex = i;
            Button b = t.GetComponent<Button>();
            //b.onClick.AddListener(() => OnLevelSelect(currentIndex));

            Image img = t.GetComponent<Image>();
            if (i <= SaveManager.Instance.state.completedLevel)
            {
                //its unlocked fr
                if (i == SaveManager.Instance.state.completedLevel)
                {
                    img.color = Color.white;
                    //in progress
                }
                else 
                {
                    //level is completed
                    img.color = Color.green;
                }
            } else
            {
                //not completed or unlocked
                b.interactable = false;
                img.color = Color.grey;
            }

            //is it unlocked

            i++;
        }
    }

    private void SetCameraTo (int menuIndex)
    {
        NavigateTo(menuIndex);
        menuContainer.anchoredPosition3D = desiredMenuPosition;
    }

    private void NavigateTo (int menuIndex)
    {
        switch  (menuIndex)
        {
            default:
            case 0:
                desiredMenuPosition = Vector3.zero;
                break;
            case 1:
                desiredMenuPosition = Vector3.right * 1280;
                break;    
            case 2:
                desiredMenuPosition = Vector3.left * 1280;
                break;
            case 3: 
                desiredMenuPosition = Vector3.down * 720;
                break;

                //1 is material, 2 shop, 3 credits , 4 back? idk how 
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



    //buttons----------------------------------------------------------------------------------------
    public void OnPlayClick ()
    {
        NavigateTo(1);
        
    }
    public void OnShopClick ()
    {
        NavigateTo(2);
        
    }
    public void OnBackClick()
    {
       
        NavigateTo(4);
    }
    public void OnCreditsClick ()
    {
        
        NavigateTo(3);
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
                graffitiBuySetText.text = "Current Theme!";
            }else 
            {
                graffitiBuySetText.text = "Select";
            }

        }
        else 
        {
            //not owned
             graffitiBuySetText.text = "Buy " + graffitiCost[currentIndex].ToString();
        }
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

    private void OnLevelSelect(int currentIndex)
    {
        Manager.Instance.currentLevel = currentIndex;
        SceneManager.LoadScene("SampleScene");
        Debug.Log("select level button: " + currentIndex);
    }

    

    public void OnMaterialBuySet ()
    {
        Debug.Log("buy or set paper");
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
    public void OnGraffitiBuySet ()
    {
        Debug.Log("buy or set theme");
        
        //is it owned
        if (SaveManager.Instance.IsGraffitiOwned(selectedGraffitiIndex))
        {
            //set color theme
            //THIS IS WHERE I THINK A POP UP WOULD SHOW AND ASK WHERE TO DOCK IT
            SetGraffiti1(selectedGraffitiIndex);
        }
        else
        {
            //attempt to buy
            if (SaveManager.Instance.BuyGraffiti(selectedGraffitiIndex, graffitiCost[selectedGraffitiIndex]))
            {
                //success
                SetGraffiti1(selectedGraffitiIndex);

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




    public void HelpMenuOpen ()
    {
        helpScreen.SetActive(true);
        audioSource.PlayOneShot(clip, 0.7F);
    }
     public void HelpMenuClose ()
    {
        helpScreen.SetActive(false);
        audioSource.PlayOneShot(close, 0.7F);
    }

    public void ResetThatSave()
    {
        SaveManager.Instance.ResetSave();
        Application.Quit();
        Debug.Log("im trying to wipe");
    }


    public void SetHighScoreText ()
    {
        highscoreText.text = "$" + SaveManager.Instance.state.endlessHighScore;
    }

    /*public void MaxOutMoney ()
    {
        SaveManager.Instance.state.Money = 999;
        Debug.Log("maxmoney");
        UpdateMoneyText();
    }*/
}
