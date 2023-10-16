using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ControllerMenu : MonoBehaviour
{
    protected bool isMouse;

    protected void Start()
    {
        InputSystem.onActionChange += InputActionChangeCallback;
    }

    protected void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && isMouse)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
        
        if (!isMouse && EventSystem.current.currentSelectedGameObject == null )
        {
            GameObject highestActiveButton = GetHighestActiveButton();
            if (highestActiveButton != null)
            {
                EventSystem.current.SetSelectedGameObject(highestActiveButton);
            }
        }

        if (EventSystem.current.currentSelectedGameObject!= null && !EventSystem.current.currentSelectedGameObject.activeInHierarchy)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    private GameObject GetHighestActiveButton()
    {
        Button[] buttons = FindObjectsOfType<Button>();
        GameObject highestButton = null;
        foreach (var button in buttons)
        {
            if (button.gameObject.activeInHierarchy && (highestButton == null || button.transform.position.y > highestButton.transform.position.y))
            {
                highestButton = button.gameObject;
            }
        }

        if (highestButton != null)
        {
            //Debug.Log("Highest Button is " + highestButton.name);
        }

        return highestButton;
    }

    private void InputActionChangeCallback(object obj, InputActionChange change)
    {
        if (change == InputActionChange.ActionPerformed)
        {
            InputAction receivedInputAction = (InputAction) obj;
            InputDevice lastDevice = receivedInputAction.activeControl.device;

            
            isMouse = /*lastDevice.name.Equals("Keyboard") ||*/ lastDevice.name.Equals("Mouse");

            if (isMouse)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }

            
        }
    }
}