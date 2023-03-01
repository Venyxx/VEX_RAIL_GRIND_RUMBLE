using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EmptyMission : MonoBehaviour
{
    [SerializeField] GameObject newIcon;
    public bool isMainMission;
    bool hasBeenClicked;
    public TextMeshProUGUI missionTitle;
    public TextMeshProUGUI missionDesc;
    public TextMeshProUGUI missionObj;
    // Start is called before the first frame update
    void Start()
    {
        hasBeenClicked = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Select()
    {
        if (hasBeenClicked == false)
        {
            hasBeenClicked = true;
            newIcon.SetActive(false);
        }
    }
}
