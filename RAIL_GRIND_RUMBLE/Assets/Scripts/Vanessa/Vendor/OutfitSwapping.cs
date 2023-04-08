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
       if(SceneManager.GetActiveScene().name != "Ari's House")
       {
        Instantiate(ETCOBJ);
        GameObject.Find("VendorCanvas").SetActive(false);
        GameObject.Find("PromptController").SetActive(false);
       }
        

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
        ManagerREF.ariTopOptions[5] = GameObject.Find("oversizedShirt");
        ManagerREF.ariTopOptions[6] = GameObject.Find("shortSleeveA");
        ManagerREF.ariTopOptions[7] = GameObject.Find("shortSleeveB");
        ManagerREF.ariTopOptions[8] = GameObject.Find("suitJacket");
        //ManagerREF.ariTopOptions[9] = GameObject.Find("suitTie");
        ManagerREF.ariTopOptions[9] = GameObject.Find("tankTopA");
        ManagerREF.ariTopOptions[10] = GameObject.Find("tankTopB");
        ManagerREF.ariTopOptions[11] = GameObject.Find("tubeTopA");


        ManagerREF.ariBottomOptions[0] = GameObject.Find("OG_pants");
        ManagerREF.ariBottomOptions[1] = GameObject.Find("longPantsB");
        ManagerREF.ariBottomOptions[2] = GameObject.Find("longPantsC");
        ManagerREF.ariBottomOptions[3] = GameObject.Find("longPantssA");
        //ManagerREF.ariBottomOptions[4] = GameObject.Find("OG_pants2");
        ManagerREF.ariBottomOptions[4] = GameObject.Find("shorts_Aa");
        ManagerREF.ariBottomOptions[5] = GameObject.Find("shorts_B");


        ManagerREF.ariSockOptions[0] = GameObject.Find("lowSocks");
        ManagerREF.ariSockOptions[1] = GameObject.Find("highSocks");

        ManagerREF.ariSkateOptions[0] = GameObject.Find("classicSkates");
        ManagerREF.ariSkateOptions[1] = GameObject.Find("GetSetBroadcast");
        ManagerREF.ariSkateOptions[2] = GameObject.Find("JuniorMiles");
        ManagerREF.ariSkateOptions[3] = GameObject.Find("soleSlider2");
        ManagerREF.ariSkateOptions[4] = GameObject.Find("craziTaxiSkate");
        ManagerREF.ariSkateOptions[5] = GameObject.Find("bruiseControl3");

        ManagerREF.ariHairOptions[0] = GameObject.Find("doubleBraidsRig");
        ManagerREF.ariHairOptions[1] = GameObject.Find("messyBunsRig");

        //ManagerREF.ariAccessoryOptions[6] = GameObject.Find("");


        
        StartCoroutine(DespawnETC());
        

        



    }

    private IEnumerator DespawnETC()
    {
            
        yield return new WaitForSeconds(1);
        Destroy(GameObject.Find("CustomizationVendor(Clone)"));

    }
}
