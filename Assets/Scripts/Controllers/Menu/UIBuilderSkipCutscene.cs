using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UIBuilderSkipCutscene : MonoBehaviour {
	Button _skipButton;
	VisualElement _root;

	void OnEnable() {
		_root = GetComponent<UIDocument>().rootVisualElement;
		_skipButton = _root.Q<Button>("SkipCutscene");
		_skipButton.clicked += SkipCutscene;

		GameObject hud = GameObject.Find("HUD");
		if (hud) {
			hud.GetComponent<UIDocument>().rootVisualElement.visible = false;
		}
	}

	public void SkipCutscene() {
		SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
