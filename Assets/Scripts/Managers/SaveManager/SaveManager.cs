using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour {

	[Header("Saving settings")]
	[SerializeField, Tooltip("Toggles save file encryption")] private bool _useEncryption = false;
	[SerializeField,] private bool _enableSaving = true;
	[SerializeField] private bool _initializeData = false;
	[SerializeField] private bool _saveOnQuit = false;

	[Header("Debug settings")]
	[SerializeField] private bool _enableLogging;


	private readonly static string _logname = "SaveManager";
	private static string _saveName = "OhMyCult";
	private string _selectedProfile = "";
	private List<ISaveable> _saveables;
	private GameData _gameData;
	private IDataManager _dataManager;

	private static SaveManager _saveManager;
	public static SaveManager Instance {
		get {
			if (!_saveManager) {
				_saveManager = FindAnyObjectByType<SaveManager>();

				if (!_saveManager) {
					Logger.LogError(_logname, "No SaveManager found in the scene!");
				}
			}
			return _saveManager;
		}
	}


	private void Awake() {
		if (_saveManager == null) {
			_saveManager = this;
		}
		else {
			Logger.LogWarning(_logname, "Multiple Instances found! Exiting...");
			Destroy(this);
			return;
		}
		DontDestroyOnLoad(Instance);

#if UNITY_EDITOR // Make sure debugging savefiles don't fuck up production saves
		_saveName += "-debug";
#endif
		_dataManager = new FileDataManager(Application.persistentDataPath, _saveName + ".WDF", _useEncryption);
	}

	private void OnEnable() {
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
		_saveables = FindAllSaveables();
		LoadGame();
	}

	private void OnDisable() {
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	private void OnApplicationQuit() {
		if (_saveOnQuit) {
			SaveGame();
		}
	}

	/// <summary>
	/// Changes the currently selected saveslot to the specified Id
	/// </summary>
	/// <param name="newProfileId"></param>
	public void ChangeSelectedProfileId(string newProfileId) {
		_selectedProfile = newProfileId;
		LoadGame();
	}

	/// <summary>
	/// Overwrites the locally stored data with a clean instance of GameData. </br>
	///
	/// </summary>
	public void NewGame() {
		_gameData = new GameData();
	}

	/// <summary>
	/// Calls the LoadData method in each ISavable with the currently stored GameData instance
	/// </summary>
	public void LoadGame() {
		if (_enableSaving) {
			SendToLogger("Loading game, looking for saves.");
			_gameData = _dataManager.Load(_selectedProfile);

			if (_gameData == null && _initializeData) {
				SendToLogger("No save found, initializing new game.");
				_selectedProfile = "debug";
				NewGame();
			}

			if (_gameData == null) {
				Logger.LogWarning(_logname, "No game data found. Start a new game before loading!");
				return;
			}

			_saveables.ForEach(saveable => saveable.LoadData(_gameData));
		}

	}

	/// <summary>
	/// Calls the SaveData method in each ISavable to save their data to the local GameData instance </br>
	/// Writes the local GameData to disk afterwarts
	/// </summary>
	public void SaveGame() {
		if (_enableSaving) {
			if (_gameData == null) {
				Logger.LogWarning(_logname, "No data to save. Start a new game before saving!");
				return;
			}

			SendToLogger("Saving game.");
			_saveables.ForEach(saveable => saveable?.SaveData(_gameData));
			_dataManager.Save(_gameData, _selectedProfile);
		}
	}


	/// <summary>
	/// Searches for all classes of defined types implementing the ISaveable interface
	/// </summary>
	/// <returns></returns>
	protected List<ISaveable> FindAllSaveables() {
		List<ISaveable> saveables = new();
		saveables.AddRange(FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>());
		return saveables;
	}

	public Dictionary<string, GameData> GetAllSaveSlotData() {
		return _dataManager.LoadAllSaveSlots();
	}

	public bool HasGameData() {
		return _gameData != null;
	}


	private void SendToLogger(string text) {
		if (_enableLogging) {
			Logger.Log(_logname, text);

		}
	}

}