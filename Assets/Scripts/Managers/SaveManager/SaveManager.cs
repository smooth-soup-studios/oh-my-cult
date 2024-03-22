using UnityEngine;

public class SaveManager : MonoBehaviour {

	[Header("Saving settings")]
	[SerializeField, Tooltip("Toggles save file encryption")] private bool _useEncryption = false;
	[SerializeField, ] private bool _enableSaving = true;
    [SerializeField] private bool _initializeData = false;
    [SerializeField] private bool _saveOnQuit = false;


	private readonly static string _logname = "SaveManager";
	private static string _saveName = "OhMyCult";
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

}