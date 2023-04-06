using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class ControllerRebinder : MonoBehaviour
{
    public List<GamepadIcons> xbox = new List<GamepadIcons>(); 

    //Jump
    [SerializeField] private InputActionReference jumpAction = null;
    [SerializeField] private TMP_Text jumpBindingDisplayNameText = null;
    [SerializeField] private GameObject startJumpRebindObject = null;
    [SerializeField] private GameObject waitingForJumpInputObject = null;
    [SerializeField] private Image jumpBindingDisplayIcon = null; 

    //MoveForward
   /* [SerializeField] private InputActionReference moveForwardAction = null;
    [SerializeField] private TMP_Text moveForwardBindingDisplayNameText = null;
    [SerializeField] private GameObject startMoveForwardRebindObject = null;
    [SerializeField] private GameObject waitingForMoveForwardInputObject = null;

    //MoveBackward
    [SerializeField] private InputActionReference moveBackAction = null;
    [SerializeField] private TMP_Text moveBackBindingDisplayNameText = null;
    [SerializeField] private GameObject startMoveBackRebindObject = null;
    [SerializeField] private GameObject waitingForMoveBackInputObject = null;

    //MoveLeft
    [SerializeField] private InputActionReference moveLeftAction = null;
    [SerializeField] private TMP_Text moveLeftBindingDisplayNameText = null;
    [SerializeField] private GameObject startMoveLeftRebindObject = null;
    [SerializeField] private GameObject waitingForMoveLeftInputObject = null;

    //MoveRight
    [SerializeField] private InputActionReference moveRightAction = null;
    [SerializeField] private TMP_Text moveRightBindingDisplayNameText = null;
    [SerializeField] private GameObject startMoveRightRebindObject = null;
    [SerializeField] private GameObject waitingForMoveRightInputObject = null;
    */

    //GrapplePull
    [SerializeField] private InputActionReference grapplePullAction = null;
    [SerializeField] private TMP_Text grapplePullBindingDisplayNameText = null;
    [SerializeField] private GameObject startGrapplePullRebindObject = null;
    [SerializeField] private GameObject waitingForGrapplePullInputObject = null;
    [SerializeField] private Image grapplePullBindingDisplayIcon = null; 

    //GrappleSwitch
    [SerializeField] private InputActionReference grappleSwitchAction = null;
    [SerializeField] private TMP_Text grappleSwitchBindingDisplayNameText = null;
    [SerializeField] private GameObject startGrappleSwitchRebindObject = null;
    [SerializeField] private GameObject waitingForGrappleSwitchInputObject = null;
    [SerializeField] private Image grappleSwitchBindingDisplayIcon = null; 

    //GrappleRelease
    [SerializeField] private InputActionReference noAimGrappleAction = null;
    [SerializeField] private TMP_Text noAimGrappleBindingDisplayNameText = null;
    [SerializeField] private GameObject startNoAimGrappleRebindObject = null;
    [SerializeField] private GameObject waitingForNoAimGrappleInputObject = null;
    [SerializeField] private Image noAimGrappleBindingDisplayIcon = null; 

    //PauseGame
    [SerializeField] private InputActionReference pauseAction = null;
    [SerializeField] private TMP_Text pauseBindingDisplayNameText = null;
    [SerializeField] private GameObject startPauseRebindObject = null;
    [SerializeField] private GameObject waitingForPauseInputObject = null;
    [SerializeField] private Image pauseBindingDisplayIcon = null; 

    //SwitchMode
    [SerializeField] private InputActionReference switchAction = null;
    [SerializeField] private TMP_Text switchBindingDisplayNameText = null;
    [SerializeField] private GameObject startSwitchRebindObject = null;
    [SerializeField] private GameObject waitingForSwitchInputObject = null;
    [SerializeField] private Image switchBindingDisplayIcon = null; 

    //HeavyAttack
    [SerializeField] private InputActionReference heavyAction = null;
    [SerializeField] private TMP_Text heavyBindingDisplayNameText = null;
    [SerializeField] private GameObject startHeavyRebindObject = null;
    [SerializeField] private GameObject waitingForHeavyInputObject = null;
    [SerializeField] private Image heavyBindingDisplayIcon = null; 

    //LightAttack
    [SerializeField] private InputActionReference lightAction = null;
    [SerializeField] private TMP_Text lightBindingDisplayNameText = null;
    [SerializeField] private GameObject startLightRebindObject = null;
    [SerializeField] private GameObject waitingForLightInputObject = null;
    [SerializeField] private Image lightBindingDisplayIcon = null; 
    
    //GraffitiUp
    [SerializeField] private InputActionReference graffitiUpAction = null;
    [SerializeField] private TMP_Text graffitiUpBindingDisplayNameText = null;
    [SerializeField] private GameObject startGraffitiUpRebindObject = null;
    [SerializeField] private GameObject waitingForGraffitiUpInputObject = null;
    [SerializeField] private Image graffitiUpBindingDisplayIcon = null; 

    //GraffitiDown
    [SerializeField] private InputActionReference graffitiDownAction = null;
    [SerializeField] private TMP_Text graffitiDownBindingDisplayNameText = null;
    [SerializeField] private GameObject startGraffitiDownRebindObject = null;
    [SerializeField] private GameObject waitingForGraffitiDownInputObject = null;
    [SerializeField] private Image graffitiDownBindingDisplayIcon = null; 

    //GraffitiLeft
    [SerializeField] private InputActionReference graffitiLeftAction = null;
    [SerializeField] private TMP_Text graffitiLeftBindingDisplayNameText = null;
    [SerializeField] private GameObject startGraffitiLeftRebindObject = null;
    [SerializeField] private GameObject waitingForGraffitiLeftInputObject = null;
    [SerializeField] private Image graffitiLeftBindingDisplayIcon = null; 

    //GraffitiRight
    [SerializeField] private InputActionReference graffitiRightAction = null;
    [SerializeField] private TMP_Text graffitiRightBindingDisplayNameText = null;
    [SerializeField] private GameObject startGraffitiRightRebindObject = null;
    [SerializeField] private GameObject waitingForGraffitiRightInputObject = null;
    [SerializeField] private Image graffitiRightBindingDisplayIcon = null; 




    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;

    void Start()
    {
         //JumpRebindingDisplay
        jumpBindingDisplayNameText.text = InputControlPath.ToHumanReadableString(
            jumpAction.action.bindings[1].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);
        
       //GrapplePullDisplay
        grapplePullBindingDisplayNameText.text = InputControlPath.ToHumanReadableString(
            grapplePullAction.action.bindings[2].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);
        
        //GrappleSwitchDisplay
        grappleSwitchBindingDisplayNameText.text = InputControlPath.ToHumanReadableString(
            grappleSwitchAction.action.bindings[0].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);
        
        //NoAimGrappleDisplay
        noAimGrappleBindingDisplayNameText.text = InputControlPath.ToHumanReadableString(
            noAimGrappleAction.action.bindings[1].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);
        
        //PauseDisplay
        pauseBindingDisplayNameText.text = InputControlPath.ToHumanReadableString(
            pauseAction.action.bindings[0].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);
        
        //SwitchDisplay
        switchBindingDisplayNameText.text = InputControlPath.ToHumanReadableString(
            switchAction.action.bindings[1].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);
        
        //HeavyDisplay
        heavyBindingDisplayNameText.text = InputControlPath.ToHumanReadableString(
            heavyAction.action.bindings[1].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);
        
        //LightDisplay
        lightBindingDisplayNameText.text = InputControlPath.ToHumanReadableString(
            lightAction.action.bindings[1].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);
        
        //GraffitiUpDisplay
        graffitiUpBindingDisplayNameText.text = InputControlPath.ToHumanReadableString(
            graffitiUpAction.action.bindings[1].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);
        
        //GraffitiDownDisplay
        graffitiDownBindingDisplayNameText.text = InputControlPath.ToHumanReadableString(
            graffitiDownAction.action.bindings[1].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);
        
        //GraffitiLeftDisplay
        graffitiLeftBindingDisplayNameText.text = InputControlPath.ToHumanReadableString(
            graffitiLeftAction.action.bindings[1].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);
        
        //GraffitiRightDisplay
        graffitiRightBindingDisplayNameText.text = InputControlPath.ToHumanReadableString(
            graffitiRightAction.action.bindings[1].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);
        
        ReplaceWithIcon();
        
    }

    //Jump Rebind
    public void StartRebindingJump()
    {
        startJumpRebindObject.SetActive(false);
        waitingForJumpInputObject.SetActive(true);

        jumpAction.action.Disable();

        rebindingOperation = jumpAction.action.PerformInteractiveRebinding()
            .WithControlsExcluding("<Mouse>")
            .WithControlsExcluding("<Pointer>/position")
            .WithControlsExcluding("<Keyboard>")
            .WithTargetBinding(1)
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => JumpRebindComplete())
            .Start();
    }

    private void JumpRebindComplete()
    {

        int bindingIndex = jumpAction.action.GetBindingIndexForControl(jumpAction.action.controls[0]);

        jumpBindingDisplayNameText.text = InputControlPath.ToHumanReadableString(
            jumpAction.action.bindings[1].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);
        
        ReplaceWithIcon();
        
        rebindingOperation.Dispose();

        jumpAction.action.Enable();

        startJumpRebindObject.SetActive(true);
        waitingForJumpInputObject.SetActive(false);
    }
    /*   DOESN'T WORK 
    //MoveForwardRebind
    public void StartRebindingMoveForward()
    {
        startMoveForwardRebindObject.SetActive(false);
        waitingForMoveForwardInputObject.SetActive(true);

        moveForwardAction.action.Disable();
        
        rebindingOperation = moveForwardAction.action.PerformInteractiveRebinding()
            .WithControlsExcluding("<Mouse>/press")
            .WithControlsExcluding("<Pointer>/position")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => MoveForwardRebindComplete() )
            .Start();
    }

   
        
    
    private void MoveForwardRebindComplete()
    {
       
        int bindingIndex = moveForwardAction.action.GetBindingIndexForControl(moveForwardAction.action.controls[0]);
        
        moveForwardBindingDisplayNameText.text = InputControlPath.ToHumanReadableString(
            moveForwardAction.action.bindings[bindingIndex].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);
        
        rebindingOperation.Dispose();
        
        moveForwardAction.action.Enable();
        
        startMoveForwardRebindObject.SetActive(true);
        waitingForMoveForwardInputObject.SetActive(false);
    }
    
    //MoveBackwardRebind
    public void StartRebindingMoveBackward()
    {
        startMoveBackRebindObject.SetActive(false);
        waitingForMoveBackInputObject.SetActive(true);

        moveBackAction.action.Disable();
        
        rebindingOperation = moveBackAction.action.PerformInteractiveRebinding()
            .WithControlsExcluding("<Mouse>/press")
            .WithControlsExcluding("<Pointer>/position")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => MoveBackwardRebindComplete() )
            .Start();
    }

    private void MoveBackwardRebindComplete()
    {
       
        int bindingIndex = moveBackAction.action.GetBindingIndexForControl(moveBackAction.action.controls[0]);
        
        moveBackBindingDisplayNameText.text = InputControlPath.ToHumanReadableString(
            moveBackAction.action.bindings[bindingIndex].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);
        
        rebindingOperation.Dispose();
        
        moveBackAction.action.Enable();
        
        startMoveBackRebindObject.SetActive(true);
        waitingForMoveBackInputObject.SetActive(false);
    }*/

    public void StartRebindingGrapplePull()
    {
        startGrapplePullRebindObject.SetActive(false);
        waitingForGrapplePullInputObject.SetActive(true);

        grapplePullAction.action.Disable();

        rebindingOperation = grapplePullAction.action.PerformInteractiveRebinding()
            .WithControlsExcluding("<Mouse>")
            .WithControlsExcluding("<Pointer>/position")
            .WithControlsExcluding("<Keyboard>")
            .WithTargetBinding(2)
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => GrapplePullRebindComplete())
            .Start();
    }

    private void GrapplePullRebindComplete()
    {

        int bindingIndex = grapplePullAction.action.GetBindingIndexForControl(grapplePullAction.action.controls[0]);

        grapplePullBindingDisplayNameText.text = InputControlPath.ToHumanReadableString(
            grapplePullAction.action.bindings[2].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);

        ReplaceWithIcon();
        
        rebindingOperation.Dispose();

        grapplePullAction.action.Enable();

        startGrapplePullRebindObject.SetActive(true);
        waitingForGrapplePullInputObject.SetActive(false);
    }

    public void StartRebindingGrappleSwitch()
    {
        startGrappleSwitchRebindObject.SetActive(false);
        waitingForGrappleSwitchInputObject.SetActive(true);

        grappleSwitchAction.action.Disable();

        rebindingOperation = grappleSwitchAction.action.PerformInteractiveRebinding()
            .WithControlsExcluding("<Mouse>")
            .WithControlsExcluding("<Pointer>/position")
            .WithControlsExcluding("<Keyboard>")
            .WithTargetBinding(0)
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => GrappleSwitchRebindComplete())
            .Start();
    }

    private void GrappleSwitchRebindComplete()
    {

        int bindingIndex = grappleSwitchAction.action.GetBindingIndexForControl(grappleSwitchAction.action.controls[0]);

        grappleSwitchBindingDisplayNameText.text = InputControlPath.ToHumanReadableString(
            grappleSwitchAction.action.bindings[0].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);
        
        ReplaceWithIcon();

        rebindingOperation.Dispose();

        grappleSwitchAction.action.Enable();

        startGrappleSwitchRebindObject.SetActive(true);
        waitingForGrappleSwitchInputObject.SetActive(false);
    }

    public void StartRebindingNoAimGrapple()
    {
        startNoAimGrappleRebindObject.SetActive(false);
        waitingForNoAimGrappleInputObject.SetActive(true);

        noAimGrappleAction.action.Disable();

        rebindingOperation = noAimGrappleAction.action.PerformInteractiveRebinding()
            .WithControlsExcluding("<Mouse>")
            .WithControlsExcluding("<Pointer>/position")
            .WithControlsExcluding("<Keyboard>")
            .WithTargetBinding(1)
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => NoAimGrappleRebindComplete())
            .Start();
    }

    private void NoAimGrappleRebindComplete()
    {

        int bindingIndex = noAimGrappleAction.action.GetBindingIndexForControl(noAimGrappleAction.action.controls[0]);

        noAimGrappleBindingDisplayNameText.text = InputControlPath.ToHumanReadableString(
            noAimGrappleAction.action.bindings[1].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);
        
        ReplaceWithIcon();

        rebindingOperation.Dispose();

        noAimGrappleAction.action.Enable();

        startNoAimGrappleRebindObject.SetActive(true);
        waitingForNoAimGrappleInputObject.SetActive(false);
    }

    public void StartRebindingPause()
    {
        startPauseRebindObject.SetActive(false);
        waitingForPauseInputObject.SetActive(true);

        pauseAction.action.Disable();

        rebindingOperation = pauseAction.action.PerformInteractiveRebinding()
            .WithControlsExcluding("<Mouse>")
            .WithControlsExcluding("<Pointer>/position")
            .WithControlsExcluding("<Keyboard>")
            .WithControlsExcluding("<Gamepad>/buttonSouth")
            // .WithBindingGroup("KeyboardMouse")
            .WithTargetBinding(0)
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => PauseRebindComplete())
            .Start();
    }

    private void PauseRebindComplete()
    {

        int bindingIndex = pauseAction.action.GetBindingIndexForControl(pauseAction.action.controls[0]);

        pauseBindingDisplayNameText.text = InputControlPath.ToHumanReadableString(
            pauseAction.action.bindings[0].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);
        
        ReplaceWithIcon();

        rebindingOperation.Dispose();

        pauseAction.action.Enable();

        startPauseRebindObject.SetActive(true);
        waitingForPauseInputObject.SetActive(false);
    }

    public void StartRebindingSwitch()
    {
        startSwitchRebindObject.SetActive(false);
        waitingForSwitchInputObject.SetActive(true);

        switchAction.action.Disable();

        rebindingOperation = switchAction.action.PerformInteractiveRebinding()
           .WithControlsExcluding("<Mouse>")
            .WithControlsExcluding("<Pointer>/position")
            .WithControlsExcluding("<Keyboard>")
            .WithTargetBinding(1)
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => SwitchRebindComplete())
            .Start();
    }

    private void SwitchRebindComplete()
    {

        int bindingIndex = switchAction.action.GetBindingIndexForControl(switchAction.action.controls[0]);

        switchBindingDisplayNameText.text = InputControlPath.ToHumanReadableString(
            switchAction.action.bindings[1].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);

        ReplaceWithIcon();
        
        rebindingOperation.Dispose();

        switchAction.action.Enable();

        startSwitchRebindObject.SetActive(true);
        waitingForSwitchInputObject.SetActive(false);
    }

    public void StartRebindingHeavy()
    {
        startHeavyRebindObject.SetActive(false);
        waitingForHeavyInputObject.SetActive(true);

        heavyAction.action.Disable();

        rebindingOperation = heavyAction.action.PerformInteractiveRebinding()
            .WithControlsExcluding("<Mouse>")
            .WithControlsExcluding("<Pointer>/position")
            .WithControlsExcluding("<Keyboard>")
            .WithTargetBinding(1)
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => HeavyRebindComplete())
            .Start();
    }

    private void HeavyRebindComplete()
    {

        int bindingIndex = heavyAction.action.GetBindingIndexForControl(heavyAction.action.controls[0]);

        heavyBindingDisplayNameText.text = InputControlPath.ToHumanReadableString(
            heavyAction.action.bindings[1].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);
        
        ReplaceWithIcon();

        rebindingOperation.Dispose();

        heavyAction.action.Enable();

        startHeavyRebindObject.SetActive(true);
        waitingForHeavyInputObject.SetActive(false);
    }

    public void StartRebindingLight()
    {
        startLightRebindObject.SetActive(false);
        waitingForLightInputObject.SetActive(true);

        lightAction.action.Disable();

        rebindingOperation = lightAction.action.PerformInteractiveRebinding()
            .WithControlsExcluding("<Mouse>")
            .WithControlsExcluding("<Pointer>/position")
            .WithControlsExcluding("<Keyboard>")
            .WithTargetBinding(1)
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => LightRebindComplete())
            .Start();
    }

    private void LightRebindComplete()
    {

        int bindingIndex = lightAction.action.GetBindingIndexForControl(lightAction.action.controls[0]);

        lightBindingDisplayNameText.text = InputControlPath.ToHumanReadableString(
            lightAction.action.bindings[1].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);
        
        ReplaceWithIcon();

        rebindingOperation.Dispose();

        lightAction.action.Enable();

        startLightRebindObject.SetActive(true);
        waitingForLightInputObject.SetActive(false);
    }

    public void StartRebindingGraffitiUp()
    {
        startGraffitiUpRebindObject.SetActive(false);
        waitingForGraffitiUpInputObject.SetActive(true);

        graffitiUpAction.action.Disable();

        rebindingOperation = graffitiUpAction.action.PerformInteractiveRebinding()
            .WithControlsExcluding("<Mouse>")
            .WithControlsExcluding("<Pointer>/position")
            .WithControlsExcluding("<Keyboard>")
            .WithTargetBinding(1)
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => GraffitiUpRebindComplete())
            .Start();
    }

    private void GraffitiUpRebindComplete()
    {

        int bindingIndex = graffitiUpAction.action.GetBindingIndexForControl(graffitiUpAction.action.controls[0]);

        graffitiUpBindingDisplayNameText.text = InputControlPath.ToHumanReadableString(
            graffitiUpAction.action.bindings[1].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);
        
        ReplaceWithIcon();

        rebindingOperation.Dispose();

        graffitiUpAction.action.Enable();

        startGraffitiUpRebindObject.SetActive(true);
        waitingForGraffitiUpInputObject.SetActive(false);
    }

    public void StartRebindingGraffitiDown()
    {
        startGraffitiDownRebindObject.SetActive(false);
        waitingForGraffitiDownInputObject.SetActive(true);

        graffitiDownAction.action.Disable();

        rebindingOperation = graffitiDownAction.action.PerformInteractiveRebinding()
            .WithControlsExcluding("<Mouse>")
            .WithControlsExcluding("<Pointer>/position")
            .WithControlsExcluding("<Keyboard>")
            .WithTargetBinding(1)
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => GraffitiDownRebindComplete())
            .Start();
    }

    private void GraffitiDownRebindComplete()
    {

        int bindingIndex = graffitiDownAction.action.GetBindingIndexForControl(graffitiDownAction.action.controls[0]);

        graffitiDownBindingDisplayNameText.text = InputControlPath.ToHumanReadableString(
            graffitiDownAction.action.bindings[1].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);
        
        ReplaceWithIcon();

        rebindingOperation.Dispose();

        graffitiDownAction.action.Enable();

        startGraffitiDownRebindObject.SetActive(true);
        waitingForGraffitiDownInputObject.SetActive(false);
    }

    public void StartRebindingGraffitiLeft()
    {
        startGraffitiLeftRebindObject.SetActive(false);
        waitingForGraffitiLeftInputObject.SetActive(true);

        graffitiLeftAction.action.Disable();

        rebindingOperation = graffitiLeftAction.action.PerformInteractiveRebinding()
            .WithControlsExcluding("<Mouse>")
            .WithControlsExcluding("<Pointer>/position")
            .WithControlsExcluding("<Keyboard>")
            .WithTargetBinding(1)
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => GraffitiLeftRebindComplete())
            .Start();
    }

    private void GraffitiLeftRebindComplete()
    {

        int bindingIndex = graffitiLeftAction.action.GetBindingIndexForControl(graffitiLeftAction.action.controls[0]);

        graffitiLeftBindingDisplayNameText.text = InputControlPath.ToHumanReadableString(
            graffitiLeftAction.action.bindings[1].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);
        
        ReplaceWithIcon();

        rebindingOperation.Dispose();

        graffitiLeftAction.action.Enable();

        startGraffitiLeftRebindObject.SetActive(true);
        waitingForGraffitiLeftInputObject.SetActive(false);
    }

    public void StartRebindingGraffitiRight()
    {
        startGraffitiRightRebindObject.SetActive(false);
        waitingForGraffitiRightInputObject.SetActive(true);

        graffitiRightAction.action.Disable();

        rebindingOperation = graffitiRightAction.action.PerformInteractiveRebinding()
            .WithControlsExcluding("<Mouse>")
            .WithControlsExcluding("<Pointer>/position")
            .WithControlsExcluding("<Keyboard>")
            .WithTargetBinding(1)
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => GraffitiRightRebindComplete())
            .Start();
    }

    private void GraffitiRightRebindComplete()
    {

        int bindingIndex = graffitiRightAction.action.GetBindingIndexForControl(graffitiRightAction.action.controls[0]);

        graffitiRightBindingDisplayNameText.text = InputControlPath.ToHumanReadableString(
            graffitiRightAction.action.bindings[1].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);
        
        ReplaceWithIcon();

        rebindingOperation.Dispose();

        graffitiRightAction.action.Enable();

        startGraffitiRightRebindObject.SetActive(true);
        waitingForGraffitiRightInputObject.SetActive(false);
    }

    private void ReplaceWithIcon()
    {
        bool foundIcon = false;
        for(int i = 0; i < xbox.Count; i++)
        {
            if (xbox[i].buttonName == jumpBindingDisplayNameText.text)
            {
                jumpBindingDisplayIcon.sprite = xbox[i].gamepadIcon;
                foundIcon = true;
            }
            
            if (xbox[i].buttonName == grapplePullBindingDisplayNameText.text)
            {
                grapplePullBindingDisplayIcon.sprite = xbox[i].gamepadIcon;
                foundIcon = true;
            }
            
            if (xbox[i].buttonName == grappleSwitchBindingDisplayNameText.text)
            {
                grappleSwitchBindingDisplayIcon.sprite = xbox[i].gamepadIcon;
                foundIcon = true;
            }
            
            if (xbox[i].buttonName == noAimGrappleBindingDisplayNameText.text)
            {
                noAimGrappleBindingDisplayIcon.sprite = xbox[i].gamepadIcon;
                foundIcon = true;
            }
            
            if (xbox[i].buttonName == pauseBindingDisplayNameText.text)
            {
                pauseBindingDisplayIcon.sprite = xbox[i].gamepadIcon;
                foundIcon = true;
            }
            
            if (xbox[i].buttonName == switchBindingDisplayNameText.text)
            {
                switchBindingDisplayIcon.sprite = xbox[i].gamepadIcon;
                foundIcon = true;
            }
            
            if (xbox[i].buttonName == heavyBindingDisplayNameText.text)
            {
                heavyBindingDisplayIcon.sprite = xbox[i].gamepadIcon;
                foundIcon = true;
            }
            
            if (xbox[i].buttonName == lightBindingDisplayNameText.text)
            {
                lightBindingDisplayIcon.sprite = xbox[i].gamepadIcon;
                foundIcon = true;
            }
            
            if (xbox[i].buttonName == graffitiUpBindingDisplayNameText.text)
            {
                graffitiUpBindingDisplayIcon.sprite = xbox[i].gamepadIcon;
                foundIcon = true;
            }
            
            if (xbox[i].buttonName == graffitiDownBindingDisplayNameText.text)
            {
                graffitiDownBindingDisplayIcon.sprite = xbox[i].gamepadIcon;
                foundIcon = true;
            }
            
            if (xbox[i].buttonName == graffitiLeftBindingDisplayNameText.text)
            {
                graffitiLeftBindingDisplayIcon.sprite = xbox[i].gamepadIcon;
                foundIcon = true;
            }
            
            if (xbox[i].buttonName == graffitiRightBindingDisplayNameText.text)
            {
                graffitiRightBindingDisplayIcon.sprite = xbox[i].gamepadIcon;
                foundIcon = true;
            }
            
            
        }

        if(!foundIcon)
        {
            
        }
    }
}

[System.Serializable]
public class GamepadIcons
{
    public string buttonName;
    public Sprite gamepadIcon;
}


