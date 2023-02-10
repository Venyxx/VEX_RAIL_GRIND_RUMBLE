using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

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
    }

    public void OpenSideHustle()
    {
        MainMissionTab.SetActive(false);
        SideHustleTab.SetActive(true);
        CompletedTab.SetActive(false);

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
    }

    public void OpenCompleted()
    {
        MainMissionTab.SetActive(false);
        SideHustleTab.SetActive(false);
        CompletedTab.SetActive(true);

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
    }

    public void OpenOutskirts()
    {
        OutskirtsTab.SetActive(true);
        InnerRingTab.SetActive(false);
        ServosHQTab.SetActive(false);

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
    }

    public void OpenInnerRing()
    {
        OutskirtsTab.SetActive(false);
        InnerRingTab.SetActive(true);
        ServosHQTab.SetActive(false);

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
    }

    public void OpenServosHQ()
    {
        OutskirtsTab.SetActive(false);
        InnerRingTab.SetActive(false);
        ServosHQTab.SetActive(true);

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
    }
}
