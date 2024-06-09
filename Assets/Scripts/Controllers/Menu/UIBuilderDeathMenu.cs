using System;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UIBuilderDeathMenu : MonoBehaviour
{
    Button _yesButton;
    Button _noButton;
    VisualElement _root;

    GameObject _player;

    void OnEnable()
    {
        EventBus.Instance.Subscribe<GameObject>(EventType.DEATH, OnDeath);
        
        _root = GetComponent<UIDocument>().rootVisualElement;

        _yesButton = _root.Q<Button>("YesButton");
        _yesButton.clicked += OnYesButton;
        _noButton = _root.Q<Button>("NoButton");
        _noButton.clicked += OnNoButton;

        _player = GameObject.Find("Player");
    }

	void OnDeath(GameObject target){
        VisualElement death = _root.Q<VisualElement>("Container");

        if(target == _player){
            if(death != null){
                death.visible = true;
                Time.timeScale = 0;
                GameObject.Find("HUD").GetComponent<UIDocument>().rootVisualElement.visible = false;
            }else{
                // Return to main menu?
                SceneManager.LoadSceneAsync(0);
            }
        }
    }

    void OnYesButton(){
        // Reset the save/scene?
        // Loads the default savestate, overwriting existing files.

        SaveManager.Instance.ChangeSelectedProfileId("1");
		SaveManager.Instance.NewGame();
		SaveManager.Instance.SaveGame();


        // Load the current scene
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
        Debug.Log("Reload scene");
    }
    void OnNoButton(){
        SceneManager.LoadSceneAsync(0);
        Time.timeScale = 1;
        Debug.Log("Main Menu");
    }
}
