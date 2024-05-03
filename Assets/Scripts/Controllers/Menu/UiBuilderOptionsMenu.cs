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
    Slider _musicVolume;
    Slider _fxVolume;
    DropdownField _quality;
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private GameObject _mainMenuUI;
    [SerializeField] private GameObject _keyBindingUI;
    [SerializeField] private GameObject _optionsUI;
    [SerializeField] private float _baseVolume = 0
    ;
    VisualElement _root;

    // Start is called before the first frame update
    void OnEnable()
    { Logger.Log("OptionsMenu", "Options Menu");
        _root = GetComponent<UIDocument>().rootVisualElement;
        _keyBindingUI.GetComponent<UIDocument>().rootVisualElement.visible = false;

        _keyBindingButton = _root.Q<Button>("KeyBinding");
        _backButton = _root.Q<Button>("Back");
        _masterVolume = _root.Q<Slider>("MasterVolume");
        _musicVolume = _root.Q<Slider>("MusicVolume");
        _fxVolume = _root.Q<Slider>("FXVolume");
        _quality = _root.Q<DropdownField>("QualitySelection");

        _masterVolume.value = _baseVolume;
        OnMasterSound(_baseVolume);
        _musicVolume.value = _baseVolume;
        OnMusicSound(_baseVolume);
        _fxVolume.value = _baseVolume;
        OnFXSound(_baseVolume);

        _keyBindingButton.clicked += OnKeyBinding;
        _backButton.clicked += OnBack;
        _masterVolume.RegisterCallback<ChangeEvent<float>>((evt) =>
        {
            OnMasterSound(evt.newValue);
        });
        _musicVolume.RegisterCallback<ChangeEvent<float>>((evt) =>
        {
            OnMusicSound(evt.newValue);
        });
        _fxVolume.RegisterCallback<ChangeEvent<float>>((evt) =>
        {
            OnFXSound(evt.newValue);
        });
        _quality.RegisterCallback<ChangeEvent<int>>((evt) =>
        {
            OnQuality(evt.newValue);
        });
    }

    void OnKeyBinding(){
        Logger.Log("OptionsMenu", "Viewing Key Binding");
        _optionsUI.GetComponent<UIDocument>().rootVisualElement.visible = false;
        _keyBindingUI.GetComponent<UIDocument>().rootVisualElement.visible = true;
    }

    void OnBack(){
        Logger.Log("OptionsMenu", "Back to Menu");
        _optionsUI.GetComponent<UIDocument>().rootVisualElement.visible = false;
        _mainMenuUI.GetComponent<UIDocument>().rootVisualElement.visible = true;
    }

    void OnMasterSound(float volume) {
        _audioMixer.SetFloat("masterVolume", volume);
    }

    void OnMusicSound(float volume) {
        _audioMixer.SetFloat("musicVolume", volume);
    }

    void OnFXSound(float volume) {
        _audioMixer.SetFloat("soundFXVolume", volume);
    }

    void OnQuality(int QualityIndex){
        QualitySettings.SetQualityLevel(QualityIndex);
    }

}
