using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UIbuilderKeyBinding : MonoBehaviour {
	InputSystemRebindManager _userInput;

	private Button _backButton;
	private Button _continueButton;
	private Button _leftButton;
	private Button _rightButton;
	private Button _upButton;
	private Button _downButton;
	private Button _interactButton;
	private Button _attackButton;
	private Button _dashButton;

	[SerializeField] private GameObject _keyBindingUI;
	[SerializeField] private GameObject _optionsUI;
	VisualElement _root;

	// Start is called before the first frame update
	void OnEnable() {
		_userInput = FindObjectOfType<InputSystemRebindManager>();

		Logger.Log("KeyBinding", "Binding Menu");
		_root = GetComponent<UIDocument>().rootVisualElement;

		_backButton = _root.Q<Button>("BackButton");
		_continueButton = _root.Q<Button>("ContinueButton");

		_leftButton = _root.Q<Button>("WalkLeftButton");
		_rightButton = _root.Q<Button>("WalkRightButton");
		_upButton = _root.Q<Button>("WalkUpButton");
		_downButton = _root.Q<Button>("WalkDownButton");
		_interactButton = _root.Q<Button>("InteractButton");
		_attackButton = _root.Q<Button>("AttackButton");
		_dashButton = _root.Q<Button>("DashButton");

		_backButton.clicked += OnBack;
		if (_continueButton != null) {
			_continueButton.clicked += OnContinue;
		}

		_leftButton.clicked += OnLeftChange;
		_rightButton.clicked += OnRightChange;
		_upButton.clicked += OnUpChange;
		_downButton.clicked += OnDownChange;
		_interactButton.clicked += OnInteractChange;
		_attackButton.clicked += OnAttackChange;
		_dashButton.clicked += OnDashChange;
	}

	void OnBack() {
		Logger.Log("KeyBinding", "Back to options");
		_keyBindingUI.GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("Container").visible = false;
		_optionsUI.GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("Container").visible = true;
	}
	//OnContinue is only used in the keybindingsintro scene where it needs to switch scenes rather then showing the right menu
	void OnContinue() {
		Logger.Log("KeyBindingIntro", "Continue to story");
		SceneManager.LoadSceneAsync(2);
	}

	void OnKeyChange(String buttonToRebind, Button button, int bindingIndex = -1) {
		if (_userInput) {
			_userInput.RemapButtonClicked(buttonToRebind, button, bindingIndex);
		}
	}

	void OnLeftChange() {
		OnKeyChange("Move", _leftButton, 3);
	}

	void OnRightChange() {
		OnKeyChange("Move", _rightButton, 4);
	}

	void OnUpChange() {
		OnKeyChange("Move", _upButton, 1);
	}

	void OnDownChange() {
		OnKeyChange("Move", _downButton, 2);
	}

	void OnInteractChange() {
		OnKeyChange("Interact", _interactButton);
	}

	void OnAttackChange() {
		OnKeyChange("Primary", _attackButton);
	}

	void OnDashChange() {
		OnKeyChange("Dash", _dashButton);
	}
}
