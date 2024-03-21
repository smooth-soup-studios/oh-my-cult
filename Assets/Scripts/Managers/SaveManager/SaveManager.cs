using UnityEngine;

public class SaveManager : MonoBehaviour {
	private static string _logname = "SaveManager";
	private static string _saveName = "OhMyCult";

	private static SaveManager _saveManager;
	public static SaveManager Instance {
		get {
			// Check if an instance exists. if not grab the one (which should be) present in the scene.
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
		if (_saveManager == null ) {
			_saveManager = this;
		}
		else {
			Logger.LogWarning(_logname, "Multiple Instances found! Exiting...");
			Destroy(this);
			return;
		}
		DontDestroyOnLoad(Instance);
	}
}