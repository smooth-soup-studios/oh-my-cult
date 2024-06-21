using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Outro : MonoBehaviour, ISaveable {
	Button _yesButton;
	//Button _noButton;
	VisualElement _root;
	[SerializeField] private GameObject _deadUI;
	[SerializeField] private GameObject _formUI;

	void OnEnable() {
		_root = GetComponent<UIDocument>().rootVisualElement;
		GameObject HUD = GameObject.Find("HUD");
		if (HUD) {
			HUD.GetComponent<UIDocument>().rootVisualElement.visible = false;
		}
		_yesButton = _root.Q<Button>("YesButton");
		_yesButton.clicked += OnYesButton;
		// _noButton = _root.Q<Button>("NoButton");
		// _noButton.clicked += OnNoButton;
	}

	void OnYesButton() {
		SceneManager.LoadSceneAsync(0);
	}
	// void OnNoButton() {
	// 	SceneManager.LoadSceneAsync(0);
	// }

	public void LoadData(GameData data) {
		if (!data.PlayerData.BossDefeated) {
			_formUI.SetActive(false);
			_deadUI.SetActive(true);
		}
	}

	public void SaveData(GameData data) {
	}
}
