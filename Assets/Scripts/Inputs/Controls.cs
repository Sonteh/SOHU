// GENERATED AUTOMATICALLY FROM 'Assets/Input/Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Controls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""3bce414b-45b3-454f-b3a7-44821ecd640b"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""433ae4fb-8d3d-4602-b072-90a882424720"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""CastFireball"",
                    ""type"": ""Button"",
                    ""id"": ""5b818859-57ff-45ac-ab7e-93d0578f26a9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""CastMagicMissile"",
                    ""type"": ""Button"",
                    ""id"": ""e201e00a-431b-43bb-a5b0-6f2bfc0113b2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Dupa"",
                    ""type"": ""Button"",
                    ""id"": ""db6efdfd-82c4-4c2c-9a55-305cb912188a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""496bcae3-6782-4f18-8f5f-8694651fa3c9"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MouseControl"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""47c33e02-e986-4ee2-a58e-1bf15413251e"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Spells"",
                    ""action"": ""CastFireball"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""94664af4-7be6-4588-8bdb-37867e60c8e0"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Spells"",
                    ""action"": ""CastMagicMissile"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""147c99bd-ec51-4e3a-9aee-a43a37346be4"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Spells"",
                    ""action"": ""Dupa"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""MouseControl"",
            ""bindingGroup"": ""MouseControl"",
            ""devices"": [
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Spells"",
            ""bindingGroup"": ""Spells"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_CastFireball = m_Player.FindAction("CastFireball", throwIfNotFound: true);
        m_Player_CastMagicMissile = m_Player.FindAction("CastMagicMissile", throwIfNotFound: true);
        m_Player_Dupa = m_Player.FindAction("Dupa", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_CastFireball;
    private readonly InputAction m_Player_CastMagicMissile;
    private readonly InputAction m_Player_Dupa;
    public struct PlayerActions
    {
        private @Controls m_Wrapper;
        public PlayerActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @CastFireball => m_Wrapper.m_Player_CastFireball;
        public InputAction @CastMagicMissile => m_Wrapper.m_Player_CastMagicMissile;
        public InputAction @Dupa => m_Wrapper.m_Player_Dupa;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @CastFireball.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCastFireball;
                @CastFireball.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCastFireball;
                @CastFireball.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCastFireball;
                @CastMagicMissile.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCastMagicMissile;
                @CastMagicMissile.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCastMagicMissile;
                @CastMagicMissile.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCastMagicMissile;
                @Dupa.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDupa;
                @Dupa.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDupa;
                @Dupa.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDupa;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @CastFireball.started += instance.OnCastFireball;
                @CastFireball.performed += instance.OnCastFireball;
                @CastFireball.canceled += instance.OnCastFireball;
                @CastMagicMissile.started += instance.OnCastMagicMissile;
                @CastMagicMissile.performed += instance.OnCastMagicMissile;
                @CastMagicMissile.canceled += instance.OnCastMagicMissile;
                @Dupa.started += instance.OnDupa;
                @Dupa.performed += instance.OnDupa;
                @Dupa.canceled += instance.OnDupa;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    private int m_MouseControlSchemeIndex = -1;
    public InputControlScheme MouseControlScheme
    {
        get
        {
            if (m_MouseControlSchemeIndex == -1) m_MouseControlSchemeIndex = asset.FindControlSchemeIndex("MouseControl");
            return asset.controlSchemes[m_MouseControlSchemeIndex];
        }
    }
    private int m_SpellsSchemeIndex = -1;
    public InputControlScheme SpellsScheme
    {
        get
        {
            if (m_SpellsSchemeIndex == -1) m_SpellsSchemeIndex = asset.FindControlSchemeIndex("Spells");
            return asset.controlSchemes[m_SpellsSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnCastFireball(InputAction.CallbackContext context);
        void OnCastMagicMissile(InputAction.CallbackContext context);
        void OnDupa(InputAction.CallbackContext context);
    }
}
