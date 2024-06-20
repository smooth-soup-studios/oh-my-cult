using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System;
using System.ComponentModel.Design.Serialization;
using Managers;

public class UIBuilderMenu : MonoBehaviour {
	private Button _newGameButton;
	private Button _continueButton;
	private Button _loadGameButton;
	private Button _optionsButton;
	private Button _quit;

	[SerializeField] private GameObject _mainMenuUI;
	[SerializeField] private GameObject _optionsUI;

	private string _lastSceneLoaded = "level_0";
	VisualElement _root;
	EventSystem _system;

	private void OnEnable() {
		_root = GetComponent<UIDocument>().rootVisualElement;

		_newGameButton = _root.Q<Button>("NewButton");
		_continueButton = _root.Q<Button>("ContinueButton");
		_loadGameButton = _root.Q<Button>("LoadButton");
		_optionsButton = _root.Q<Button>("OptionsButton");
		_quit = _root.Q<Button>("QuitButton");

		_newGameButton.clicked += OnNewGame;
		//_continueButton.clicked += OnContinue;
		//_loadGameButton.clicked += LoadData;
		_optionsButton.clicked += OnOptions;
		_quit.clicked += QuitGame;
		DisableButtons();

		_newGameButton.RegisterCallback<FocusInEvent>(OnFocusInNewGame);
		_newGameButton.RegisterCallback<FocusOutEvent>(OnFocusOutNewGame);

		_continueButton.RegisterCallback<FocusInEvent>(OnFocusInContinueGame);
		_continueButton.RegisterCallback<FocusOutEvent>(OnFocusOutContinueGame);

		_loadGameButton.RegisterCallback<FocusInEvent>(OnFocusInLoadGame);
		_loadGameButton.RegisterCallback<FocusOutEvent>(OnFocusOutLoadGame);

		_optionsButton.RegisterCallback<FocusInEvent>(OnFocusInOptions);
		_optionsButton.RegisterCallback<FocusOutEvent>(OnFocusOutOptions);

		_quit.RegisterCallback<FocusInEvent>(OnFocusInQuit);
		_quit.RegisterCallback<FocusOutEvent>(OnFocusOutQuit);

	}

	void Start ()
    {
        _system = EventSystem.current;
         
    }


	void OnNewGame() {
		DisableButtons();
		Logger.Log("MenuController", "Starting new game");

		SaveManager.Instance.ChangeSelectedProfileId("1");

		// Loads the default savestate, overwriting existing files.
		SaveManager.Instance.NewGame();
		SaveManager.Instance.SaveGame();
		SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void OnContinue() {
		// DisableButtons();
		// Logger.Log("MenuController", "Loading Savefile");

		// SaveManager.Instance.ChangeSelectedProfileId("1");

		// // Works w/ Savemanager OnSceneLoaded() to load the game.
		// SaveManager.Instance.SaveGame();
		// SceneManager.LoadSceneAsync(_lastSceneLoaded);
	}

	public void QuitGame() {
		GameManager.QuitGame();
	}

	public void DisableButtons() {
		if (!SaveManager.Instance.HasGameData()) {
			_loadGameButton.style.unityBackgroundImageTintColor = new Color(255f, 255f, 255f, .5f);
			_loadGameButton.focusable = false;
			_continueButton.style.unityBackgroundImageTintColor = new Color(255f, 255f, 255f, .5f);
			_continueButton.focusable = false;
		}
	}

	public void LoadData(GameData data) {
		if (!(data.PlayerData.SceneName == "" || data.PlayerData.SceneName == null)) {
			_lastSceneLoaded = data.PlayerData.SceneName;
		}
	}

	public void SaveData(GameData data) {
	}

	void OnOptions() {
		Logger.Log("MenuController", "Viewing options");
		SaveManager.Instance.ChangeSelectedProfileId("1");
		_mainMenuUI.GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("Container").visible = false;
		_optionsUI.GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("Container").visible = true;
	}

	private void OnFocusInNewGame(FocusInEvent evt) {
		_newGameButton.style.unityBackgroundImageTintColor = new Color(204f, 204f, 204f);
	}

	private void OnFocusOutNewGame(FocusOutEvent evt) {
		_newGameButton.style.unityBackgroundImageTintColor = new Color(255f, 255f, 255f);
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
