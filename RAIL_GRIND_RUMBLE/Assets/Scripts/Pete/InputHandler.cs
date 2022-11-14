//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Scripts/Pete/InputHandler.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @InputHandler : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputHandler()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputHandler"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""829e7416-919b-4c1d-af89-4f045068756e"",
            ""actions"": [
                {
                    ""name"": ""Grapple Switch"",
                    ""type"": ""Button"",
                    ""id"": ""6c689b1b-bf46-495c-9d9a-354fd2a479d2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Grapple Release"",
                    ""type"": ""Button"",
                    ""id"": ""e065dc05-4bd1-4b79-b2d1-1143afcb780b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""59e214cf-aa86-43f1-a78e-b2d9a67ee410"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""ab8367a2-9ba5-4a58-a32c-51b5bb5daab0"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Grapple Pull"",
                    ""type"": ""Button"",
                    ""id"": ""9bb6d427-a3a1-47f5-8ebf-5a8a5f8e0833"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Pause Game"",
                    ""type"": ""Button"",
                    ""id"": ""10810a43-77a0-479a-937e-59746fb1ae39"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""219f795e-1e07-4249-abb8-0912b6fa90c9"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Switch Mode"",
                    ""type"": ""Button"",
                    ""id"": ""1372210f-a333-48dd-8076-6df5d453fc7b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""a7a7501f-250d-4dc0-ba45-5c02fc46adfa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Graffiti"",
                    ""type"": ""Button"",
                    ""id"": ""24a71db0-cb8e-4c66-a963-45bfa06ff53c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""bc4dd424-7b15-4898-8348-2a5d18d0fb49"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Grapple Switch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""43ff0014-8b63-4127-ac22-baac6a59d312"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Grapple Switch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d698645a-0db6-48c9-a5be-abc2f832dccf"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Grapple Release"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9e58518f-480d-4d6b-88ac-b3759ae10a8b"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Grapple Release"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7da6ec1e-7ae3-41fa-8c70-4fc3a3da7b98"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c2423efe-ebd2-4298-a9a8-3960ca5eed1b"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6e8b769b-8dfe-4f4c-993b-c52080959925"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""50ac7999-389e-4ef5-973c-60cb02dcc9d1"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""76f8eb9e-a614-4c97-b0cc-6004d4e3d7a8"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""40ed8c63-c2e3-4033-a90e-863c03660321"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""64b7b42e-811a-4394-a597-625b0737f0dc"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""ab8c093f-07c7-43e9-b4b5-52fa8027ea58"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""c6551683-a54d-4d12-9515-888a4f553bb7"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Grapple Pull"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""65cfb76e-2858-4bdb-a7b4-50c4deb25b3c"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Grapple Pull"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c591ae14-7422-4258-bb52-39482322c0dc"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Grapple Pull"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8532e611-0f37-449b-bf87-195a5743f70c"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause Game"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""03168051-8b0b-4cfa-98f2-77a6c22cf7ca"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause Game"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""171e4304-11d5-45de-a7eb-d850e507db30"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": ""ScaleVector2(x=15,y=15),StickDeadzone"",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c1f2493e-70b8-4116-a181-f40421d547ab"",
                    ""path"": ""<Gamepad>/leftStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Switch Mode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fe8e876b-27ae-4625-ade0-756e61f9619a"",
                    ""path"": ""<Keyboard>/#(O)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Switch Mode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""72ebcf71-b5c1-41e8-8777-1a13ffd446f8"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0313dcce-2fa2-494a-8098-1b94bd338c6b"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4073bcc7-beb6-4086-a57c-9d97e1aa8245"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Graffiti"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0bb78d55-d953-4e10-9ed4-7d84f63dbf9a"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Graffiti"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d0ce54a1-0094-431a-b456-d19ea63c1bee"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Graffiti"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1e9d92bc-dc63-47a7-8585-f48b148bdb22"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Graffiti"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6222c16e-c418-43f0-8dd7-62957f0294d5"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Graffiti"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_GrappleSwitch = m_Player.FindAction("Grapple Switch", throwIfNotFound: true);
        m_Player_GrappleRelease = m_Player.FindAction("Grapple Release", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_GrapplePull = m_Player.FindAction("Grapple Pull", throwIfNotFound: true);
        m_Player_PauseGame = m_Player.FindAction("Pause Game", throwIfNotFound: true);
        m_Player_Look = m_Player.FindAction("Look", throwIfNotFound: true);
        m_Player_SwitchMode = m_Player.FindAction("Switch Mode", throwIfNotFound: true);
        m_Player_Attack = m_Player.FindAction("Attack", throwIfNotFound: true);
        m_Player_Graffiti = m_Player.FindAction("Graffiti", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_GrappleSwitch;
    private readonly InputAction m_Player_GrappleRelease;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_GrapplePull;
    private readonly InputAction m_Player_PauseGame;
    private readonly InputAction m_Player_Look;
    private readonly InputAction m_Player_SwitchMode;
    private readonly InputAction m_Player_Attack;
    private readonly InputAction m_Player_Graffiti;
    public struct PlayerActions
    {
        private @InputHandler m_Wrapper;
        public PlayerActions(@InputHandler wrapper) { m_Wrapper = wrapper; }
        public InputAction @GrappleSwitch => m_Wrapper.m_Player_GrappleSwitch;
        public InputAction @GrappleRelease => m_Wrapper.m_Player_GrappleRelease;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @GrapplePull => m_Wrapper.m_Player_GrapplePull;
        public InputAction @PauseGame => m_Wrapper.m_Player_PauseGame;
        public InputAction @Look => m_Wrapper.m_Player_Look;
        public InputAction @SwitchMode => m_Wrapper.m_Player_SwitchMode;
        public InputAction @Attack => m_Wrapper.m_Player_Attack;
        public InputAction @Graffiti => m_Wrapper.m_Player_Graffiti;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @GrappleSwitch.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGrappleSwitch;
                @GrappleSwitch.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGrappleSwitch;
                @GrappleSwitch.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGrappleSwitch;
                @GrappleRelease.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGrappleRelease;
                @GrappleRelease.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGrappleRelease;
                @GrappleRelease.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGrappleRelease;
                @Jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Move.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @GrapplePull.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGrapplePull;
                @GrapplePull.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGrapplePull;
                @GrapplePull.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGrapplePull;
                @PauseGame.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPauseGame;
                @PauseGame.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPauseGame;
                @PauseGame.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnPauseGame;
                @Look.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                @SwitchMode.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwitchMode;
                @SwitchMode.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwitchMode;
                @SwitchMode.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwitchMode;
                @Attack.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAttack;
                @Graffiti.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGraffiti;
                @Graffiti.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGraffiti;
                @Graffiti.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnGraffiti;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @GrappleSwitch.started += instance.OnGrappleSwitch;
                @GrappleSwitch.performed += instance.OnGrappleSwitch;
                @GrappleSwitch.canceled += instance.OnGrappleSwitch;
                @GrappleRelease.started += instance.OnGrappleRelease;
                @GrappleRelease.performed += instance.OnGrappleRelease;
                @GrappleRelease.canceled += instance.OnGrappleRelease;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @GrapplePull.started += instance.OnGrapplePull;
                @GrapplePull.performed += instance.OnGrapplePull;
                @GrapplePull.canceled += instance.OnGrapplePull;
                @PauseGame.started += instance.OnPauseGame;
                @PauseGame.performed += instance.OnPauseGame;
                @PauseGame.canceled += instance.OnPauseGame;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @SwitchMode.started += instance.OnSwitchMode;
                @SwitchMode.performed += instance.OnSwitchMode;
                @SwitchMode.canceled += instance.OnSwitchMode;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @Graffiti.started += instance.OnGraffiti;
                @Graffiti.performed += instance.OnGraffiti;
                @Graffiti.canceled += instance.OnGraffiti;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnGrappleSwitch(InputAction.CallbackContext context);
        void OnGrappleRelease(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnGrapplePull(InputAction.CallbackContext context);
        void OnPauseGame(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnSwitchMode(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnGraffiti(InputAction.CallbackContext context);
    }
}
