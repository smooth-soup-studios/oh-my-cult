using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour, ISaveable {
	[SerializeField] private Button _newGameButton;
	[SerializeField] private Button _continueButton;
	[SerializeField] private Button _loadGameButton;
	private string _lastSceneLoaded = "MovementTestScene";

	private void Start() {
		SaveManager.Instance.ChangeSelectedProfileId("1");

		if (!SaveManager.Instance.HasGameData()) {
			_continueButton.interactable = false;
		}
		_loadGameButton.interactable = false;
	}


	public void OnNewGame() {
		DisableButtons();
		Logger.Log("MenuController", "Starting new game");


		// Loads the default savestate, overwriting existing files.
		SaveManager.Instance.NewGame();
		SaveManager.Instance.SaveGame();
		SceneManager.LoadSceneAsync(1);
	}

	public void OnContinue() {
		DisableButtons();
		Logger.Log("MenuController", "Loading Savefile");

		SaveManager.Instance.ChangeSelectedProfileId("1");

		// Works w/ Savemanager OnSceneLoaded() to load the game.
		SaveManager.Instance.SaveGame();
		SceneManager.LoadSceneAsync(_lastSceneLoaded);
	}

	public void QuitGame() {
		Logger.Log("MenuController", "THE MENU IS DEAD!");
		//Application.Quit();
	}

	public void DisableButtons() {
		_newGameButton.interactable = false;
		_continueButton.interactable = false;
	}

	public void LoadData(GameData data) {
		if (!(data.PlayerData.SceneName == "" || data.PlayerData.SceneName == null)) {
			_lastSceneLoaded = data.PlayerData.SceneName;
		}
	}

	public void SaveData(GameData data) {
	}
}

