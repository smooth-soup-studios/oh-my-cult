using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Audio;

public class UiBuilderOptionsMenu : MonoBehaviour
{
    Button _keyBindingButton;
    Button _backButton;
    Slider _masterVolume;
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private GameObject _mainMenuUI;
    [SerializeField] private GameObject _keyBindingUI;
    [SerializeField] private GameObject _optionsUI;
    [SerializeField] private float _baseVolume = 80;
    VisualElement _root;

    // Start is called before the first frame update
    void OnEnable()
    { Logger.Log("OptionsMenu", "Options Menu");
         _root = GetComponent<UIDocument>().rootVisualElement;
        _keyBindingButton = _root.Q<Button>("KeyBinding");
        _backButton = _root.Q<Button>("Back");
        _masterVolume = _root.Q<Slider>("MasterVolume");

        _masterVolume.value = _baseVolume;
        OnMasterSound(_baseVolume);

        _keyBindingButton.clicked += OnKeyBinding;
        _backButton.clicked += OnBack;
        _masterVolume.RegisterCallback<ChangeEvent<float>>((evt) =>
        {
            OnMasterSound(evt.newValue - 80);
        });
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

    void OnMasterSound(float volume) {
        _audioMixer.SetFloat("masterVolume", volume);
    }

}
