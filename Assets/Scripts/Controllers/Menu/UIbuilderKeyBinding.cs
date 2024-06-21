using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UIbuilderKeyBinding : MonoBehaviour {
	InputSystemRebindManager _userInput;

	private string _controlScheme;

	private Button _keyboard;
	private Button _controller;
	private Button _backButton;
	private Button _continueButton;

	private VisualElement _leftButton;
	private VisualElement _rightButton;
	private VisualElement _upButton;
	private VisualElement _downButton;
	private VisualElement _interactButton;
	private VisualElement _attackButton;
	private VisualElement _dashButton;

	private int _controllerOffset = 0;

	[SerializeField] private GameObject _keyBindingUI;
	[SerializeField] private GameObject _optionsUI;
	VisualElement _root;

	// Start is called before the first frame update
	void OnEnable() {
		_userInput = gameObject.GetComponent<InputSystemRebindManager>();
		_controlScheme = "Keyboard";

		Logger.Log("KeyBinding", "Binding Menu");
		_root = GetComponent<UIDocument>().rootVisualElement;

		_keyboard = _root.Q<Button>("Keyboard");
		_controller = _root.Q<Button>("Controller");
		_backButton = _root.Q<Button>("BackButton");
		_continueButton = _root.Q<Button>("ContinueButton");

		_leftButton = _root.Q<VisualElement>("WalkLeftButton");
		_rightButton = _root.Q<VisualElement>("WalkRightButton");
		_upButton = _root.Q<VisualElement>("WalkUpButton");
		_downButton = _root.Q<VisualElement>("WalkDownButton");
		_interactButton = _root.Q<VisualElement>("InteractButton");
		_attackButton = _root.Q<VisualElement>("AttackButton");
		_dashButton = _root.Q<VisualElement>("DashButton");

		_keyboard.clicked += OnKeyboard;
		_controller.clicked += OnController;
		_backButton.clicked += OnBack;
		if (_continueButton != null) {
			_continueButton.clicked += OnContinue;
			_backButton.focusable = false;
			_backButton.visible = false;
		}

		_leftButton.Q<Button>().clicked += OnLeftChange;
		_rightButton.Q<Button>().clicked += OnRightChange;
		_upButton.Q<Button>().clicked += OnUpChange;
		_downButton.Q<Button>().clicked += OnDownChange;
		_interactButton.Q<Button>().clicked += OnInteractChange;
		_attackButton.Q<Button>().clicked += OnAttackChange;
		_dashButton.Q<Button>().clicked += OnDashChange;

		ReloadText("Keyboard");
	}

	void OnKeyboard(){
		_controllerOffset = 0;
		_controlScheme = "Keyboard";
		ReloadText(_controlScheme);
	}

	void OnController(){
		_controllerOffset = 10;
		_controlScheme = "Controller";
		ReloadText(_controlScheme);
	}

	void ReloadText(string bindingGroup){
		if(!_userInput)
			return;

		string newText;

		//Move Up button
		newText = _userInput.GetBindingDisplayString("Move", bindingGroup, 1 + _controllerOffset);
		_userInput.TextChange(newText, _upButton, bindingGroup);

		//Move down button
		newText = _userInput.GetBindingDisplayString("Move", bindingGroup, 2 + _controllerOffset);
		_userInput.TextChange(newText, _downButton, bindingGroup);

		//Move down button
		newText = _userInput.GetBindingDisplayString("Move", bindingGroup, 3 + _controllerOffset);
		_userInput.TextChange(newText, _leftButton, bindingGroup);

		//Move down button
		newText = _userInput.GetBindingDisplayString("Move", bindingGroup, 4 + _controllerOffset);
		_userInput.TextChange(newText, _rightButton, bindingGroup);

		//Interact button
		newText = _userInput.GetBindingDisplayString("Interact", bindingGroup);
		_userInput.TextChange(newText, _interactButton, bindingGroup);

		//Attack button
		newText = _userInput.GetBindingDisplayString("Primary", bindingGroup);
		_userInput.TextChange(newText, _attackButton, bindingGroup);

		//Dash button
		newText = _userInput.GetBindingDisplayString("Dash", bindingGroup);
		_userInput.TextChange(newText, _dashButton, bindingGroup);
	}

	void OnBack() {
		Logger.Log("KeyBinding", "Back to options");
		_keyBindingUI.GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("Container").visible = false;
		_optionsUI.GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("Container").visible = true;
	}

	//OnContinue is only used in the keybindingsintro scene where it needs to switch scenes rather then showing the right menu
	void OnContinue() {
		Logger.Log("KeyBindingIntro", "Continue to story");
		SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
	}

	void OnKeyChange(string buttonToRebind, VisualElement container, int KeyBinding = -1) {
		if (_userInput) {
			_userInput.RemapButtonClicked(buttonToRebind, container, KeyBinding, _controlScheme);
		}
	}

	void OnLeftChange() {
		OnKeyChange("Move", _leftButton, 3 + _controllerOffset);
	}

	void OnRightChange() {
		OnKeyChange("Move", _rightButton, 4 + _controllerOffset);
	}

	void OnUpChange() {
		OnKeyChange("Move", _upButton, 1 + _controllerOffset);
	}

	void OnDownChange() {
		OnKeyChange("Move", _downButton, 2 + _controllerOffset);
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
