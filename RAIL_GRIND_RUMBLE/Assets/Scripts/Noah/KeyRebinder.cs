using TMPro; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class KeyRebinder : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private InputActionReference jumpAction = null;
    [SerializeField] private ThirdPersonMovement playerController = null;
    [SerializeField] private TMP_Text bindingDisplayNameText = null;
    [SerializeField] private GameObject startRebindObject = null;
    [SerializeField] private GameObject waitingForInputObject = null;

    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;
    
    public void StartRebinding()
    {
        startRebindObject.SetActive(false);
        waitingForInputObject.SetActive(true);

        jumpAction.action.Disable();
        
        rebindingOperation = jumpAction.action.PerformInteractiveRebinding()
            .WithControlsExcluding("Mouse")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => RebindComplete() )
            .Start();
    }

    private void RebindComplete()
    {
        rebindingOperation.Dispose();
        
        jumpAction.action.Enable();
        
        startRebindObject.SetActive(true);
        waitingForInputObject.SetActive(false);
    }
}
