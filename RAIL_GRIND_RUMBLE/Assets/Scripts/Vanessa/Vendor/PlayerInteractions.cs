using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayerInteractions : MonoBehaviour
{
    
    private GameObject canvas;
    private float holdXTime = 3f;
    private float currentHoldXTime;
    private bool isTimer;
    private GameObject prompt;
    private GameObject previewCamera;
    private bool inRange;
    public ThirdPersonMovement movementScriptREF;
    private GameObject previewCamItself;
    private GameObject [] thingsToHide = new GameObject [2];
    private ETCCustomizationVendor ETCCusREF;

    private GameObject gamepadCur;
    

    Scene m_scene;

    private void Start ()
    {
        //references
        movementScriptREF = GameObject.Find("playerPrefab").GetComponent<ThirdPersonMovement>();

        //gamepadCur = GameObject.Find("GamepadCursor");
        //gamepadCur.SetActive(false);

        if (GameObject.Find("CustomizationVendor"))
            ETCCusREF = GameObject.Find("CustomizationVendor").GetComponent<ETCCustomizationVendor>();
        
        previewCamera = GameObject.Find("CharacterPreviewBackgr");
         if (previewCamera)
            previewCamera.SetActive(false);

        previewCamItself = GameObject.Find ("CharacterPreviewCamera");
          if (previewCamItself)
            previewCamItself.SetActive(false);


        canvas = GameObject.Find("VendorCanvas");
        Debug.Log("the canvas is off now");
        if (canvas)
            canvas.SetActive(false);

        //find overlays
        thingsToHide[0] = GameObject.Find("SpeedometerUI");
        thingsToHide[1] = GameObject.Find("CompassMask");
        foreach (GameObject g in thingsToHide) 
        {
            g.SetActive(true);
        }

        //while in aris house hide hud overlays
        m_scene = SceneManager.GetActiveScene(); 
        if (m_scene.name == "Ari's House")
        {
            foreach (GameObject g in thingsToHide) 
            {
                g.SetActive(false);
            }
        } 
         
    }


    public void OpeningCust(InputAction.CallbackContext context)
    {
        Debug.Log("theres cust input");

        if (context.started)
        {
            //are we in info? okay was the button pressed
            if (InfoScreen.isOpen)
            {
            
               //gamepadCur.SetActive(true);
                //Closes Info Screen if Customization is opened from Info Screen
                if (InfoScreen.isOpen)
                {
                    InfoScreen infoScreen = GameObject.Find("canvasPrefab").GetComponent<InfoScreen>();
                    infoScreen.StartCoroutine(infoScreen.CloseInfoScreen());
                }
                
                //show vendorcanvas
                canvas.SetActive(true);

                //hide overlays
                if (canvas.activeInHierarchy)
                {
                    previewCamera.SetActive(true);
                    foreach (GameObject g in thingsToHide) 
                    {
                        g.SetActive(false);
                    }
                }
                else
                {
                    previewCamera.SetActive(false);
                    foreach (GameObject g in thingsToHide) 
                    {
                        g.SetActive(true);
                    }
                }


                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                movementScriptREF.dialogueManager.freezePlayer = true;
                previewCamItself.SetActive(true);
            }
        }
    }


    public void OpeningCustomizationMK()
    {
        Debug.Log("theres cust input");

            //are we in info? okay was the button pressed
            if (InfoScreen.isOpen)
            {   
                //gamepadCur.SetActive(true);
            
                //Closes Info Screen if Customization is opened from Info Screen
                if (InfoScreen.isOpen)
                {
                    InfoScreen infoScreen = GameObject.Find("canvasPrefab").GetComponent<InfoScreen>();
                    infoScreen.StartCoroutine(infoScreen.CloseInfoScreen());
                }
                
                //show vendorcanvas
                canvas.SetActive(true);

                //hide overlays
                if (canvas.activeInHierarchy)
                {
                    previewCamera.SetActive(true);
                    foreach (GameObject g in thingsToHide) 
                    {
                        g.SetActive(false);
                    }
                }
                else
                {
                    previewCamera.SetActive(false);
                    foreach (GameObject g in thingsToHide) 
                    {
                        g.SetActive(true);
                    }
                }


                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                movementScriptREF.dialogueManager.freezePlayer = true;
                previewCamItself.SetActive(true);
            }
        
    }


        public void CloseMenu ()
    {
        canvas = GameObject.Find("VendorCanvas");
        canvas.SetActive(false);
        Cursor.visible = false;
        ETCCusREF.ResetOutfitToSaveState();
        movementScriptREF.dialogueManager.freezePlayer = false;

         //gamepadCur.SetActive(false);

        //reopen infoscreen
        InfoScreen infoScreen = GameObject.Find("canvasPrefab").GetComponent<InfoScreen>();
        infoScreen.StartCoroutine(infoScreen.OpenInfoScreen());

    }



}
