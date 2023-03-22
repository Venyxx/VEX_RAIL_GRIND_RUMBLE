using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager Instance {set; get;}

    //public Material wallMaterial;
    //public GameObject[] roomThemeGameObjects = new GameObject[8];
    //public Material[] roomWallPapers = new Material[8];

    public Material ariMaterial;
    public GameObject ariGraffitiSlotUp1;
    public GameObject ariGraffitiSlotRight2;
    public GameObject ariGraffitiSlotDown3;
    public GameObject ariGraffitiSlotLeft4;

    public Material[] ariMaterialOptions = new Material[3];
    public GameObject[] ariGraffitiOptions = new GameObject[8];
    public GameObject[] ariHairOptions = new GameObject[4];
    public GameObject[] ariAccessoryOptions = new GameObject[3];
    public GameObject[] ariTopOptions = new GameObject[11];
    public GameObject[] ariBottomOptions = new GameObject[6];
    public GameObject[] ariSockOptions = new GameObject[2];
    public GameObject[] ariSkateOptions = new GameObject[6];
    


    private void Awake ()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
        
    }

    public int currentLevel = 0;
    //public int menuFocus = 0;



}
