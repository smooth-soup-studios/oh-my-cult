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
	[SerializeField] private bool _dataManagerLogging;

	protected readonly static string Logname = "SaveManager";
	protected static string SaveName = "OhMyCult";
	protected string SelectedProfile = "";
	protected List<ISaveable> Saveables;
	protected GameData GameData;
	protected GameData InjectionCache;
	protected IDataManager DataManager;

	private static SaveManager _saveManager;
	public static SaveManager Instance {
		get {
			if (!_saveManager) {
				_saveManager = FindAnyObjectByType<SaveManager>();

				if (!_saveManager) {
					Logger.LogError(Logname, "No SaveManager found in the scene!");
				}
			}
			return _saveManager;
		}
	}

	protected void OnEnable() {
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	protected void Awake() {
		if (_saveManager == null) {
			_saveManager = this;
		}
		else if (_saveManager != this) {
			Logger.LogWarning(Logname, "Multiple Instances found! Exiting...");
			Destroy(gameObject);
			return;
		}
		DontDestroyOnLoad(Instance);

#if UNITY_EDITOR // Make sure debugging savefiles don't fuck up production saves
		SaveName += "-debug";
#endif
		DataManager = new FileDataManager(Application.persistentDataPath, SaveName + ".WDF", _useEncryption, _dataManagerLogging);
	}


	protected void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
		Saveables = FindAllSaveables();
		LoadGame();
	}

	protected void OnDisable() {
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	protected void OnApplicationQuit() {
		if (_saveOnQuit) {
			SaveGame();
		}
	}

	/// <summary>
	/// Changes the currently selected saveslot to the specified Id
	/// </summary>
	/// <param name="newProfileId"></param>
	public void ChangeSelectedProfileId(string newProfileId) {
		SelectedProfile = newProfileId;
		LoadGame();
	}

	/// <summary>
	/// Changes the currently selected saveslot to the specified Id but doesn't automatically load the new savedata
	/// </summary>
	/// <param name="newProfileId"></param>
	public void ChangeSelectedProfileIdNoLoad(string newProfileId) {
		SelectedProfile = newProfileId;
	}

	/// <summary>
	/// Overwrites the locally stored data with a clean instance of GameData.
	/// </summary>
	public void NewGame() {
		SendToLogger("Starting new game. Creating default gamedata");
		GameData oldData = GameData;
		GameData = new();
		TransferSettings(ref oldData, ref GameData);
	}

	/// <summary>
	/// Calls the LoadData method in each ISavable with the currently stored GameData instance
	/// </summary>
	public void LoadGame() {
		if (_enableSaving) {
			SendToLogger("Loading game, looking for saves.");
			GameData oldData = GameData;
			GameData = DataManager.Load(SelectedProfile);

			if (GameData == null && _initializeData) {
				SendToLogger("No save found. initializing new game.");
				SelectedProfile = "debug";
				NewGame();
			}

			if (GameData == null) {
				Logger.LogWarning(Logname, "No game data found. Start a new game before loading!");
				return;
			}

			Saveables.ForEach(saveable => saveable.LoadData(GameData));
			TransferSettings(ref oldData, ref GameData);
		}
	}

	/// <summary>
	/// Calls the SaveData method in each ISavable to save their data to the local GameData instance </br>
	/// Writes the local GameData to disk afterwarts
	/// </summary>
	public void SaveGame() {
		if (_enableSaving) {
			if (GameData == null) {
				Logger.LogWarning(Logname, "No data to save. Start a new game before saving!");
				return;
			}

			SendToLogger("Saving game.");
			Saveables.ForEach(saveable => saveable?.SaveData(GameData));
			DataManager.Save(GameData, SelectedProfile);
		}
	}

	/// <summary>
	/// Saves the gamestate to the injection cache without writing to disk
	/// </summary>
	public void SoftSaveGame() {
		if (_enableSaving) {
			SendToLogger("SoftSaving game.");
			GameData tempData = new();
			Saveables.ForEach(saveable => saveable?.SaveData(tempData));
			TransferSettings(ref tempData, ref GameData);
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
		return DataManager.LoadAllSaveSlots();
	}

	public bool HasGameData() {
		return GameData != null;
	}

	public void TransferSettings(ref GameData source, ref GameData target) {
		if (source == null || target == null) {
			return;
		}
		target.PlayerSettings = source.PlayerSettings;
	}


	private void SendToLogger(string text) {
		if (_enableLogging) {
			Logger.Log(Logname, text);
		}
	}

}