using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {
	private static readonly string _logName = "AudioManager";
	public List<SoundObject> Sounds;
	private static AudioManager _audioManager;
	public static AudioManager Instance { get { return _audioManager; } }

	private void Awake() {
		if (_audioManager == null) {
			_audioManager = this;
		}
		else if (_audioManager != this) {
			Logger.LogWarning(_logName, "Multiple Instances found! Exiting...");
			Destroy(gameObject);
			return;
		}
		DontDestroyOnLoad(Instance);

		// Eventbus Integration
		EventBus.Instance.Subscribe<string>(EventType.AUDIO_PLAY, Play);
		EventBus.Instance.Subscribe<string>(EventType.AUDIO_STOP, Stop);
		EventBus.Instance.Subscribe(EventType.AUDIO_STOP_ALL, StopAll);

		UpdateAudioComponents();
	}

	protected void UpdateAudioComponents() {
		Sounds.ForEach(sound => {
			// Ensure the sound has a valid name
			if (string.IsNullOrEmpty(sound.ClipName) && sound.Clip != null) {
				sound.ClipName = sound.Clip.name;
			}
			// Load settings into the AudioSource
			if (sound.Source == null) {
				sound.Source = gameObject.AddComponent<AudioSource>();
			}
			sound.Source.clip = sound.Clip;
			sound.Source.volume = sound.Volume;
			sound.Source.pitch = sound.Pitch;
			sound.Source.loop = sound.Loop;
		});
	}

	private void Update() {
		// Allows using the sliders in the list instead of manually looking for the AudioSource
		UpdateAudioComponents();
	}


	protected void Play(string clipName) {
		SoundObject sound = Sounds.Find(s => s.ClipName == clipName);
		if (sound == null) {
			Logger.LogWarning(_logName, "Sound with name " + clipName + " not found!");
			return;
		}
		sound.Source.Play();
	}

	protected void Stop(string clipName) {
		SoundObject sound = Sounds.Find(s => s.ClipName == clipName);
		if (sound == null) {
			Logger.LogWarning(_logName, "Sound with name " + clipName + " not found!");
			return;
		}
		// stop playback and reset to zero
		sound.Source.Stop();
		sound.Source.time = 0;
	}

	protected void StopAll() {
		Sounds.ForEach(sound => {
			Stop(sound.ClipName);
		});
	}
}