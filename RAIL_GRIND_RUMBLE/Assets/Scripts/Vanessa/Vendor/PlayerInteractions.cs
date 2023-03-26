using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    private void Start ()
    {
        if (GameObject.Find("CustomizationVendor"))
            ETCCusREF = GameObject.Find("CustomizationVendor").GetComponent<ETCCustomizationVendor>();
        else if (GameObject.Find("CustomizationVendor(Clone)"))
            ETCCusREF = GameObject.Find("CustomizationVendor(Clone)").GetComponent<ETCCustomizationVendor>();
        
        thingsToHide[0] = GameObject.Find("SpeedometerUI");
        thingsToHide[1] = GameObject.Find("CompassMask");
        foreach (GameObject g in thingsToHide) 
        {
            g.SetActive(true);
        }

         movementScriptREF = GetComponent<ThirdPersonMovement>();
         
         
        previewCamera = GameObject.Find("CharacterPreviewBackgr");
         if (previewCamera)
            previewCamera.SetActive(false);

        previewCamItself = GameObject.Find ("CharacterPreviewCamera");
          if (previewCamItself)
            previewCamItself.SetActive(false);

         prompt = GameObject.Find("PromptController");
         if (prompt)
            prompt.SetActive(false);

        canvas = GameObject.Find("VendorCanvas");
        Debug.Log("the canvas is " + canvas);
        if (canvas)
            canvas.SetActive(false);
         
    }
    // Start is called before the first frame update
    private void OnTriggerEnter (Collider col)
    {
        if (col.gameObject.transform.tag == "Vendor")
        {
            //display interact prompt
            inRange = true;
            
        }
    }

    private void OnTriggerExit (Collider col)
    {
        if (col.gameObject.transform.tag == "Vendor")
        {
            //display interact prompt
            inRange = false;
            
        }
    }

    private void Update ()
    {
        // i just need it to work rn
        if (canvas == null)
        {
            if (previewCamera)
                previewCamera.SetActive(false);

            if (prompt)
                prompt.SetActive(false);
            if (previewCamera)
                previewCamItself.SetActive(false);

            return;
        }
            

        if (Input.GetKeyDown(KeyCode.E) &&inRange)
        {
            canvas.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            movementScriptREF.dialogueManager.freezePlayer = true;
            prompt.SetActive(false);
            previewCamItself.SetActive(true);


        }

        if (canvas.activeInHierarchy == false && inRange)
            prompt.SetActive(true);
        else  
            prompt.SetActive(false);

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
            



        //for the hold x to interact sys
        if (isTimer)
        {
            currentHoldXTime += Time.deltaTime;
            Debug.Log("incr timer");
        } else 
            currentHoldXTime = 0;
    }

    /*public void Interact(InputAction.CallbackContext context)
    {
        Debug.Log("theres input");
        if (context.ReadValueAsButton() == true)
        {
            isTimer = true;
            if (currentHoldXTime > holdXTime)
            {
                canvas.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }*/
        public void CloseMenu ()
    {
        canvas.SetActive(false);
        Cursor.visible = false;
        ETCCusREF.ResetOutfitToSaveState();

    }

}
