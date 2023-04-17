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

    [System.NonSerialized] public Material[] ariMaterialOptions = new Material[3];
    [System.NonSerialized] public GameObject[] ariGraffitiOptions = new GameObject[8];
    [System.NonSerialized] public GameObject[] ariHairOptions = new GameObject[4];
    [System.NonSerialized] public GameObject[] ariAccessoryOptions = new GameObject[3]; //piercing
    [System.NonSerialized] public GameObject[] ariTopOptions = new GameObject[12];
    [System.NonSerialized] public GameObject[] ariBottomOptions = new GameObject[12];
    [System.NonSerialized] public GameObject[] ariSockOptions = new GameObject[3];
    [System.NonSerialized] public GameObject[] ariSkateOptions = new GameObject[6];
    [System.NonSerialized] public GameObject[] ariMaskOptions = new GameObject[5]; // includes glasses

   



    private void Awake ()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(gameObject);
        
        
    }

    public int currentLevel = 0;
    //public int menuFocus = 0;



}
