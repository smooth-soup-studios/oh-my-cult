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

		_skipButton.RegisterCallback<FocusInEvent>(OnFocusInSkip);
		_skipButton.RegisterCallback<FocusOutEvent>(OnFocusOutSkip);
	}

	public void SkipCutscene() {
		SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
	}

	private void OnFocusInSkip(FocusInEvent evt) {
		_skipButton.style.unityBackgroundImageTintColor = new Color(255f, 255f, 255f, .8f);
	}

	private void OnFocusOutSkip(FocusOutEvent evt) {
		_skipButton.style.unityBackgroundImageTintColor = new Color(255f, 255f, 255f, 1f);
	}
}
