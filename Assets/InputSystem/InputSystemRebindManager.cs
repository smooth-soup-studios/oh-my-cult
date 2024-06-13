using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class InputSystemRebindManager : MonoBehaviour {
	public static InputSystemRebindManager Instance { get; private set; }

	public Vector2 MoveInput { get; private set; }
	public bool AttackInput { get; private set; }
	public bool DashInput { get; private set; }
	public bool MenuToggleInput { get; private set; }
	public bool ItemPickUpInput { get; private set; }

	private static string _logname = "UserInput";
	private string _bindingGroup = "Keyboard";

	private PlayerInput _playerInput;

	private InputAction _moveAction;
	private InputAction _attackAction;
	private InputAction _dashAction;
	private InputAction _menuToggleAction;
	private InputAction _itemPickUpAction;


	private void Awake() {
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

	private void SetupInputActions() {
		_moveAction = _playerInput.actions["Move"];
		_attackAction = _playerInput.actions["Primary"];
		_dashAction = _playerInput.actions["Dash"];
		_menuToggleAction = _playerInput.actions["ToggleMenu"];
		_itemPickUpAction = _playerInput.actions["Interact"];
	}

	private void UpdateInputs() {
		MoveInput = _moveAction.ReadValue<Vector2>();
		AttackInput = _attackAction.WasPressedThisFrame();
		DashInput = _dashAction.WasPressedThisFrame();
		MenuToggleInput = _menuToggleAction.WasPressedThisFrame();
		ItemPickUpInput = _itemPickUpAction.WasPerformedThisFrame();
	}

	public void RemapButtonClicked(String actionToRebind, Button button) {
		int bindingIndex = _playerInput.actions[actionToRebind].GetBindingIndex(_bindingGroup);
		_playerInput.actions[actionToRebind].Disable();
		_playerInput.actions[actionToRebind].PerformInteractiveRebinding(bindingIndex)
			.WithBindingGroup(_playerInput.currentControlScheme)
			// To avoid accidental input from mouse motion
			.WithControlsExcluding("Mouse")
			.WithCancelingThrough("<Keyboard>/escape")
			.OnMatchWaitForAnother(0.1f)
			.OnComplete(operation => {
				String newText = GetBindingDisplayString(actionToRebind);
				RebindComplete(button, newText);
				operation.Dispose();
			})
			.Start();

		_playerInput.actions[actionToRebind].Enable();
	}

	public string GetBindingDisplayString(string actionName) {
		InputAction action = _playerInput.actions[actionName];
		if (action == null) {
			return string.Empty;
		}
		int bindingIndex = action.GetBindingIndex(_bindingGroup);
		return action.GetBindingDisplayString(bindingIndex);

	}

	public void RebindComplete(Button button, String buttonText) {
		button.text = buttonText;
	}
}