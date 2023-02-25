using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ControllerRebinder : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private InputActionReference inputActionReference;

    [SerializeField] private bool excludeMouse = true;
    [Range(0, 10)] 
    [SerializeField] private int selectedBinding;

    [SerializeField] private InputBinding.DisplayStringOptions displayStringOptions;

    [Header("Binding Info - DO NOT EDIT")]
    [SerializeField] private InputBinding inputBinding;

    private int bindingIndex;

    private string actionName;

    private void OnValidate()
    {
        GetBindingInfo();
        
    }

    private void GetBindingInfo()
    {
        if (inputActionReference.action != null)
            actionName = inputActionReference.action.name;

        if (inputActionReference.action.bindings.Count > selectedBinding)
        {
            inputBinding = inputActionReference.action.bindings[selectedBinding];
            bindingIndex = selectedBinding;
        }

    }
}
