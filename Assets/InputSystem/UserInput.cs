using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UserInput : MonoBehaviour
{
    public static UserInput Instance { get; private set;}

    public Vector2 MoveInput { get; private set; }
    public bool AttackInput { get; private set; }
    public bool DashInput { get; private set; }
    public bool MenuToggleInput { get; private set; }
    public bool ItemPickUpInput { get; private set; }

    private static string _logname = "UserInput";

    private PlayerInput _playerInput;

    private InputAction _moveAction;
    private InputAction _attackAction;
    private InputAction _dashAction;
    private InputAction _menuToggleAction;
    private InputAction _itemPickUpAction;

    
    private void Awake()
    {
        if (Instance == null) {
			Instance = this;
		}
		else {
			Logger.LogWarning(_logname, "Multiple Instances found! Exiting..");
			Destroy(this);
			return;
		}

        _playerInput = GetComponent<PlayerInput>();

        SetupInputActions();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetupInputActions(){
        _moveAction = _playerInput.actions["Move"];
        _attackAction = _playerInput.actions["Primary"];
        _dashAction = _playerInput.actions["Dash"];
        _menuToggleAction = _playerInput.actions["ToggleMenu"];
        _itemPickUpAction = _playerInput.actions["Interact"];
    }

    private void UpdateInputs(){
        MoveInput = _moveAction.ReadValue<Vector2>();
        AttackInput = _attackAction.WasPressedThisFrame();
        DashInput = _dashAction.WasPressedThisFrame();
        MenuToggleInput = _menuToggleAction.WasPressedThisFrame();
        ItemPickUpInput = _itemPickUpAction.WasPerformedThisFrame();
    }

    public void RemapButtonClicked(String actionToRebind, int bindingIndex = -1)
    {
        _playerInput.actions[actionToRebind].Disable();
        _playerInput.actions[actionToRebind].PerformInteractiveRebinding(bindingIndex)
            // To avoid accidental input from mouse motion "<Keyboard>/escape"
            .WithControlsExcluding("Mouse")
            .WithCancelingThrough("<Keyboard>/escape")
            .OnMatchWaitForAnother(0.1f)

            .Start();
        _playerInput.actions[actionToRebind].Enable();
    }
}
