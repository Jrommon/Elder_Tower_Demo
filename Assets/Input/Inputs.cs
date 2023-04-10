//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Input/Inputs.inputactions
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

public partial class @Inputs : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Inputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Inputs"",
    ""maps"": [
        {
            ""name"": ""Game"",
            ""id"": ""305aab9d-768c-4ba7-8efd-e17fe173d3ec"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Button"",
                    ""id"": ""c53cc6fe-c606-4023-bdfd-317979ccc3a9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""d88bf686-eb9d-49ac-9b11-28e7952eabe3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""LightAttack"",
                    ""type"": ""Button"",
                    ""id"": ""9ff1b630-16bd-43a8-8139-cd1d1fe35627"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""HeavyAttack"",
                    ""type"": ""Button"",
                    ""id"": ""46b271cd-802a-4d96-98d0-edc3336f16b1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SecondaryAttack"",
                    ""type"": ""Button"",
                    ""id"": ""e46cfb00-9da0-44b2-88e4-d7fa6fa8d7c8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SwitchAttack"",
                    ""type"": ""Button"",
                    ""id"": ""d8a0ec72-7758-4510-a941-ac085ecf6378"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""5526c526-73c1-491d-9a07-756f22866ff4"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Negative"",
                    ""id"": ""9139c31b-6e17-4337-a091-ff7aac21e876"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Positive"",
                    ""id"": ""d474fd66-5d66-4936-84bc-323beb6cd16f"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""9f63b9e8-9312-4728-8d26-c85d222ff8e2"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0ee11028-d719-44ee-9f89-e47f969b3139"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LightAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dc548b3b-b674-424a-a84d-ab7b28f49b1c"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HeavyAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""986b88a8-ff09-4035-8ae1-7848678b8822"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SecondaryAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4a8f3f88-9310-4338-9829-8c1636c5573a"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Game
        m_Game = asset.FindActionMap("Game", throwIfNotFound: true);
        m_Game_Movement = m_Game.FindAction("Movement", throwIfNotFound: true);
        m_Game_Jump = m_Game.FindAction("Jump", throwIfNotFound: true);
        m_Game_LightAttack = m_Game.FindAction("LightAttack", throwIfNotFound: true);
        m_Game_HeavyAttack = m_Game.FindAction("HeavyAttack", throwIfNotFound: true);
        m_Game_SecondaryAttack = m_Game.FindAction("SecondaryAttack", throwIfNotFound: true);
        m_Game_SwitchAttack = m_Game.FindAction("SwitchAttack", throwIfNotFound: true);
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

    // Game
    private readonly InputActionMap m_Game;
    private IGameActions m_GameActionsCallbackInterface;
    private readonly InputAction m_Game_Movement;
    private readonly InputAction m_Game_Jump;
    private readonly InputAction m_Game_LightAttack;
    private readonly InputAction m_Game_HeavyAttack;
    private readonly InputAction m_Game_SecondaryAttack;
    private readonly InputAction m_Game_SwitchAttack;
    public struct GameActions
    {
        private @Inputs m_Wrapper;
        public GameActions(@Inputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Game_Movement;
        public InputAction @Jump => m_Wrapper.m_Game_Jump;
        public InputAction @LightAttack => m_Wrapper.m_Game_LightAttack;
        public InputAction @HeavyAttack => m_Wrapper.m_Game_HeavyAttack;
        public InputAction @SecondaryAttack => m_Wrapper.m_Game_SecondaryAttack;
        public InputAction @SwitchAttack => m_Wrapper.m_Game_SwitchAttack;
        public InputActionMap Get() { return m_Wrapper.m_Game; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameActions set) { return set.Get(); }
        public void SetCallbacks(IGameActions instance)
        {
            if (m_Wrapper.m_GameActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_GameActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnMovement;
                @Jump.started -= m_Wrapper.m_GameActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnJump;
                @LightAttack.started -= m_Wrapper.m_GameActionsCallbackInterface.OnLightAttack;
                @LightAttack.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnLightAttack;
                @LightAttack.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnLightAttack;
                @HeavyAttack.started -= m_Wrapper.m_GameActionsCallbackInterface.OnHeavyAttack;
                @HeavyAttack.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnHeavyAttack;
                @HeavyAttack.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnHeavyAttack;
                @SecondaryAttack.started -= m_Wrapper.m_GameActionsCallbackInterface.OnSecondaryAttack;
                @SecondaryAttack.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnSecondaryAttack;
                @SecondaryAttack.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnSecondaryAttack;
                @SwitchAttack.started -= m_Wrapper.m_GameActionsCallbackInterface.OnSwitchAttack;
                @SwitchAttack.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnSwitchAttack;
                @SwitchAttack.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnSwitchAttack;
            }
            m_Wrapper.m_GameActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @LightAttack.started += instance.OnLightAttack;
                @LightAttack.performed += instance.OnLightAttack;
                @LightAttack.canceled += instance.OnLightAttack;
                @HeavyAttack.started += instance.OnHeavyAttack;
                @HeavyAttack.performed += instance.OnHeavyAttack;
                @HeavyAttack.canceled += instance.OnHeavyAttack;
                @SecondaryAttack.started += instance.OnSecondaryAttack;
                @SecondaryAttack.performed += instance.OnSecondaryAttack;
                @SecondaryAttack.canceled += instance.OnSecondaryAttack;
                @SwitchAttack.started += instance.OnSwitchAttack;
                @SwitchAttack.performed += instance.OnSwitchAttack;
                @SwitchAttack.canceled += instance.OnSwitchAttack;
            }
        }
    }
    public GameActions @Game => new GameActions(this);
    public interface IGameActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnLightAttack(InputAction.CallbackContext context);
        void OnHeavyAttack(InputAction.CallbackContext context);
        void OnSecondaryAttack(InputAction.CallbackContext context);
        void OnSwitchAttack(InputAction.CallbackContext context);
    }
}