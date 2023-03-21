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
    private bool inRange;
    public ThirdPersonMovement movementScriptREF;

    private void Start ()
    {
         movementScriptREF = GetComponent<ThirdPersonMovement>();
         canvas = GameObject.FindGameObjectWithTag("VendorCanvas");
         prompt = GameObject.Find("PromptController");
         prompt.SetActive(false);
         canvas.SetActive (false);
         
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

        if (Input.GetKeyDown(KeyCode.E))
        {
            canvas.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                movementScriptREF.dialogueManager.freezePlayer = true;
                prompt.SetActive(false);
        }

        if (canvas.activeInHierarchy == false && inRange)
            prompt.SetActive(true);
        else  
            prompt.SetActive(false);






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
    }

}
