using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutfitSwapping : MonoBehaviour
{
    // Start is called before the first frame update

    private CustomizationOptionsStruct customizationOptions;
    private CustomizationVendor _cusRef;
    private GameObject ariOBJREF;

    //private GameObject ariAccessoryREF;
    private GameObject ariTopREF;
    private GameObject ariBottomREF;
    private GameObject ariSockREF;
    private GameObject ariSkateREF;

    public GameObject ETCOBJ;


    
    

    void Start()
    {

        

        customizationOptions = GameObject.Find("Customization Options").GetComponent<CustomizationOptionsStruct>();
        ariOBJREF = GameObject.Find("AriRig");
        ariTopREF = GameObject.Find("tops");
        ariBottomREF = GameObject.Find("bottoms");
        ariSockREF = GameObject.Find("socks");
        ariSkateREF = GameObject.Find("skates");


        //sry gonna hard code
        //Debug.Log("fit setting");
        customizationOptions.ariTopOptions[0] = GameObject.Find("OG_top");
        customizationOptions.ariTopOptions[1] = GameObject.Find("frillyTopB");
        customizationOptions.ariTopOptions[2] = GameObject.Find("longSleeveA");
        customizationOptions.ariTopOptions[3] = GameObject.Find("longSleeveB");
        customizationOptions.ariTopOptions[4] = GameObject.Find("frillyTopA");
        customizationOptions.ariTopOptions[5] = GameObject.Find("shortSleeveC");
        customizationOptions.ariTopOptions[6] = GameObject.Find("shortSleeveA");
        customizationOptions.ariTopOptions[7] = GameObject.Find("shortSleeveB");
        customizationOptions.ariTopOptions[8] = GameObject.Find("suitJacket");
        customizationOptions.ariTopOptions[9] = GameObject.Find("tankTopA");
        customizationOptions.ariTopOptions[10] = GameObject.Find("tankTopB");
        customizationOptions.ariTopOptions[11] = GameObject.Find("longSleeveC");


        customizationOptions.ariBottomOptions[0] = GameObject.Find("ClassicPants");
        customizationOptions.ariBottomOptions[1] = GameObject.Find("longPantssA");
        customizationOptions.ariBottomOptions[2] = GameObject.Find("longPantsB");
        customizationOptions.ariBottomOptions[3] = GameObject.Find("longPantsC");
        customizationOptions.ariBottomOptions[4] = GameObject.Find("longPantsD");
        customizationOptions.ariBottomOptions[5] = GameObject.Find("longPantsE");
        customizationOptions.ariBottomOptions[6] = GameObject.Find("shorts_A");
        customizationOptions.ariBottomOptions[7] = GameObject.Find("shorts_B");
        customizationOptions.ariBottomOptions[8] = GameObject.Find("shortsC");
        customizationOptions.ariBottomOptions[9] = GameObject.Find("shortsD");
        customizationOptions.ariBottomOptions[10] = GameObject.Find("shortsE");
        customizationOptions.ariBottomOptions[11] = GameObject.Find("suitPants");


        customizationOptions.ariSockOptions[0] = GameObject.Find("BLANKSOCKS");
        customizationOptions.ariSockOptions[1] = GameObject.Find("lowSocks");
        customizationOptions.ariSockOptions[2] = GameObject.Find("highSocks");

        customizationOptions.ariSkateOptions[0] = GameObject.Find("classicSkates");
        customizationOptions.ariSkateOptions[1] = GameObject.Find("GetSetBroadcast");
        customizationOptions.ariSkateOptions[2] = GameObject.Find("JuniorMiles");
        customizationOptions.ariSkateOptions[3] = GameObject.Find("soleSlider");
        customizationOptions.ariSkateOptions[4] = GameObject.Find("craziTaxiSkate");
        customizationOptions.ariSkateOptions[5] = GameObject.Find("bruiseControl3");

        customizationOptions.ariHairOptions[0] = GameObject.Find("twoBraids");
        customizationOptions.ariHairOptions[1] = GameObject.Find("singleBun");
        customizationOptions.ariHairOptions[2] = GameObject.Find("singleBraid");
        customizationOptions.ariHairOptions[3] = GameObject.Find("messyBun");

        customizationOptions.ariAccessoryOptions[0] = GameObject.Find("BLANKPIERCINGS");
        customizationOptions.ariAccessoryOptions[1] = GameObject.Find("earringSphere");
        customizationOptions.ariAccessoryOptions[2] = GameObject.Find("Hoops");

        Debug.Log("trying masks");
        customizationOptions.ariMaskOptions[0] = GameObject.Find("BLANKMASK");
        customizationOptions.ariMaskOptions[1] = GameObject.Find("roundGlasses");
        customizationOptions.ariMaskOptions[2] = GameObject.Find("sunGlasses1");
        customizationOptions.ariMaskOptions[3] = GameObject.Find("Android");
        customizationOptions.ariMaskOptions[4] = GameObject.Find("Trickster");
    



    }


}
