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
		_continueButton.clicked += OnContinue;
		//_loadGameButton.clicked += LoadData;
		_optionsButton.clicked += OnOptions;
		_quit.clicked += QuitGame;

	}

	void Start ()
    {
        _system = EventSystem.current;

    }


	void OnNewGame() {

		Logger.Log("MenuController", "Starting new game");

		SaveManager.Instance.ChangeSelectedProfileId("1");

		// Loads the default savestate, overwriting existing files.
		SaveManager.Instance.NewGame();
		SaveManager.Instance.SaveGame();
		SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void OnContinue() {
		SaveManager.Instance.NewGame();
		SaveManager.Instance.SaveGame();
		SceneManager.LoadSceneAsync(SceneDefs.Shortcut);
	}

	public void QuitGame() {
		GameManager.QuitGame();
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
}
