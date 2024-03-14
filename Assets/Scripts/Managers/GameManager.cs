using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers {
public class GameManager : MonoBehaviour {
	public static GameManager Instance { get; private set; }

	void Awake() {
		if (Instance != null && Instance != this) {
			Destroy(gameObject);
			return;
		}

		Instance = this;
		DontDestroyOnLoad(gameObject);
	}

	void LoadScene(string scene) {
		if (DoesSceneExist(scene)) SceneManager.LoadScene(scene);
	}

	static bool DoesSceneExist(string name){
		if (string.IsNullOrEmpty(name)) {
			return false;
		}

		for (var i = 0; i < SceneManager.sceneCountInBuildSettings; i++) {
			var scenePath = SceneUtility.GetScenePathByBuildIndex(i);
			var lastSlash = scenePath.LastIndexOf("/", StringComparison.Ordinal);
			var sceneName = scenePath.Substring(lastSlash + 1, scenePath.LastIndexOf(".", StringComparison.Ordinal) - lastSlash - 1);

			if (string.Compare(name, sceneName, StringComparison.OrdinalIgnoreCase) == 0) {
				return true;
			}
		}
		return false;
	}
}
}