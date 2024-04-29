using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UIBuilderInGameMenu : MonoBehaviour
{
	private Button _continueButton;
    private Button _saveGameButton;
	private Button _loadGameButton;
	private Button _optionsButton;
	private Button _quit;

	[SerializeField] private GameObject _inGameUI;
	[SerializeField] private GameObject _optionsUI;

	private string _lastSceneLoaded = "level_0";
	VisualElement _root;

	private void OnEnable() {
        Time.timeScale = 0f;
		_root = GetComponent<UIDocument>().rootVisualElement;

		_continueButton = _root.Q<Button>("ContinueButton");
        _saveGameButton = _root.Q<Button>("SaveButton");
		_loadGameButton = _root.Q<Button>("LoadButton");
		_optionsButton = _root.Q<Button>("OptionsButton");
		_quit = _root.Q<Button>("QuitButton");

		_continueButton.clicked += OnContinue;
        _saveGameButton.clicked += OnSaveGame;
		//_loadGameButton.clicked += LoadData;
		_optionsButton.clicked += OnOptions;
		_quit.clicked += QuitGame;
		DisableButtons();

	}




	void OnSaveGame() {
		// DisableButtons();
		// Logger.Log("MenuController", "Starting new game");

		// SaveManager.Instance.ChangeSelectedProfileId("1");

		// // Loads the default savestate, overwriting existing files.
		// SaveManager.Instance.NewGame();
		// SaveManager.Instance.SaveGame();
		// SceneManager.LoadSceneAsync(1);
	}

	public void OnContinue() {
        Time.timeScale = 1f;
		_inGameUI.SetActive(false);
	}

	public void QuitGame() {
		Logger.Log("MenuController", "THE MENU IS DEAD!");
		//Application.Quit();
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

	public void SaveData(GameData data) {
	}

	void OnOptions() {
		Logger.Log("InGameController", "Viewing options");
		_inGameUI.SetActive(false);
		_optionsUI.SetActive(true);
	}
}
