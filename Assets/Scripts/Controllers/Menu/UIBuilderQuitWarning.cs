using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.UIElements;

public class UIBuilderQuitWarning : MonoBehaviour
{
    Button _yesButton;
    Button _noButton;
    VisualElement _root;
    VisualElement _quitWarning;


    void OnEnable()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;
    	_quitWarning = GameObject.Find("QuitWarning").GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("Container");

        _yesButton = _root.Q<Button>("YesButton");
        _yesButton.clicked += OnYesButton;
        _noButton = _root.Q<Button>("NoButton");
        _noButton.clicked += OnNoButton;

        _yesButton.RegisterCallback<FocusInEvent>(OnFocusInYes);
		_yesButton.RegisterCallback<FocusOutEvent>(OnFocusOutYes);

		_noButton.RegisterCallback<FocusInEvent>(OnFocusInNo);
		_noButton.RegisterCallback<FocusOutEvent>(OnFocusOutNo);
    }

    void OnYesButton(){
        GameManager.QuitGame();
    }
    void OnNoButton(){
        _quitWarning.visible = false;
    }

    private void OnFocusInYes(FocusInEvent evt) {
		_yesButton.style.unityBackgroundImageTintColor = new Color(204f, 204f, 204f);
	}

	private void OnFocusOutYes(FocusOutEvent evt) {
		_yesButton.style.unityBackgroundImageTintColor = new Color(255f, 255f, 255f);
	}

    private void OnFocusInNo(FocusInEvent evt) {
		_yesButton.style.unityBackgroundImageTintColor = new Color(204f, 204f, 204f);
	}

	private void OnFocusOutNo(FocusOutEvent evt) {
		_yesButton.style.unityBackgroundImageTintColor = new Color(255f, 255f, 255f);
	}
}
