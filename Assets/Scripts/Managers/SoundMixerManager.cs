using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour {
	[SerializeField] AudioMixer _audioMixer;

	public void SetMasterVolume(float level) {
		_audioMixer.SetFloat("masterVolume", level);
	}

	public void SetSoundFXVolume(float level) {
		_audioMixer.SetFloat("soundFXVolume", level);
	}

	public void SetMusicVolume(float level) {
		_audioMixer.SetFloat("musicVolume", level);
	}
}