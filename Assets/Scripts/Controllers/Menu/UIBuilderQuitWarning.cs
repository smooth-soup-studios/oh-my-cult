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
    }

    void OnYesButton(){
        GameManager.QuitGame();
    }
    void OnNoButton(){
        _quitWarning.visible = false;
    }
}
