using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Outro : MonoBehaviour, ISaveable
{
    Button _yesButton;
    Button _noButton;
    VisualElement _root;
    [SerializeField] private GameObject _deadUI;
    [SerializeField] private GameObject _formUI;
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
        SceneManager.LoadSceneAsync(3);
    }
    void OnNoButton(){
        _deadUI.SetActive(false);
        _formUI.SetActive(true);
    }

	public void LoadData(GameData data) {
        if(!data.PlayerData.BossDefeated){
            _formUI.SetActive(false);
            _deadUI.SetActive(true);
        }
		throw new System.NotImplementedException();
	}

	public void SaveData(GameData data) {
	}
}
