using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;
using TMPro;

public class ETCCustomizationVendor : MonoBehaviour
{
    
    private GameObject Vendor;


    public Transform accessoryPanel;
    public Transform topPanel;
    public Transform bottomPanel;
    public Transform sockPanel;
    public Transform skatePanel;
    public Transform hairPanel;
    
    private ThirdPersonMovement thirdPersonMovementREF;

    //public Transform questPanel;
    public RectTransform menuContainer;

    public TextMeshProUGUI accessoryBuySetText;
    public TextMeshProUGUI topBuySetText;
    public TextMeshProUGUI bottomBuySetText;
    public TextMeshProUGUI sockBuySetText;
    public TextMeshProUGUI skateBuySetText;
    public TextMeshProUGUI hairBuySetText;
    public TextMeshProUGUI moneyText;
    //public TextMeshProUGUI highscoreText;

    public AudioClip clip;
    public AudioClip close;
    AudioSource audioSource;

    private int [] accessoryCost = new int [] {0,15,20};
    private int [] topCost = new int [] {0,50,55,60,0,50,55,60,0,50,55};
    private int [] bottomCost = new int [] {0,50,55,60, 60, 60};
    private int [] sockCost = new int [] {10,50};
    private int [] skateCost = new int [] {10,50,55,60,60,60};
    private int [] hairCost = new int [] {0,50,55,60};

    private int selectedAccessoryIndex;
    private int selectedTopIndex;
    private int selectedBottomIndex;
    private int selectedSockIndex;
    private int selectedSkateIndex;
    private int selectedHairIndex;

    private int activeAccessoryIndex;

    private int activeTopIndex;
    private int activeBottomIndex;
    private int activeSockIndex;
    private int activeSkateIndex;
    private int activeHairIndex;

    private Manager ManagerREF;



    private Vector3 desiredMenuPosition;
    //public GameObject helpScreen;

