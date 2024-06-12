using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UIBuilderDeathMenu : MonoBehaviour {
	Button _yesButton;
	Button _noButton;
	VisualElement _root;

	GameObject _player;
	VisualElement _death;

	void OnEnable() {
		_root = GetComponent<UIDocument>().rootVisualElement;
		EventBus.Instance.Subscribe<GameObject>(EventType.DEATH, OnDeath);


		_yesButton = _root.Q<Button>("YesButton");
		_yesButton.clicked += OnYesButton;
		_noButton = _root.Q<Button>("NoButton");
		_noButton.clicked += OnNoButton;
		_death = _root.Q<VisualElement>("Container");

		_player = GameObject.Find("Player");
	}

	void OnDeath(GameObject target) {
		if (target == _player) {
			if (_death != null) {
				GameObject.Find("HUD").GetComponent<UIDocument>().rootVisualElement.visible = false;
				StartCoroutine(ShowAnimation());

			}
			else {
				// Return to main menu?
				SceneManager.LoadSceneAsync(0);
			}
		}
	}

	void OnYesButton() {
		// Initialize new gamedata
		SaveManager.Instance.ChangeSelectedProfileId("1");
		SaveManager.Instance.NewGame();

		// Reload the current scene
		SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		Time.timeScale = 1;
	}
	void OnNoButton() {
		// Load the mainmenu
		SceneManager.LoadSceneAsync(0);
		Time.timeScale = 1;
	}
	public IEnumerator ShowAnimation() {
		yield return new WaitForSeconds(1f);
		_death.visible = true;
		Time.timeScale = 0;
	}
}
