using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers {
	public class GameManager : MonoBehaviour {
		public static GameManager Instance { get; private set; }
		private static readonly string _logname = "GameManager";

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

		public static void QuitGame() {
#if UNITY_WEBGL && !UNITY_EDITOR
				Logger.Log(_logname,"WebGL build detected, redirecting to homepage");
				Application.OpenURL("/");
#elif UNITY_EDITOR
			Logger.Log(_logname, "Quitting Playmode..");
			UnityEditor.EditorApplication.isPlaying = false;
#else
				Logger.Log(_logname, "Quitting Game..");
				Application.Quit();
#endif
		}

		public IEnumerator DoTheBossCutsceneThingHereBecauseTheBossWouldGetDisabled() {
			yield return new WaitForSeconds(1f);
			BlackFadeManager.Instance.Blacken(1f);
			BlackFadeManager.Instance.UnblackenOnLoad = true;
			yield return new WaitForSeconds(1f);

			SceneManager.LoadScene(SceneDefs.OutroCutscene);
		}
	}
}