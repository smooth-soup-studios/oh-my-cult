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

		return SceneManager.GetSceneByName(name).IsValid() || SceneManager.GetSceneByBuildIndex(int.Parse(name)).IsValid();
	}
}
}