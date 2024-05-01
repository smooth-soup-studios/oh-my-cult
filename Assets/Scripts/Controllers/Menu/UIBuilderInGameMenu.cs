using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UIBuilderInGameMenu : MonoBehaviour
{
	private Button _continueButton;
	private Button _loadGameButton;
	private Button _optionsButton;
	private Button _quit;

	[SerializeField] private GameObject _inGameUI;
	[SerializeField] private GameObject _optionsUI;

	private string _lastSceneLoaded = "level_0";
	VisualElement _root;
    VisualElement _hud;

	private void OnEnable() {
        EventBus.Instance.Subscribe(EventType.PAUSE, OnPause);
		_root = GetComponent<UIDocument>().rootVisualElement;
        _hud = GameObject.Find("HUD").GetComponent<UIDocument>().rootVisualElement;

		_continueButton = _root.Q<Button>("ContinueButton");
		_loadGameButton = _root.Q<Button>("LoadButton");
		_optionsButton = _root.Q<Button>("OptionsButton");
		_quit = _root.Q<Button>("QuitButton");

		_continueButton.clicked += OnContinue;
		//_loadGameButton.clicked += LoadData;
		_optionsButton.clicked += OnOptions;
		_quit.clicked += QuitGame;
		DisableButtons();

	}

	public void OnContinue() {
        _hud.visible = true;
        _inGameUI.GetComponent<UIDocument>().rootVisualElement.visible = false;
        Time.timeScale = 1;
	}

	public void QuitGame() {
		Logger.Log("MenuController", "THE MENU IS DEAD!");
		//Application.Quit();
	}

	public void DisableButtons() {
		if (!SaveManager.Instance.HasGameData()) {
			_loadGameButton.style.unityBackgroundImageTintColor = new Color(255f, 255f, 255f, .5f);
		}
	}

	public void LoadData(GameData data) {
		if (!(data.PlayerData.SceneName == "" || data.PlayerData.SceneName == null)) {
			_lastSceneLoaded = data.PlayerData.SceneName;
		}
	}

	void OnOptions() {
		Logger.Log("InGameController", "Viewing options");
		_inGameUI.GetComponent<UIDocument>().rootVisualElement.visible = false;
		_optionsUI.GetComponent<UIDocument>().rootVisualElement.visible = true;
	}

    void OnPause(){
        Logger.Log("InGameMenu", "On Pause2");  //Test to see if the menu becomes visible
        if(Time.timeScale == 1){
            _hud.visible = false;
            _inGameUI.GetComponent<UIDocument>().rootVisualElement.visible = true;
            Time.timeScale = 0;
        }
        else if(Time.timeScale == 0){
            _hud.visible = true;
            _inGameUI.GetComponent<UIDocument>().rootVisualElement.visible = false;
            Time.timeScale = 1;
        }
    }
}
