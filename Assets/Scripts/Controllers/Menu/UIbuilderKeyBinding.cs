using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIbuilderKeyBinding : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Button _backButton;
    [SerializeField] private GameObject _keyBindingUI;
    [SerializeField] private GameObject _optionsUI;
    VisualElement _root;

    // Start is called before the first frame update
    void OnEnable()
    { Logger.Log("KeyBinding", "Binding Menu");
         _root = GetComponent<UIDocument>().rootVisualElement;
        _backButton = _root.Q<Button>("BackButton");

        _backButton.clicked += OnBack;
    }

    void OnBack(){
        Logger.Log("KeyBinding", "Back to options");
        _keyBindingUI.GetComponent<UIDocument>().rootVisualElement.visible = false;
        _optionsUI.GetComponent<UIDocument>().rootVisualElement.visible = true;
    }
}
