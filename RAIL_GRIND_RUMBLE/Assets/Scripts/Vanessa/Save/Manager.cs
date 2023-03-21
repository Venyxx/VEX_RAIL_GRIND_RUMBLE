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

    public GameObject[] ariGraffitiOptions = new GameObject[8];
    public GameObject[] ariHairOptions = new GameObject[3];
    public Material[] ariMaterialOptions = new Material[3];


    private void Awake ()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
        
    }

    public int currentLevel = 0;
    //public int menuFocus = 0;



}
