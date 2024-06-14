using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Audio;

public class UiBuilderOptionsMenu : MonoBehaviour, ISaveable {
	private static readonly string _logname = "OptionsMenu";
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
	[SerializeField] private float _baseVolume = 0;
	VisualElement _root;

	// Start is called before the first frame update
	void OnEnable() {
		_root = GetComponent<UIDocument>().rootVisualElement;

		_keyBindingButton = _root.Q<Button>("KeyBinding");
		_backButton = _root.Q<Button>("Back");
		_masterVolume = _root.Q<Slider>("MasterVolume");
		_musicVolume = _root.Q<Slider>("MusicVolume");
		_fxVolume = _root.Q<Slider>("FXVolume");
		_quality = _root.Q<DropdownField>("QualitySelection");

		initializeSettings();

		_keyBindingButton.clicked += OnKeyBinding;
		_backButton.clicked += OnBack;
		_masterVolume.RegisterCallback<ChangeEvent<float>>((evt) => {
			OnMasterSound(evt.newValue);
		});
		_musicVolume.RegisterCallback<ChangeEvent<float>>((evt) => {
			OnMusicSound(evt.newValue);
		});
		_fxVolume.RegisterCallback<ChangeEvent<float>>((evt) => {
			OnFXSound(evt.newValue);
		});
		_quality.RegisterValueChangedCallback((evt) => {
			OnQuality(evt.newValue);
		});
	}

	void initializeSettings() {
		_audioMixer.GetFloat("masterVolume", out _baseVolume);
		_masterVolume.value = _baseVolume;

		_audioMixer.GetFloat("musicVolume", out _baseVolume);
		_musicVolume.value = _baseVolume;

		_audioMixer.GetFloat("soundFXVolume", out _baseVolume);
		_fxVolume.value = _baseVolume;

		_quality.choices.Clear();
		_quality.choices.Add("Very Low");
		_quality.choices.Add("Low");
		_quality.choices.Add("Medium");
		_quality.choices.Add("High");
		_quality.choices.Add("Very High");
		_quality.choices.Add("Ultra");
		_quality.index = QualitySettings.GetQualityLevel();
	}

	void OnKeyBinding() {
		Logger.Log(_logname, "Viewing Key Binding");
		_optionsUI.GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("Container").visible = false;
		_keyBindingUI.GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("Container").visible = true;
	}

	void OnBack() {
		Logger.Log(_logname, "Back to Menu");
		SaveManager.Instance.SoftSaveGame();
		_optionsUI.GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("Container").visible = false;
		_mainMenuUI.GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("Container").visible = true;
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

	void OnQuality(string QualityIndex) {
		switch (QualityIndex) {
			case "Very Low":
				QualitySettings.SetQualityLevel(0, true);
				break;
			case "Low":
				QualitySettings.SetQualityLevel(1, true);
				break;
			case "Medium":
				QualitySettings.SetQualityLevel(2, true);
				break;
			case "High":
				QualitySettings.SetQualityLevel(3, true);
				break;
			case "Very High":
				QualitySettings.SetQualityLevel(4, true);
				break;
			case "Ultra":
				QualitySettings.SetQualityLevel(5, true);
				break;
			default:
				QualitySettings.SetQualityLevel(2, true);
				Logger.Log(_logname, "Quality settings set to 'Medium'");
				return;
		}
		Logger.Log(_logname, $"Quality settings set to '{QualityIndex}'");
	}

	void QualityVisual() {

	}

	public void LoadData(GameData data) {
		int QualityLevel = data.PlayerSettings.QualityIndex;
		if (QualityLevel > -1) {
			QualitySettings.SetQualityLevel(QualityLevel, true);
		}
		if (data.PlayerSettings.VolumeValues.ContainsKey($"{_logname}-MasterVolume")) {
			data.PlayerSettings.VolumeValues.TryGetValue($"{_logname}-MasterVolume", out float value);
			_audioMixer.SetFloat("masterVolume", value);
		}
		if (data.PlayerSettings.VolumeValues.ContainsKey($"{_logname}-MusicVolume")) {
			data.PlayerSettings.VolumeValues.TryGetValue($"{_logname}-MusicVolume", out float value);
			_audioMixer.SetFloat("musicVolume", value);
		}
		if (data.PlayerSettings.VolumeValues.ContainsKey($"{_logname}-FXVolume")) {
			data.PlayerSettings.VolumeValues.TryGetValue($"{_logname}-FXVolume", out float value);
			_audioMixer.SetFloat("soundFXVolume", value);
		}
		initializeSettings();
	}

	public void SaveData(GameData data) {
		_audioMixer.GetFloat("masterVolume", out float value);
		data.PlayerSettings.VolumeValues[$"{_logname}-MasterVolume"] = value;
		_audioMixer.GetFloat("musicVolume", out value);
		data.PlayerSettings.VolumeValues[$"{_logname}-MusicVolume"] = value;
		_audioMixer.GetFloat("soundFXVolume", out value);
		data.PlayerSettings.VolumeValues[$"{_logname}-FXVolume"] = value;
		data.PlayerSettings.QualityIndex = QualitySettings.GetQualityLevel();
	}
}
