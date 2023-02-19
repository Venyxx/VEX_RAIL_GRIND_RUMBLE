using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTab : MonoBehaviour
{
    InfoScreen infoScreen;
    
    void Start()
    {
        infoScreen = GameObject.Find("canvasPrefab").GetComponent<InfoScreen>();
    }

    
    void Update()
    {
        
    }

    //Animation Event
    public void AnimPassthrough(string tab)
    {
        infoScreen.TabOpened(tab);
    }
}
