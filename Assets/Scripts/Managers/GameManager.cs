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

		public void LoadScene(string scene) {
			if (DoesSceneExist(scene)) SceneManager.LoadScene(scene);
		}

		static bool DoesSceneExist(string name) {
			if (string.IsNullOrEmpty(name)) {
				return false;
			}

			if (SceneManager.GetSceneByName(name).IsValid()) {
				return true;
			}

			if (!int.TryParse(name, out int sceneInt)) {
				return SceneManager.GetSceneByBuildIndex(sceneInt).IsValid();
			}

			return false;
		}
	}
}