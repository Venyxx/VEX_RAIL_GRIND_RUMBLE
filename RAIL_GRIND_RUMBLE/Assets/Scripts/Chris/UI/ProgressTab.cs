using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using TMPro;

public class ProgressTab : MonoBehaviour
{
    public string[] Titles;
    public string[] TextBoxes;
    public GameObject[] Buttons;
    [SerializeField] private TextMeshProUGUI mainText;
    [SerializeField] private TextMeshProUGUI titleBanner;

    public Sprite selected;
    public Sprite notSelected;

    public GameObject[] DropDown;
    public Button Act1Button;
    public Button ProgressTabButton;


    void Start()
    {
        mainText.text = "";
        titleBanner.text = "";
        for (int i = 0; i < Titles.Length; i++)
        {
            Transform buttonText = Buttons[i].gameObject.transform.Find("Text (TMP)");
            buttonText.gameObject.GetComponent<TextMeshProUGUI>().text = Titles[i];
        }
    }

    void Awake()
    {
        //Act1Button.Select();
    }

    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            Act1Button.Select();
        }
    }

    public void ProgressButtonClick(GameObject button)
    {
        for (int i = 0; i < Titles.Length; i++)
        {
            Buttons[i].GetComponent<Image>().sprite = notSelected;
            if (button.name == Titles[i])
            {
                titleBanner.text = Titles[i];
                mainText.text = TextBoxes[i];
            }
        }
        button.GetComponent<Image>().sprite = selected;
    }

    public void ActivateTab(int select)
    {
        if (DropDown[select - 1].activeInHierarchy)
        {
            DropDown[select - 1].SetActive(false);
        } else {

            for (int i = 0; i < DropDown.Length; i++)
            {
                DropDown[i].SetActive(false);
            }

            DropDown[select - 1].SetActive(true);
        }
    }
}
