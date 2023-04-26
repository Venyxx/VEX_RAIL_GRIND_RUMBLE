using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class NewMissionTab : MonoBehaviour
{
    //[SerializeField] string[] missionTitles;
    [SerializeField] TextMeshProUGUI locationText;
    //[SerializeField] TextMeshProUGUI titleObject;
    [SerializeField] TextMeshProUGUI textObject;
    [SerializeField] InfoScreen infoScreen;
    [SerializeField] ProgressTab progressTab;
    [SerializeField] Sprite[] missionImages;
    [SerializeField] Image currentImage;
    public static int[] currentMission = new int[2];
    // Start is called before the first frame update
    void Start()
    {
        string scene = SceneManager.GetActiveScene().name;
        if (scene == "Outskirts" || scene == "Ari's House")
        {
            locationText.text = "Outskirts";
            currentImage.sprite = missionImages[0];

        } else if (scene == "InnerRingLevel")
        {
            locationText.text = "Inner Ring";
            currentImage.sprite = missionImages[1];
        } else if (scene == "Servos HQ")
        {
            locationText.text = "Servos HQ";
            currentImage.sprite = missionImages[2];
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.M))
        {
            SetMission(1, 5);
        }*/
    }

    public void NewAnimPassthrough(string tab)
    {
        infoScreen.TabOpened(tab);
    }

    public void SetMission(int actNum, int missionNum)
    {
        //Corrects for array placement
        missionNum = missionNum - 1;

        if (missionNum != 0)
        {
            for (int i = 0; i < missionNum; i++)
            {
                progressTab.UnlockEntry(actNum, i);
            }
        }

        currentMission[0] = actNum;
        currentMission[1] = missionNum;
    }

    public void SetLanguage(string lang)
    {
        if (currentMission[0] == 0) return;

        if (lang == "Spanish")
        {
            if (currentMission[0] == 1)
            {
                textObject.text = progressTab.Act1SpanishTextBoxes[currentMission[1]];
            } else if (currentMission[0] == 2)
            {
                textObject.text = progressTab.Act2SpanishTextBoxes[currentMission[1]];
            } else if (currentMission[0] == 3)
            {
                textObject.text = progressTab.Act3SpanishTextBoxes[currentMission[1]];
            }
        } else {
            if (currentMission[0] == 1)
            {
                textObject.text = progressTab.Act1TextBoxes[currentMission[1]];
            } else if (currentMission[0] == 2)
            {
                textObject.text = progressTab.Act2TextBoxes[currentMission[1]];
            } else if (currentMission[0] == 3)
            {
                textObject.text = progressTab.Act3TextBoxes[currentMission[1]];
            }
        }
    }
}
