using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using TMPro;

public class ProgressTab : MonoBehaviour
{
    public string[] Act1Titles;
    public string[] Act2Titles;
    public string[] Act3Titles;
    public string[] Act1TextBoxes;
    public string[] Act2TextBoxes;
    public string[] Act3TextBoxes;
    public GameObject[] Act1Buttons;
    public GameObject[] Act2Buttons;
    public GameObject[] Act3Buttons;
    [SerializeField] private TextMeshProUGUI mainText;
    [SerializeField] private TextMeshProUGUI titleBanner;

    public Sprite selected;
    public Sprite notSelected;

    [SerializeField] GameObject[] ActTabs;
    public GameObject[] DropDown;

    Animator anim;

    private GameObject canvasREF;
    private InfoScreen infoScreen;


    void Start()
    {
        mainText.text = "";
        titleBanner.text = "";
        for (int i = 0; i < Act1Titles.Length; i++)
        {
            Transform buttonText = Act1Buttons[i].gameObject.transform.Find("Text (TMP)");
            buttonText.gameObject.GetComponent<TextMeshProUGUI>().text = Act1Titles[i];
            Act1Buttons[i].name = Act1Titles[i];
        }

        for (int i = 0; i < Act2Titles.Length; i++)
        {
            Transform buttonText = Act2Buttons[i].gameObject.transform.Find("Text (TMP)");
            buttonText.gameObject.GetComponent<TextMeshProUGUI>().text = Act2Titles[i];
            Act2Buttons[i].name = Act2Titles[i];
        }

        for (int i = 0; i < Act3Titles.Length; i++)
        {
            Transform buttonText = Act3Buttons[i].gameObject.transform.Find("Text (TMP)");
            buttonText.gameObject.GetComponent<TextMeshProUGUI>().text = Act3Titles[i];
            Act3Buttons[i].name = Act3Titles[i];
        }

        anim = GetComponent<Animator>();

        canvasREF = GameObject.Find("canvasPrefab");
        infoScreen = canvasREF.GetComponent<InfoScreen>();
    }

    public void DatalogEntryClick(GameObject button)
    {   
        if (DropDown[0].activeInHierarchy)
        {
            for (int i = 0; i < Act1Titles.Length; i++)
            {
                Act1Buttons[i].GetComponent<Image>().sprite = notSelected;
                if (button.name == Act1Titles[i])
                    {
                        titleBanner.text = Act1Titles[i];
                        mainText.text = Act1TextBoxes[i];
                    }
            }
        } else if (DropDown[1].activeInHierarchy)
        {
            for (int i = 0; i < Act2Titles.Length; i++)
            {
                Act2Buttons[i].GetComponent<Image>().sprite = notSelected;
                if (button.name == Act2Titles[i])
                    {
                        titleBanner.text = Act2Titles[i];
                        mainText.text = Act2TextBoxes[i];
                    }
            }
        } else if (DropDown[2].activeInHierarchy)
        {
            for (int i = 0; i < Act3Titles.Length; i++)
            {
                Act3Buttons[i].GetComponent<Image>().sprite = notSelected;
                if (button.name == Act3Titles[i])
                    {
                        titleBanner.text = Act3Titles[i];
                        mainText.text = Act3TextBoxes[i];
                    }
                }
        }
        
        button.GetComponent<Image>().sprite = selected;
        infoScreen.PlaySoundUI(infoScreen.selectSound);

    }

    public void ActivateTab(int select)
    {
        if (DropDown[select - 1].activeInHierarchy)
        {
            DropDown[select - 1].SetActive(false);
            for (int i = 0; i < ActTabs.Length; i++)
            {
                ActTabs[i].SetActive(true);
            }
            anim.SetTrigger("Return");
            mainText.text = "";
            titleBanner.text = "";
        } else {

            for (int i = 0; i < DropDown.Length; i++)
            {
                DropDown[i].SetActive(false);
                ActTabs[i].SetActive(true);
            }

            if (select == 1)
            {
                //anim.SetTrigger("Act1");
                anim.CrossFade("Act1Open", 0);
                ActTabs[0].GetComponent<Button>().Select();
            } else if (select == 2)
            {
                //anim.SetTrigger("Act2");
                anim.CrossFade("Act2Open", 0);
                ActTabs[1].GetComponent<Button>().Select();
            } else if (select == 3)
            {
                //anim.SetTrigger("Act3");
                anim.CrossFade("Act3Open", 0);
                ActTabs[2].GetComponent<Button>().Select();
            }
        }

        infoScreen.PlaySoundUI(infoScreen.selectSound);
        
    }

    public void AnimationHandler(string condition)
    {
        if (condition == "Act1Open")
        {
            ActTabs[1].SetActive(false);
            ActTabs[2].SetActive(false);
            DropDown[0].SetActive(true);
        } else if (condition == "Act2Open")
        {
            ActTabs[0].SetActive(false);
            ActTabs[2].SetActive(false);
            DropDown[1].SetActive(true);
        } else if (condition == "Act3Open")
        {
            ActTabs[0].SetActive(false);
            ActTabs[1].SetActive(false);
            DropDown[2].SetActive(true);
        } 
    }

    //Animation Event
    public void AnimPassthrough(string tab)
    {
        infoScreen.TabOpened(tab);
    }
}
