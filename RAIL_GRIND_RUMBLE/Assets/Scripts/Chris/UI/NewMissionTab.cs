using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class NewMissionTab : MonoBehaviour
{
    //[SerializeField] string[] missionTitles;
    [SerializeField] string[] missionText;
    [SerializeField] TextMeshProUGUI locationText;
    //[SerializeField] TextMeshProUGUI titleObject;
    [SerializeField] TextMeshProUGUI textObject;
    [SerializeField] InfoScreen infoScreen;
    // Start is called before the first frame update
    void Start()
    {
        string scene = SceneManager.GetActiveScene().name;
        if (scene == "Outskirts")
        {
            locationText.text = "Outskirts";
        } else if (scene == "InnerRingLevel")
        {
            locationText.text = "Inner Ring";
        } else if (scene == "Servos HQ")
        {
            locationText.text = "Servos HQ";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewAnimPassthrough(string tab)
    {
        infoScreen.TabOpened(tab);
    }

    void SetMission(string mission)
    {

    }
}