    private void Start()
    {
        Debug.Log("ETC");
        ManagerREF = GameObject.Find("Manager").GetComponent<Manager>();
        Time.timeScale = 1f;
        if (GameObject.Find("playerPrefab"))
            thirdPersonMovementREF = GameObject.Find("playerPrefab").GetComponent<ThirdPersonMovement>();

        //temp starting money
        

        //pos camera on focus menu 
        //SetCameraTo(Manager.Instance.menuFocus);

        ///current money
        UpdateMoneyText();

        //button on click events to shop 
        InitShop();

        
        //player pref
        OnAriAccessorySelect(SaveManager.Instance.state.activeAriAccessory);
        SetAriAccessory(SaveManager.Instance.state.activeAriAccessory);

        OnTopSelect(SaveManager.Instance.state.activeAriTop);
        SetTop(SaveManager.Instance.state.activeAriTop);

        OnBottomSelect(SaveManager.Instance.state.activeAriBottom);
        SetBottom(SaveManager.Instance.state.activeAriBottom);

        OnSockSelect(SaveManager.Instance.state.activeAriSock);
        SetSock(SaveManager.Instance.state.activeAriSock);

        OnSkateSelect(SaveManager.Instance.state.activeAriSkate);
        SetSkate(SaveManager.Instance.state.activeAriSkate);

        OnHairSelect(SaveManager.Instance.state.activeAriHair);
        SetHair(SaveManager.Instance.state.activeAriHair);

        //visual selected item
        accessoryPanel.GetChild(SaveManager.Instance.state.activeAriAccessory).GetComponent<RectTransform>().localScale = Vector3.one * 1.125f;
        topPanel.GetChild(SaveManager.Instance.state.activeAriTop).GetComponent<RectTransform>().localScale = Vector3.one * 1.125f;
        bottomPanel.GetChild(SaveManager.Instance.state.activeAriBottom).GetComponent<RectTransform>().localScale = Vector3.one * 1.125f;
        sockPanel.GetChild(SaveManager.Instance.state.activeAriSock).GetComponent<RectTransform>().localScale = Vector3.one * 1.125f;
        skatePanel.GetChild(SaveManager.Instance.state.activeAriSkate).GetComponent<RectTransform>().localScale = Vector3.one * 1.125f;
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
        if (accessoryPanel == null || topPanel == null || bottomPanel == null || sockPanel == null || skatePanel == null)
        {
            Debug.Log("not assigned");
        }

        // for each child button find buton and add 
        int i = 0;
        foreach (Transform t in accessoryPanel)
        {
            int currentIndex = i;
            Button b = t.GetComponent<Button>();
            b.onClick.AddListener(() => OnAriAccessorySelect(currentIndex));

            //set color if owned
            Image img = t.GetComponent<Image>();
            img.color = SaveManager.Instance.IsAriMatOwned(i) ? Color.white : new Color (0.7f, 0.7f, 0.7f);

            i++;
        }
        i = 0;

        foreach (Transform t in topPanel)
        {
            int currentIndex = i;
            Button b = t.GetComponent<Button>();
            b.onClick.AddListener(() => OnTopSelect(currentIndex));

            //set theme if owned
            Image img = t.GetComponent<Image>();
            img.color = SaveManager.Instance.IsGraffitiOwned(i) ? Color.white : new Color (0.7f, 0.7f, 0.7f);

            i++;
        }
        i = 0;
        foreach (Transform t in bottomPanel)
        {
            int currentIndex = i;
            Button b = t.GetComponent<Button>();
            b.onClick.AddListener(() => OnBottomSelect(currentIndex));

            //set theme if owned
            Image img = t.GetComponent<Image>();
            img.color = SaveManager.Instance.IsAriHairOwned(i) ? Color.white : new Color (0.7f, 0.7f, 0.7f);

            i++;
        }
        i = 0;
        foreach (Transform t in sockPanel)
        {
            int currentIndex = i;
            Button b = t.GetComponent<Button>();
            b.onClick.AddListener(() => OnSockSelect(currentIndex));

            //set theme if owned
            Image img = t.GetComponent<Image>();
            img.color = SaveManager.Instance.IsAriSockOwned(i) ? Color.white : new Color (0.7f, 0.7f, 0.7f);

            i++;
        }
        i = 0;
        foreach (Transform t in skatePanel)
        {
            int currentIndex = i;
            Button b = t.GetComponent<Button>();
            b.onClick.AddListener(() => OnSkateSelect(currentIndex));

            //set theme if owned
            Image img = t.GetComponent<Image>();
            img.color = SaveManager.Instance.IsAriSkateOwned(i) ? Color.white : new Color (0.7f, 0.7f, 0.7f);

            i++;
        }

         i = 0;
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


    private void SetAriAccessory (int index)
    {
        //set active
        activeAccessoryIndex = index;
        SaveManager.Instance.state.activeAriAccessory = index;

        
        //change room material
        //I DONT THINK THEER ARY ANY ACCESSORIES IN THE GAME RN
        //GameObjectAccessorySetting();

        //change buy set text
        accessoryBuySetText.text = "Current Accessory!";
         Debug.Log("ran set accessory");

        SaveManager.Instance.Save();
    }


    private void SetTop (int index)
    {
        //set active
        activeTopIndex = index;
        SaveManager.Instance.state.activeAriTop = index;

        
        //change hair material
        GameObjectTopSetting();

        

        //change buy set text
        topBuySetText.text = "Current Top!";
         Debug.Log("ran set top");

        SaveManager.Instance.Save();
    }

    private void SetBottom (int index)
    {
        //set active
        activeBottomIndex = index;
        SaveManager.Instance.state.activeAriBottom = index;

        
        //change hair material
        GameObjectBottomSetting();

        //change buy set text
        bottomBuySetText.text = "Current Bottom!";
         Debug.Log("ran set bottom");

        SaveManager.Instance.Save();
    }
    
    private void SetSock (int index)
    {
        //set active
        activeSockIndex = index;
        SaveManager.Instance.state.activeAriSock = index;

        
        //change hair material
        GameObjectSockSetting();

        //change buy set text
        sockBuySetText.text = "Current Sock!";
         Debug.Log("ran set sock");

        SaveManager.Instance.Save();
    }

    private void SetSkate (int index)
    {
        //set active
        activeSkateIndex = index;
        SaveManager.Instance.state.activeAriSkate = index;

        
        //change hair material
        GameObjectSkateSetting();

        //change buy set text
        skateBuySetText.text = "Current Skate!";
         Debug.Log("ran set skate");

        thirdPersonMovementREF.RecalculateStats();
        SaveManager.Instance.Save();

        
    }

    private void SetHair (int index)
    {
        //set active
        activeHairIndex = index;
        SaveManager.Instance.state.activeAriHair = index;

        
        //change hair material
        GameObjectHairSetting();

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
    

    private void OnAriAccessorySelect(int currentIndex)
    {
        //if clicked is alr active
        if (selectedAccessoryIndex == currentIndex)
        {
            Debug.Log("it was the current one");
            return;
           
        }

        //if not make the icon bigger
        accessoryPanel.GetChild(currentIndex).GetComponent<RectTransform>().localScale = Vector3.one * 1.125f;

        //make the old one normal sized
        accessoryPanel.GetChild(selectedAccessoryIndex).GetComponent<RectTransform>().localScale = Vector3.one;

        //Debug.Log("select ari acccess button" + currentIndex);
        
        //set selected paper
        selectedAccessoryIndex = currentIndex;

        // change content od buy set button
        if (SaveManager.Instance.IsAriAccessoryOwned(currentIndex))
        {
            //owned colorway color
            //is it alr current?
            if (activeAccessoryIndex == currentIndex)
            {
                accessoryBuySetText.text = "Current Accessory!";
            }else 
            {
                accessoryBuySetText.text = "Select";
            }

            
        }
        else 
        {
            //not owned
             accessoryBuySetText.text = "Buy " + accessoryCost[currentIndex].ToString();
        }

        //accessory object preview
        for (int i = 0; i < ManagerREF.ariAccessoryOptions.Length; i++)
        {
            if (selectedAccessoryIndex != i)
            {
                //turn the object off
                ManagerREF.ariAccessoryOptions[i].SetActive(false);
            } else 
            {
                //turn it on
                ManagerREF.ariAccessoryOptions[i].SetActive(true);
            }
        }
        //GameObjectAccessorySetting();

    }


    private void OnTopSelect(int currentIndex)
    {
        //if clicked is alr active
        if (selectedTopIndex == currentIndex)
        {
            return;
        }


         //if not make the icon bigger
        topPanel.GetChild(currentIndex).GetComponent<RectTransform>().localScale = Vector3.one * 1.125f;

        //make the old one normal sized
        topPanel.GetChild(selectedTopIndex).GetComponent<RectTransform>().localScale = Vector3.one;

        //set selected theme
        selectedTopIndex = currentIndex;

        // change content od buy set button
        if (SaveManager.Instance.IsAriTopOwned(currentIndex))
        {
            //owned wallpaper color
             //is it alr current?
            if (activeTopIndex == currentIndex)
            {
                topBuySetText.text = "Current Top!";
            }else 
            {
                topBuySetText.text = "Select";
            }

        }
        else 
        {
            //not owned
             topBuySetText.text = "Buy " + topCost[currentIndex].ToString();
        }



        //top object preview
        for (int i = 0; i <  ManagerREF.ariTopOptions.Length; i++)
        {
            if (selectedAccessoryIndex != i)
            {
                //turn the object off
                ManagerREF.ariTopOptions[i].SetActive(false);
            } else 
            {
                //turn it on
                ManagerREF.ariTopOptions[i].SetActive(true);
            }
        }
        GameObjectTopSetting();
    }


    private void OnBottomSelect(int currentIndex)
    {
        Debug.Log("select bottom button" + currentIndex);
        //if clicked is alr active
        if (selectedBottomIndex == currentIndex)
        {
            return;
        }


         //if not make the icon bigger
        bottomPanel.GetChild(currentIndex).GetComponent<RectTransform>().localScale = Vector3.one * 1.125f;

        //make the old one normal sized
        bottomPanel.GetChild(selectedBottomIndex).GetComponent<RectTransform>().localScale = Vector3.one;

        //set selected theme
        selectedBottomIndex = currentIndex;

        // change content od buy set button
        if (SaveManager.Instance.IsAriBottomOwned(currentIndex))
        {
            //owned hair color
             //is it alr current?
            if (activeBottomIndex == currentIndex)
            {
                bottomBuySetText.text = "Current Bottom!";
            }else 
            {
                bottomBuySetText.text = "Select";
            }

        }
        else 
        {
            //not owned
             bottomBuySetText.text = "Buy " + bottomCost[currentIndex].ToString();
        }
        GameObjectBottomSetting();
    }

    private void OnSockSelect(int currentIndex)
    {
        Debug.Log("select sock button" + currentIndex);
        //if clicked is alr active
        if (selectedSockIndex == currentIndex)
        {
            return;
        }


         //if not make the icon bigger
        sockPanel.GetChild(currentIndex).GetComponent<RectTransform>().localScale = Vector3.one * 1.125f;

        //make the old one normal sized
        sockPanel.GetChild(selectedSockIndex).GetComponent<RectTransform>().localScale = Vector3.one;

        //set selected theme
        selectedSockIndex = currentIndex;

        // change content od buy set button
        if (SaveManager.Instance.IsAriSockOwned(currentIndex))
        {
            //owned hair color
             //is it alr current?
            if (selectedSockIndex == currentIndex)
            {
                sockBuySetText.text = "Current Sock!";
            }else 
            {
                sockBuySetText.text = "Select";
            }

        }
        else 
        {
            //not owned
             sockBuySetText.text = "Buy " + sockCost[currentIndex].ToString();
        }
        GameObjectSockSetting();
    }

    
    private void OnSkateSelect(int currentIndex)
    {
        Debug.Log("select skate button" + currentIndex);
        //if clicked is alr active
        if (selectedSkateIndex == currentIndex)
        {
            return;
        }


         //if not make the icon bigger
        skatePanel.GetChild(currentIndex).GetComponent<RectTransform>().localScale = Vector3.one * 1.125f;

        //make the old one normal sized
        skatePanel.GetChild(selectedSkateIndex).GetComponent<RectTransform>().localScale = Vector3.one;

        //set selected theme
        selectedSkateIndex = currentIndex;

        // change content od buy set button
        if (SaveManager.Instance.IsAriSkateOwned(currentIndex))
        {
            //owned hair color
             //is it alr current?
            if (selectedSkateIndex == currentIndex)
            {
                skateBuySetText.text = "Current Skate!";
            }else 
            {
                skateBuySetText.text = "Select";
            }

        }
        else 
        {
            //not owned
             skateBuySetText.text = "Buy " + skateCost[currentIndex].ToString();
        }
        GameObjectSkateSetting();
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

        GameObjectHairSetting();
    }



    private void OnLevelSelect(int currentIndex)
    {
        Manager.Instance.currentLevel = currentIndex;
        SceneManager.LoadScene("SampleScene");
        Debug.Log("select level button: " + currentIndex);
    }

    

    public void OnAccessoryBuySet ()
    {
        Debug.Log("buy or set accessory");
        //is it owned
        if (SaveManager.Instance.IsAriAccessoryOwned(selectedAccessoryIndex))
        {
            //set color
            SetAriAccessory(selectedAccessoryIndex);
        }
        else
        {
            //attempt to buy
            if (SaveManager.Instance.BuyAriAccessory(selectedAccessoryIndex, accessoryCost[selectedAccessoryIndex]))
            {
                //success
                SetAriAccessory(selectedAccessoryIndex);
                //change color button
                accessoryPanel.GetChild(selectedAccessoryIndex).GetComponent<Image>().color = Color.white;

                //update visual money text
                UpdateMoneyText();
            }else
            {
                Debug.Log("youre broke");
                
            }

        }

        
    }
    public void OnTopBuySet ()
    {
        Debug.Log("buy or set top");
        
        //is it owned
        if (SaveManager.Instance.IsAriTopOwned(selectedTopIndex))
        {
            //set color
            SetTop(selectedAccessoryIndex);
        }
        else
        {
            //attempt to buy
            if (SaveManager.Instance.BuyAriTop(selectedTopIndex, topCost[selectedTopIndex]))
            {
                //success
                SetTop(selectedTopIndex);

                //change color button
                topPanel.GetChild(selectedTopIndex).GetComponent<Image>().color = Color.white;

                //update visual money text
                UpdateMoneyText();

            }else
            {
                Debug.Log("youre broke");
            }

        }
    }

    public void OnBottomBuySet ()
    {
        Debug.Log("buy or set bottom");
        
        //is it owned
        if (SaveManager.Instance.IsAriBottomOwned(selectedBottomIndex))
        {
            //set color hair
            
            SetBottom(selectedBottomIndex);
        }
        else
        {
            //attempt to buy
            if (SaveManager.Instance.BuyAriBottom(selectedBottomIndex, bottomCost[selectedBottomIndex]))
            {
                //success
                SetBottom(selectedBottomIndex);

                //change color button
                bottomPanel.GetChild(selectedBottomIndex).GetComponent<Image>().color = Color.white;

                //update visual money text
                UpdateMoneyText();

            }else
            {
                Debug.Log("youre broke");
            }

        }
    }

    public void OnSockBuySet ()
    {
        Debug.Log("buy or set sock");
        
        //is it owned
        if (SaveManager.Instance.IsAriSockOwned(selectedSockIndex))
        {
            //set color hair
            
            SetSock(selectedSockIndex);
        }
        else
        {
            //attempt to buy
            if (SaveManager.Instance.BuyAriSock(selectedSockIndex, sockCost[selectedSockIndex]))
            {
                //success
                SetSock(selectedSockIndex);

                //change color button
                sockPanel.GetChild(selectedSockIndex).GetComponent<Image>().color = Color.white;

                //update visual money text
                UpdateMoneyText();

            }else
            {
                Debug.Log("youre broke");
            }

        }

        
    }

    public void OnSkateBuySet ()
    {
        Debug.Log("buy or set skate");
        
        //is it owned
        if (SaveManager.Instance.IsAriSkateOwned(selectedSkateIndex))
        {
            //set color hair
            
            SetSkate(selectedSkateIndex);
        }
        else
        {
            //attempt to buy
            if (SaveManager.Instance.BuyAriSkate(selectedSkateIndex, skateCost[selectedSkateIndex]))
            {
                //success
                SetSkate(selectedSkateIndex);

                //change color button
                skatePanel.GetChild(selectedSkateIndex).GetComponent<Image>().color = Color.white;

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



   
    public void ResetThatSave()
    {
        SaveManager.Instance.ResetSave();
        Application.Quit();
        Debug.Log("im trying to wipe");
    }

    public void GameObjectAccessorySetting ()
    {
        for (int i = 0; i <  ManagerREF.ariAccessoryOptions.Length; i++)
        {
            if (selectedAccessoryIndex != i)
            {
                //turn the object off
                ManagerREF.ariAccessoryOptions[i].SetActive(false);
            } else 
            {
                //turn it on
                ManagerREF.ariAccessoryOptions[i].SetActive(true);
            }
        }
    }

    public void GameObjectTopSetting ()
    {
        for (int i = 0; i <  ManagerREF.ariTopOptions.Length; i++)
        {
            if (selectedTopIndex != i)
            {
                //turn the object off
                ManagerREF.ariTopOptions[i].SetActive(false);
            } else 
            {
                //turn it on
                ManagerREF.ariTopOptions[i].SetActive(true);
            }
        }
    }

    public void GameObjectBottomSetting ()
    {
        for (int i = 0; i <  ManagerREF.ariBottomOptions.Length; i++)
        {
            if (selectedBottomIndex != i)
            {
                //turn the object off
                ManagerREF.ariBottomOptions[i].SetActive(false);
            } else 
            {
                //turn it on
                ManagerREF.ariBottomOptions[i].SetActive(true);
            }
        }
    }

    public void GameObjectSockSetting ()
    {
        for (int i = 0; i <  ManagerREF.ariSockOptions.Length; i++)
        {
            if (selectedSockIndex != i)
            {
                //turn the object off
                ManagerREF.ariSockOptions[i].SetActive(false);
            } else 
            {
                //turn it on
                ManagerREF.ariSockOptions[i].SetActive(true);
            }
        }
    }

    public void GameObjectSkateSetting ()
    {
        for (int i = 0; i <  ManagerREF.ariSkateOptions.Length; i++)
        {
            if (selectedSkateIndex != i)
            {
                //turn the object off
                ManagerREF.ariSkateOptions[i].SetActive(false);
            } else 
            {
                //turn it on
                ManagerREF.ariSkateOptions[i].SetActive(true);
            }
        }
    }

    public void GameObjectHairSetting ()
    {
        for (int i = 0; i <  ManagerREF.ariHairOptions.Length; i++)
        {
            if (selectedHairIndex != i)
            {
                //turn the object off
                ManagerREF.ariHairOptions[i].SetActive(false);
            } else 
            {
                //turn it on
                ManagerREF.ariHairOptions[i].SetActive(true);
            }
        }
    }


    

    

    public void ResetOutfitToSaveState()
    {
        //selectedAccessoryIndex = SaveManager.Instance.state.activeAccessoryIndex;
        selectedTopIndex = SaveManager.Instance.state.activeAriTop;
        selectedBottomIndex = SaveManager.Instance.state.activeAriBottom;
        selectedSockIndex = SaveManager.Instance.state.activeAriSock;
        selectedSkateIndex = SaveManager.Instance.state.activeAriSkate;
        selectedHairIndex = SaveManager.Instance.state.activeAriHair;

        
        //GameObjectAccessorySetting();
        GameObjectTopSetting();
        GameObjectBottomSetting();
        GameObjectSockSetting();
        GameObjectSkateSetting();
        GameObjectHairSetting();
    
    }


    

    /*public void MaxOutMoney ()
    {
        SaveManager.Instance.state.Money = 999;
        Debug.Log("maxmoney");
        UpdateMoneyText();
    }*/
}
