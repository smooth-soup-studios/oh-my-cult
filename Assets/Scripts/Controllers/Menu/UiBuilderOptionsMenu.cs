using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class UiBuilderOptionsMenu : MonoBehaviour
{
    [SerializeField] private Button _keyBindingButton;
    [SerializeField] private Button _backButton;
    [SerializeField] private GameObject _mainMenuUI;
    [SerializeField] private GameObject _keyBindingUI;
    [SerializeField] private GameObject _optionsUI;
    VisualElement _root;

    // Start is called before the first frame update
    void OnEnable()
    { Logger.Log("OptionsMenu", "Options Menu");
         _root = GetComponent<UIDocument>().rootVisualElement;
        _keyBindingButton = _root.Q<Button>("KeyBinding");
        _backButton = _root.Q<Button>("Back");

        _keyBindingButton.clicked += OnKeyBinding;
        _backButton.clicked += OnBack;
    }

    void OnKeyBinding(){
        Logger.Log("OptionsMenu", "Viewing Key Binding");
        _optionsUI.SetActive(false);
        _keyBindingUI.SetActive(true);
    }

    void OnBack(){
        Logger.Log("OptionsMenu", "Back to Menu");
        _optionsUI.SetActive(false);
        _mainMenuUI.SetActive(true);
    }

}
