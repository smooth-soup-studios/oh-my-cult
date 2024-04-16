using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers {
	public class GameManager : MonoBehaviour {
		public static GameManager Instance { get; private set; }

		private List<string> _sceneNamesInBuild = new();

		private List<string> _scenePathsInBuild = new();

		void Awake() {
			if (Instance != null && Instance != this) {
				Destroy(gameObject);
				return;
			}

			Instance = this;
			DontDestroyOnLoad(gameObject);

			// Cache all scene names & paths in build
			for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++) {
				string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
				_scenePathsInBuild.Add(scenePath);

				int lastSlash = scenePath.LastIndexOf("/");
				_sceneNamesInBuild.Add(scenePath.Substring(lastSlash + 1, scenePath.LastIndexOf(".") - lastSlash - 1));
			}
		}

		public void LoadScene(string scene) {
			Debug.Log("Does scene exist: " + DoesSceneExist(scene));
			SaveManager.Instance.SaveGame();
			if (DoesSceneExist(scene)) SceneManager.LoadScene(scene);
		}

		bool DoesSceneExist(string name) {
			if (string.IsNullOrEmpty(name)) {
				return false;
			}

			if (_sceneNamesInBuild.Contains(name) || _scenePathsInBuild.Contains(name)) {
				return true;
			}

			if (!int.TryParse(name, out int sceneInt)) {
				return SceneManager.GetSceneByBuildIndex(sceneInt).IsValid();
			}

			return false;
		}
	}
}