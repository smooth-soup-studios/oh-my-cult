using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class TempKeyBindingIntro : MonoBehaviour {
	Button _continueButton;
	VisualElement _root;
	// Start is called before the first frame update
	void Start() {
		_root = GetComponent<UIDocument>().rootVisualElement;
		_continueButton = _root.Q<Button>("ContinueButton");
		_continueButton.clicked += OnStartButton;
	}

	void OnStartButton() {
		SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
