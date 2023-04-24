using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutfitSwapping : MonoBehaviour
{
    // Start is called before the first frame update

    private Manager ManagerREF;
    private ETCCustomizationVendor ETCCusREF;
    private GameObject ariOBJREF;

    //private GameObject ariAccessoryREF;
    private GameObject ariTopREF;
    private GameObject ariBottomREF;
    private GameObject ariSockREF;
    private GameObject ariSkateREF;

    public GameObject ETCOBJ;


    
    

    void Start()
    {

        

        ManagerREF = GameObject.Find("Manager").GetComponent<Manager>();
        ariOBJREF = GameObject.Find("AriRig");
        ariTopREF = GameObject.Find("tops");
        ariBottomREF = GameObject.Find("bottoms");
        ariSockREF = GameObject.Find("socks");
        ariSkateREF = GameObject.Find("skates");


        //sry gonna hard code
        //Debug.Log("fit setting");
        ManagerREF.ariTopOptions[0] = GameObject.Find("OG_top");
        ManagerREF.ariTopOptions[1] = GameObject.Find("frillyTopB");
        ManagerREF.ariTopOptions[2] = GameObject.Find("longSleeveA");
        ManagerREF.ariTopOptions[3] = GameObject.Find("longSleeveB");
        ManagerREF.ariTopOptions[4] = GameObject.Find("frillyTopA");
        ManagerREF.ariTopOptions[5] = GameObject.Find("shortSleeveC");
        ManagerREF.ariTopOptions[6] = GameObject.Find("shortSleeveA");
        ManagerREF.ariTopOptions[7] = GameObject.Find("shortSleeveB");
        ManagerREF.ariTopOptions[8] = GameObject.Find("suitJacket");
        ManagerREF.ariTopOptions[9] = GameObject.Find("tankTopA");
        ManagerREF.ariTopOptions[10] = GameObject.Find("tankTopB");
        ManagerREF.ariTopOptions[11] = GameObject.Find("longSleeveC");


        ManagerREF.ariBottomOptions[0] = GameObject.Find("ClassicPants");
        ManagerREF.ariBottomOptions[1] = GameObject.Find("longPantssA");
        ManagerREF.ariBottomOptions[2] = GameObject.Find("longPantsB");
        ManagerREF.ariBottomOptions[3] = GameObject.Find("longPantsC");
        ManagerREF.ariBottomOptions[4] = GameObject.Find("longPantsD");
        ManagerREF.ariBottomOptions[5] = GameObject.Find("longPantsE");
        ManagerREF.ariBottomOptions[6] = GameObject.Find("shorts_A");
        ManagerREF.ariBottomOptions[7] = GameObject.Find("shorts_B");
        ManagerREF.ariBottomOptions[8] = GameObject.Find("shortsC");
        ManagerREF.ariBottomOptions[9] = GameObject.Find("shortsD");
        ManagerREF.ariBottomOptions[10] = GameObject.Find("shortsE");
        ManagerREF.ariBottomOptions[11] = GameObject.Find("suitPants");


        ManagerREF.ariSockOptions[0] = GameObject.Find("BLANKSOCKS");
        ManagerREF.ariSockOptions[1] = GameObject.Find("lowSocks");
        ManagerREF.ariSockOptions[2] = GameObject.Find("highSocks");

        ManagerREF.ariSkateOptions[0] = GameObject.Find("classicSkates");
        ManagerREF.ariSkateOptions[1] = GameObject.Find("GetSetBroadcast");
        ManagerREF.ariSkateOptions[2] = GameObject.Find("JuniorMiles");
        ManagerREF.ariSkateOptions[3] = GameObject.Find("soleSlider");
        ManagerREF.ariSkateOptions[4] = GameObject.Find("craziTaxiSkate");
        ManagerREF.ariSkateOptions[5] = GameObject.Find("bruiseControl3");

        ManagerREF.ariHairOptions[0] = GameObject.Find("twoBraids");
        ManagerREF.ariHairOptions[1] = GameObject.Find("singleBun");
        ManagerREF.ariHairOptions[2] = GameObject.Find("singleBraid");
        ManagerREF.ariHairOptions[3] = GameObject.Find("messyBun");

        ManagerREF.ariAccessoryOptions[0] = GameObject.Find("BLANKPIERCINGS");
        ManagerREF.ariAccessoryOptions[1] = GameObject.Find("earringSphere");
        ManagerREF.ariAccessoryOptions[2] = GameObject.Find("Hoops");

        Debug.Log("trying masks");
        ManagerREF.ariMaskOptions[0] = GameObject.Find("BLANKMASK");
        ManagerREF.ariMaskOptions[1] = GameObject.Find("roundGlasses");
        ManagerREF.ariMaskOptions[2] = GameObject.Find("sunGlasses1");
        ManagerREF.ariMaskOptions[3] = GameObject.Find("Android");
        ManagerREF.ariMaskOptions[4] = GameObject.Find("Trickster");
    



    }


}
