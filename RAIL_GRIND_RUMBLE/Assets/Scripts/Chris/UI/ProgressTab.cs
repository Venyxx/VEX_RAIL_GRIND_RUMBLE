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

    //Need to be added when finished
    public string[] Act1SpanishTitles;
    public string[] Act2SpanishTitles;
    public string[] Act3SpanishTitles;
    public string[] Act1SpanishTextBoxes;
    public string[] Act2SpanishTextBoxes;
    public string[] Act3SpanishTextBoxes;
    //

    public GameObject[] Act1Buttons;
    public GameObject[] Act2Buttons;
    public GameObject[] Act3Buttons;
    public static bool[] Act1Active = new bool[7];
    public static bool[] Act2Active = new bool[6];
    public static bool[] Act3Active = new bool[5];
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

        SetButtons();

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
                        if (SpanishMode.spanishMode)
                        {
                            titleBanner.text = Act1SpanishTitles[i];
                            mainText.text = Act1SpanishTextBoxes[i];
                        } else {
                            titleBanner.text = Act1Titles[i];
                            mainText.text = Act1TextBoxes[i];
                        }
                        
                    }
            }
        } else if (DropDown[1].activeInHierarchy)
        {
            for (int i = 0; i < Act2Titles.Length; i++)
            {
                Act2Buttons[i].GetComponent<Image>().sprite = notSelected;
                if (button.name == Act2Titles[i])
                    {
                        if (SpanishMode.spanishMode)
                        {
                            titleBanner.text = Act2SpanishTitles[i];
                            mainText.text = Act1SpanishTextBoxes[i];
                        } else {
                            titleBanner.text = Act2Titles[i];
                            mainText.text = Act2TextBoxes[i];
                        }
                    }
            }
        } else if (DropDown[2].activeInHierarchy)
        {
            for (int i = 0; i < Act3Titles.Length; i++)
            {
                Act3Buttons[i].GetComponent<Image>().sprite = notSelected;
                if (button.name == Act3Titles[i])
                    {
                        if (SpanishMode.spanishMode)
                        {
                            titleBanner.text = Act2SpanishTitles[i];
                            mainText.text = Act2SpanishTextBoxes[i];
                        } else {
                            titleBanner.text = Act2Titles[i];
                            mainText.text = Act2TextBoxes[i];
                        }
                    }
                }
        }
        
        button.GetComponent<Image>().sprite = selected;
        infoScreen.PlaySoundUI(infoScreen.selectSound);

    }

    public void ActivateTab(int select)
    {
        if (select != 0 && DropDown[select - 1].activeInHierarchy)
        {
            DropDown[select - 1].SetActive(false);
            for (int i = 0; i < ActTabs.Length; i++)
            {
                ActTabs[i].SetActive(true);
            }
            //anim.SetTrigger("Return");
            mainText.text = "";
            titleBanner.text = "";
        } else {

            for (int i = 0; i < DropDown.Length; i++)
            {
                DropDown[i].SetActive(false);
                ActTabs[i].SetActive(true);
            }

            if (select == 0)
            {
                mainText.text = "";
                titleBanner.text = "";
                return;
            } else if (select == 1)
            {
                //anim.SetTrigger("Act1");
                //anim.CrossFade("Act1Open", 0);

                DropDown[0].SetActive(true);

                ActTabs[0].GetComponent<Button>().Select();
            } else if (select == 2)
            {
                //anim.SetTrigger("Act2");
                //anim.CrossFade("Act2Open", 0);

                DropDown[1].SetActive(true);

                ActTabs[1].GetComponent<Button>().Select();
            } else if (select == 3)
            {
                //anim.SetTrigger("Act3");
                //anim.CrossFade("Act3Open", 0);

                DropDown[2].SetActive(true);

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

    public void UnlockEntry(int actNum, int entryNum)
    {
        if (actNum == 1)
        {
            Act1Active[entryNum] = true;
            Act1Buttons[entryNum].SetActive(true);
        } else if (actNum == 2)
        {
            Act2Active[entryNum] = true;
            Act2Buttons[entryNum].SetActive(true);
        } else if (actNum == 3)
        {
            Act3Active[entryNum] = true;
            Act3Buttons[entryNum].SetActive(true);
        }
    }

    public void SetButtons()
    {
        for (int i = 0; i < Act1Titles.Length; i++)
        {
            Transform buttonText = Act1Buttons[i].gameObject.transform.Find("Text (TMP)");

            if (SpanishMode.spanishMode)
            {
                buttonText.gameObject.GetComponent<TextMeshProUGUI>().text = Act1SpanishTitles[i];
            } else {
                buttonText.gameObject.GetComponent<TextMeshProUGUI>().text = Act1Titles[i];
            }

            Act1Buttons[i].name = Act1Titles[i];

            if (Act1Active[i] == true)
            {
                Act1Buttons[i].SetActive(true);
            }
        }

        for (int i = 0; i < Act2Titles.Length; i++)
        {
            Transform buttonText = Act2Buttons[i].gameObject.transform.Find("Text (TMP)");

            if (SpanishMode.spanishMode)
            {
                buttonText.gameObject.GetComponent<TextMeshProUGUI>().text = Act2SpanishTitles[i];
            } else {
                buttonText.gameObject.GetComponent<TextMeshProUGUI>().text = Act2Titles[i];
            }

            Act2Buttons[i].name = Act2Titles[i];

            if (Act2Active[i] == true)
            {
                Act2Buttons[i].SetActive(true);
            }
        }

        for (int i = 0; i < Act3Titles.Length; i++)
        {
            Transform buttonText = Act3Buttons[i].gameObject.transform.Find("Text (TMP)");

            if (SpanishMode.spanishMode)
            {
                buttonText.gameObject.GetComponent<TextMeshProUGUI>().text = Act3SpanishTitles[i];
            } else {
                buttonText.gameObject.GetComponent<TextMeshProUGUI>().text = Act3Titles[i];
            }

            Act3Buttons[i].name = Act3Titles[i];

            if (Act3Active[i] == true)
            {
                Act3Buttons[i].SetActive(true);
            }
        }
    }
}
