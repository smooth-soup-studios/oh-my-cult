using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Managers;
using UnityEngine.SceneManagement;

public class UIBuilderSkipCutscene : MonoBehaviour {
	Button _skipButton;
	VisualElement _root;
	// Start is called before the first frame update
	void OnEnable() {
		_root = GetComponent<UIDocument>().rootVisualElement;
		_skipButton = _root.Q<Button>("SkipCutscene");
		_skipButton.clicked += SkipCutscene;
		GameObject.Find("HUD").GetComponent<UIDocument>().rootVisualElement.visible = false;
	}

	// Update is called once per frame
	public void SkipCutscene() {
		SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
