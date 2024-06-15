using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class InputSystemRebindManager : MonoBehaviour {
	public static InputSystemRebindManager Instance { get; private set; }

	private static string _logname = "UserInput";

	private PlayerInput _playerInput;
	private PreMadeMovementButtons _buttoning;

	private void Awake() {
		if (Instance == null) {
			Instance = this;
		}
		else {
			Logger.LogWarning(_logname, "Multiple Instances found! Exiting..");
			Destroy(this);
			return;
		}
		AcquireRefs();
	}

	private void Start() {
		AcquireRefs();
	}

	public void AcquireRefs() {
		_playerInput = FindObjectOfType<EventBus>().GetComponent<PlayerInput>();
		_buttoning = gameObject.GetComponent<PreMadeMovementButtons>();
	}

	public void RemapButtonClicked(string actionToRebind, Button container, int bindingIndex, string controlScheme) {
		if (_playerInput == null) {
			AcquireRefs();
		}

		string currentControlScheme = _playerInput.currentControlScheme;

		if (bindingIndex == -1)
			bindingIndex = _playerInput.actions[actionToRebind].GetBindingIndex(currentControlScheme);

		if (currentControlScheme != controlScheme)
			return;

		_playerInput.actions[actionToRebind].Disable();
		_playerInput.actions[actionToRebind].PerformInteractiveRebinding(bindingIndex)
			.WithBindingGroup(currentControlScheme)
			// To avoid accidental input from mouse motion
			.WithControlsExcluding("Mouse")
			.WithCancelingThrough("<Keyboard>/escape")
			.OnMatchWaitForAnother(0.1f)
			.OnComplete(operation => {
				string newText = GetBindingDisplayString(actionToRebind, currentControlScheme, bindingIndex);
				TextChange(newText, container, currentControlScheme);
				operation.Dispose();
			})
			.Start();

		_playerInput.actions[actionToRebind].Enable();
	}

	public string GetBindingDisplayString(string actionName, string bindingGroup = null, int bindingIndex = -1) {
		if (bindingGroup == null) {
			bindingGroup = _playerInput.currentControlScheme;
		}
		InputAction action = _playerInput.actions[actionName];
		if (action == null) {
			return string.Empty;
		}
		if (bindingIndex == -1)
			bindingIndex = action.GetBindingIndex(bindingGroup);
		string text = action.GetBindingDisplayString(bindingIndex);
		return text;

	}

	public void TextChange(string buttonText, Button container, string bindingGroup = null) {
		Button button;
		if (bindingGroup == "Controller") {
			button = _buttoning.GetControllerButton(buttonText);
			container.text = "";
		}
		else {
			button = _buttoning.GetKeyboardButton(buttonText);
			container.text = buttonText;
		}
		NewButton(container, button);
	}

	public void NewButton(Button container, Button button) {
		container.style.width = button.style.width;
		container.style.height = button.style.height;
		container.style.backgroundImage = button.style.backgroundImage;
	}
}