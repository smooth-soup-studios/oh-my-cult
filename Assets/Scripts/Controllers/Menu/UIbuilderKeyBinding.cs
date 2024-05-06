using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UIbuilderKeyBinding : MonoBehaviour {
	// Start is called before the first frame update
	private Button _backButton;
	private Button _continueButton;
	[SerializeField] private GameObject _keyBindingUI;
	[SerializeField] private GameObject _optionsUI;
	VisualElement _root;

	// Start is called before the first frame update
	void OnEnable() {
		Logger.Log("KeyBinding", "Binding Menu");
		_root = GetComponent<UIDocument>().rootVisualElement;
		_backButton = _root.Q<Button>("BackButton");
		_continueButton = _root.Q<Button>("ContinueButton");

		_backButton.clicked += OnBack;
		if (_continueButton != null) {
			_continueButton.clicked += OnContinue;
		}
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
}
