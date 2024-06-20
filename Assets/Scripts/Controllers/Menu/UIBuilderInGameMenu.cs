using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UIBuilderInGameMenu : MonoBehaviour {
	private Button _continueButton;
	private Button _loadGameButton;
	private Button _optionsButton;
	private Button _quit;

	//[SerializeField] private GameObject _inGameUI;
	//[SerializeField] private GameObject _optionsUI;

	private string _lastSceneLoaded = "level_0";
	VisualElement _root;
	VisualElement _hud;
	VisualElement _optionsUI;
	VisualElement _pauseMenu;
	VisualElement _quitWarning;

	private void OnEnable() {
		EventBus.Instance.Subscribe(EventType.PAUSE, OnPause);
		_root = GetComponent<UIDocument>().rootVisualElement;
		_hud = GameObject.Find("HUD").GetComponent<UIDocument>().rootVisualElement;
		_optionsUI = GameObject.Find("OptionsMenu").GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("Container");
		_pauseMenu = GameObject.Find("PauseMenu").GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("Container");
		_quitWarning = GameObject.Find("QuitWarning").GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("Container");

		_continueButton = _root.Q<Button>("ContinueButton");
		_loadGameButton = _root.Q<Button>("LoadButton");
		_optionsButton = _root.Q<Button>("OptionsButton");
		_quit = _root.Q<Button>("QuitButton");

		_continueButton.clicked += OnContinue;
		//_loadGameButton.clicked += LoadData;
		_optionsButton.clicked += OnOptions;
		_quit.clicked += QuitGame;
		DisableButtons();

		// Check if HUD was disabled on outro in earlier run
		if (!_hud.visible) {
			_hud.visible = true;
		}

		_continueButton.RegisterCallback<FocusInEvent>(OnFocusInContinueGame);
		_continueButton.RegisterCallback<FocusOutEvent>(OnFocusOutContinueGame);

		_loadGameButton.RegisterCallback<FocusInEvent>(OnFocusInLoadGame);
		_loadGameButton.RegisterCallback<FocusOutEvent>(OnFocusOutLoadGame);

		_optionsButton.RegisterCallback<FocusInEvent>(OnFocusInOptions);
		_optionsButton.RegisterCallback<FocusOutEvent>(OnFocusOutOptions);

		_quit.RegisterCallback<FocusInEvent>(OnFocusInQuit);
		_quit.RegisterCallback<FocusOutEvent>(OnFocusOutQuit);
	}

	public void OnContinue() {
		_hud.visible = true;
		_pauseMenu.visible = false;
		Time.timeScale = 1;
	}

	public void QuitGame() {
		_quitWarning.visible = true;
	}

	public void DisableButtons() {
		if (!SaveManager.Instance.HasGameData()) {
			_loadGameButton.style.unityBackgroundImageTintColor = new Color(255f, 255f, 255f, .5f);
		}
	}

	public void LoadData(GameData data) {
		if (!(data.PlayerData.SceneName == "" || data.PlayerData.SceneName == null)) {
			_lastSceneLoaded = data.PlayerData.SceneName;
		}
	}

	void OnOptions() {
		Logger.Log("InGameController", "Viewing options");
		_pauseMenu.visible = false;
		_optionsUI.visible = true;
	}

	void OnPause() {
		if (Time.timeScale == 1) {
			_hud.visible = false;
			_pauseMenu.visible = true;
			Time.timeScale = 0;
		}
		else if (Time.timeScale == 0 && _pauseMenu.visible && !_quitWarning.visible) {
			_hud.visible = true;
			_pauseMenu.visible = false;
			Time.timeScale = 1;
		}
	}

	private void OnFocusInContinueGame(FocusInEvent evt) {
		_continueButton.style.unityBackgroundImageTintColor = new Color(204f, 204f, 204f);
	}

	private void OnFocusOutContinueGame(FocusOutEvent evt) {
		_continueButton.style.unityBackgroundImageTintColor = new Color(255f, 255f, 255f);
	}

	private void OnFocusInLoadGame(FocusInEvent evt) {
		_loadGameButton.style.unityBackgroundImageTintColor = new Color(204f, 204f, 204f);
	}

	private void OnFocusOutLoadGame(FocusOutEvent evt) {
		_loadGameButton.style.unityBackgroundImageTintColor = new Color(255f, 255f, 255f);
	}

	private void OnFocusInOptions(FocusInEvent evt) {
		_optionsButton.style.unityBackgroundImageTintColor = new Color(204f, 204f, 204f);
	}

	private void OnFocusOutOptions(FocusOutEvent evt) {
		_optionsButton.style.unityBackgroundImageTintColor = new Color(255f, 255f, 255f);
	}

	private void OnFocusInQuit(FocusInEvent evt) {
		_quit.style.unityBackgroundImageTintColor = new Color(204f, 204f, 204f);
	}

	private void OnFocusOutQuit(FocusOutEvent evt) {
		_quit.style.unityBackgroundImageTintColor = new Color(255f, 255f, 255f);
	}
}
