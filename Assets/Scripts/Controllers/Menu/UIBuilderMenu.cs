using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System;
using System.ComponentModel.Design.Serialization;

public class UIBuilderMenu : MonoBehaviour {
	[SerializeField] private Button _newGameButton;
	[SerializeField] private Button _continueButton;
	[SerializeField] private Button _loadGameButton;
	[SerializeField] private Button _optionsButton;
	[SerializeField] private Button _quit;
	[SerializeField] private GameObject _mainMenuUI;
	[SerializeField] private GameObject _optionsUI;

	private string _lastSceneLoaded = "level_0";
	VisualElement _root;

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

	}




	void OnNewGame() {
		DisableButtons();
		Logger.Log("MenuController", "Starting new game");

		SaveManager.Instance.ChangeSelectedProfileId("1");

		// Loads the default savestate, overwriting existing files.
		SaveManager.Instance.NewGame();
		SaveManager.Instance.SaveGame();
		SceneManager.LoadSceneAsync(1);
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
		#if UNITY_WEBGL
            Logger.Log("UIBuilder","WebGL build detected, redirecting to homepage");
            Application.OpenURL("/");
        #else
            Logger.Log("UIBuilder","Quitting Game..");
            Application.Quit();
        #endif
	}

	public void DisableButtons() {
		if (!SaveManager.Instance.HasGameData()) {
			_loadGameButton.style.unityBackgroundImageTintColor = new Color(255f, 255f, 255f, .5f);
			_continueButton.style.unityBackgroundImageTintColor = new Color(255f, 255f, 255f, .5f);
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
		_mainMenuUI.SetActive(false);
		_optionsUI.SetActive(true);
	}
}
