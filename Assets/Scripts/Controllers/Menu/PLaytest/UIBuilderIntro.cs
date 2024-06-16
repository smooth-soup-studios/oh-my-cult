using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UIBuilderIntro : MonoBehaviour
{
    Button _startButton;
    VisualElement _root;
    // Start is called before the first frame update
    void Start()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;
        _startButton = _root.Q<Button>("StartButton");
        _startButton.clicked += OnStartButton;
    }

    void OnStartButton(){
        SceneManager.LoadSceneAsync(SceneDefs.IntroCutscene);
    }
}
