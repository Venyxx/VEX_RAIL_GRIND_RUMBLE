using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class MissionsTab : MonoBehaviour
{
    //Top Tabs
    public GameObject OutskirtsTab;
    public GameObject InnerRingTab;
    public GameObject ServosHQTab;

    //Side Tabs

    public GameObject MainMissionTab;
    public GameObject SideHustleTab;
    public GameObject CompletedTab;

    //Mission Lists
    public GameObject[] OutskirtsLists;
    public GameObject[] InnerRingLists;
    public GameObject[] ServosHQLists;
    
    //Buttons for selection
    public Button MainMissionButton;
    public Button SideHustleButton;
    public Button CompletedButton;
    public Button OutskirtsButton;
    public Button InnerRingButton;
    public Button ServosHQButton;
    public Sprite locationSelected;
    public Sprite locationNotSelected;
    public Sprite missionTypeSelected;
    public Sprite missionTypeNotSelected;

    private GameObject canvasREF;
    private InfoScreen infoScreen;
    private AudioSource audioSource;

    void Start()
    {
        canvasREF = GameObject.Find("canvasPrefab");
        infoScreen = canvasREF.GetComponent<InfoScreen>();
        audioSource = canvasREF.GetComponent<AudioSource>();
    }

    void Awake()
    {
        for (int i = 0; i < 3; i++)
        {
            OutskirtsLists[i].SetActive(false);
            InnerRingLists[i].SetActive(false);
            ServosHQLists[i].SetActive(false);
        }

        SideHustleTab.SetActive(false);
        CompletedTab.SetActive(false);

        InnerRingTab.SetActive(false);
        ServosHQTab.SetActive(false);

        MainMissionTab.SetActive(true);
        OutskirtsTab.SetActive(true);
        OutskirtsLists[0].SetActive(true);
    }

    public void OpenMainMissions()
    {
        MainMissionTab.SetActive(true);
        SideHustleTab.SetActive(false);
        CompletedTab.SetActive(false);

        MainMissionButton.GetComponent<Image>().sprite = missionTypeSelected;
        SideHustleButton.GetComponent<Image>().sprite = missionTypeNotSelected;
        CompletedButton.GetComponent<Image>().sprite = missionTypeNotSelected;

        for (int i = 0; i < 3; i++)
        {
            OutskirtsLists[i].SetActive(false);
            InnerRingLists[i].SetActive(false);
            ServosHQLists[i].SetActive(false);
        }

        if (OutskirtsTab.activeInHierarchy == true)
        {
            OutskirtsLists[0].SetActive(true);
        } else if (InnerRingTab.activeInHierarchy == true)
        {
            InnerRingLists[0].SetActive(true);
        } else if (ServosHQTab.activeInHierarchy == true)
        {
            ServosHQLists[0].SetActive(true);
        }

        PlaySound();
    }

    public void OpenSideHustle()
    {
        MainMissionTab.SetActive(false);
        SideHustleTab.SetActive(true);
        CompletedTab.SetActive(false);

        MainMissionButton.GetComponent<Image>().sprite = missionTypeNotSelected;
        SideHustleButton.GetComponent<Image>().sprite = missionTypeSelected;
        CompletedButton.GetComponent<Image>().sprite = missionTypeNotSelected;

        for (int i = 0; i < 3; i++)
        {
            OutskirtsLists[i].SetActive(false);
            InnerRingLists[i].SetActive(false);
            ServosHQLists[i].SetActive(false);
        }

        if (OutskirtsTab.activeInHierarchy == true)
        {
            OutskirtsLists[1].SetActive(true);
        } else if (InnerRingTab.activeInHierarchy == true)
        {
            InnerRingLists[1].SetActive(true);
        } else if (ServosHQTab.activeInHierarchy == true)
        {
            ServosHQLists[1].SetActive(true);
        }

        PlaySound();
    }

    public void OpenCompleted()
    {
        MainMissionTab.SetActive(false);
        SideHustleTab.SetActive(false);
        CompletedTab.SetActive(true);

        MainMissionButton.GetComponent<Image>().sprite = missionTypeNotSelected;
        SideHustleButton.GetComponent<Image>().sprite = missionTypeNotSelected;
        CompletedButton.GetComponent<Image>().sprite = missionTypeSelected;

        for (int i = 0; i < 3; i++)
        {
            OutskirtsLists[i].SetActive(false);
            InnerRingLists[i].SetActive(false);
            ServosHQLists[i].SetActive(false);
        }

        if (OutskirtsTab.activeInHierarchy == true)
        {
            OutskirtsLists[2].SetActive(true);
        } else if (InnerRingTab.activeInHierarchy == true)
        {
            InnerRingLists[2].SetActive(true);
        } else if (ServosHQTab.activeInHierarchy == true)
        {
            ServosHQLists[2].SetActive(true);
        }

        PlaySound();
    }

    public void OpenOutskirts()
    {
        OutskirtsTab.SetActive(true);
        InnerRingTab.SetActive(false);
        ServosHQTab.SetActive(false);

        OutskirtsButton.GetComponent<Image>().sprite = locationSelected;
        InnerRingButton.GetComponent<Image>().sprite = locationNotSelected;
        ServosHQButton.GetComponent<Image>().sprite = locationNotSelected;

        for (int i = 0; i < 3; i++)
        {
            OutskirtsLists[i].SetActive(false);
            InnerRingLists[i].SetActive(false);
            ServosHQLists[i].SetActive(false);
        }

        if (MainMissionTab.activeInHierarchy == true)
        {
            OutskirtsLists[0].SetActive(true);
        } else if (SideHustleTab.activeInHierarchy == true)
        {
            OutskirtsLists[1].SetActive(true);
        } else if (CompletedTab.activeInHierarchy == true)
        {
            OutskirtsLists[2].SetActive(true);
        }

        PlaySound();
    }

    public void OpenInnerRing()
    {
        OutskirtsTab.SetActive(false);
        InnerRingTab.SetActive(true);
        ServosHQTab.SetActive(false);

        OutskirtsButton.GetComponent<Image>().sprite = locationNotSelected;
        InnerRingButton.GetComponent<Image>().sprite = locationSelected;
        ServosHQButton.GetComponent<Image>().sprite = locationNotSelected;

        for (int i = 0; i < 3; i++)
        {
            OutskirtsLists[i].SetActive(false);
            InnerRingLists[i].SetActive(false);
            ServosHQLists[i].SetActive(false);
        }

        if (MainMissionTab.activeInHierarchy == true)
        {
            InnerRingLists[0].SetActive(true);
        } else if (SideHustleTab.activeInHierarchy == true)
        {
            InnerRingLists[1].SetActive(true);
        } else if (CompletedTab.activeInHierarchy == true)
        {
            InnerRingLists[2].SetActive(true);
        }

        PlaySound();
    }

    public void OpenServosHQ()
    {
        OutskirtsTab.SetActive(false);
        InnerRingTab.SetActive(false);
        ServosHQTab.SetActive(true);

        OutskirtsButton.GetComponent<Image>().sprite = locationNotSelected;
        InnerRingButton.GetComponent<Image>().sprite = locationNotSelected;
        ServosHQButton.GetComponent<Image>().sprite = locationSelected;

        for (int i = 0; i < 3; i++)
        {
            OutskirtsLists[i].SetActive(false);
            InnerRingLists[i].SetActive(false);
            ServosHQLists[i].SetActive(false);
        }

        if (MainMissionTab.activeInHierarchy == true)
        {
            ServosHQLists[0].SetActive(true);
        } else if (SideHustleTab.activeInHierarchy == true)
        {
            ServosHQLists[1].SetActive(true);
        } else if (CompletedTab.activeInHierarchy == true)
        {
            ServosHQLists[2].SetActive(true);
        }

        PlaySound();
    }

    //Trigger Mapping
    public void RightTriggerPressed(InputAction.CallbackContext context)
    {
        if (!context.started || InfoScreen.isOpen == false) return;

        if (OutskirtsTab.activeInHierarchy)
        {
            OpenInnerRing();
        } else if (InnerRingTab.activeInHierarchy)
        {
            OpenServosHQ();
        } else if (ServosHQTab.activeInHierarchy)
        {
            OpenOutskirts();
        }
    }

    public void LeftTriggerPressed(InputAction.CallbackContext context)
    {
        if (!context.started || InfoScreen.isOpen == false) return;

        if (OutskirtsTab.activeInHierarchy)
        {
            OpenServosHQ();
        } else if (InnerRingTab.activeInHierarchy)
        {
            OpenOutskirts();
        } else if (ServosHQTab.activeInHierarchy)
        {
            OpenInnerRing();
        }
    }

    //Sounds
    void PlaySound()
    {
        audioSource.clip = infoScreen.selectSound;
        audioSource.Play(0);
    }
}
