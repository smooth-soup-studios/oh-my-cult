using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

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

        _playerInput = FindObjectOfType<EventBus>().GetComponent<PlayerInput>();
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

    public void RemapButtonClicked(String actionToRebind,Button button, int bindingIndex = -1)
    {
        _playerInput.actions[actionToRebind].Disable();   
        _playerInput.actions[actionToRebind].PerformInteractiveRebinding(bindingIndex)
            .WithBindingGroup(_playerInput.currentControlScheme)
            // To avoid accidental input from mouse motion
            .WithControlsExcluding("Mouse")
            .WithCancelingThrough("<Keyboard>/escape")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => {
                String newText = GetBindingDisplayString(actionToRebind, bindingIndex);
                RebindComplete(button, newText);
                operation.Dispose(); 
            })
            .Start();
        // String newButton = _playerInput.actions[actionToRebind].GetBindingDisplayString();
        // Debug.Log("Rebind : " +  newButton);
        _playerInput.actions[actionToRebind].Enable();
    }

    public string GetBindingDisplayString(string actionName, int bindingIndex)
    {
        InputAction action = _playerInput.actions[actionName];
        if (action == null)
        {Debug.Log("action not found");
            return string.Empty;
        }
        if (bindingIndex == -1)
        {Debug.Log("" + action.GetBindingDisplayString());
            return action.GetBindingDisplayString();
        }
Debug.Log("" + action.GetBindingDisplayString(bindingIndex));
        return action.GetBindingDisplayString(bindingIndex);
        
    }

    public void RebindComplete(Button button, String buttonText){
        button.text = buttonText;
        Debug.Log("Rebind complete");
    }
}