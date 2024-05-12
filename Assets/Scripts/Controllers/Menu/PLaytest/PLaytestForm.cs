using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlaytestForm : MonoBehaviour
{
    Button _yesButton;
    Button _noButton;
    VisualElement _root;
    // Start is called before the first frame update
    void Start()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;
        _yesButton = _root.Q<Button>("YesButton");
        _yesButton.clicked += OnYesButton;
        _noButton = _root.Q<Button>("NoButton");
        _noButton.clicked += OnNoButton;
    }

    void OnYesButton(){
        Logger.Log("Forms", "Opening the form");
        Application.OpenURL("https://forms.gle/xaRoFbseE2cSS3J69");
    }

    void OnNoButton(){
        SceneManager.LoadSceneAsync(0);
    }
}
