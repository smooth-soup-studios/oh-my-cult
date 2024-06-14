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

	private PlayerInput _playerInput;
	private PreMadeMovementButtons _buttoning;

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
		_buttoning = gameObject.GetComponent<PreMadeMovementButtons>();
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

	public void RemapButtonClicked(String actionToRebind, VisualElement container, int bindingIndex) {
		if(bindingIndex == -1)
			bindingIndex = _playerInput.actions[actionToRebind].GetBindingIndex(_playerInput.currentControlScheme);
		_playerInput.actions[actionToRebind].Disable();
		_playerInput.actions[actionToRebind].PerformInteractiveRebinding(bindingIndex)
			.WithBindingGroup(_playerInput.currentControlScheme)
			// To avoid accidental input from mouse motion
			.WithControlsExcluding("Mouse")
			.WithCancelingThrough("<Keyboard>/escape")
			.OnMatchWaitForAnother(0.1f)
			.OnComplete(operation => {
				String newText = GetBindingDisplayString(actionToRebind, _playerInput.currentControlScheme, bindingIndex);
				TextChange(newText, container, _playerInput.currentControlScheme);
				operation.Dispose();
			})
			.Start();

		_playerInput.actions[actionToRebind].Enable();
	}

	public string GetBindingDisplayString(string actionName, String bindingGroup = null, int bindingIndex = -1) {
		if (bindingGroup == null) {
			bindingGroup = _playerInput.currentControlScheme;
		}
		InputAction action = _playerInput.actions[actionName];
		if (action == null) {
			return string.Empty;
		}
		if (bindingIndex == -1)
			bindingIndex = action.GetBindingIndex(bindingGroup);
		String text = action.GetBindingDisplayString(bindingIndex);
		//text = text.Split("/")[0];
		return text;

	}

	public void TextChange(String buttonText, VisualElement container, String bindingGroup = null) {
		Button button;
		if (bindingGroup == "Controller") {
			button = _buttoning.GetControllerButton(buttonText);
			container.Q<Button>().text = "";
		}else{
			button = _buttoning.GetKeyboardButton(buttonText);
			container.Q<Button>().text = buttonText;
		}
		buttonChange(container, button);
	}

	private void buttonChange(VisualElement container, Button button){
		container.Q<Button>().style.unityTextAlign = button.style.unityTextAlign;
		container.Q<Button>().style.backgroundImage = button.style.backgroundImage;
		container.Q<Button>().style.marginTop = button.style.marginTop;
		container.Q<Button>().style.marginBottom = button.style.marginBottom;
		container.Q<Button>().style.marginLeft = button.style.marginLeft;
		container.Q<Button>().style.marginRight = button.style.marginRight;
	}
}