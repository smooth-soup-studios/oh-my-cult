using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {
	private static readonly string _logName = "AudioManager";
	[SerializeField] protected List<SoundObject> Sounds;

	[Header("Mixer Groups")]
	public AudioMixerGroup MasterMixer;
	public AudioMixerGroup MusicMixer;
	public AudioMixerGroup FXMixer;

	[Header("Debug settings")]
	[Tooltip("Update generated AudioSources when their SoundObjects are updated. Will mess up crossfading!")]
	[SerializeField] private bool _updateGeneratedSources = false;

	private Dictionary<string, Coroutine> _runningCoroutines = new();

	private static AudioManager _audioManager;
	public static AudioManager Instance {
		get {
			if (!_audioManager) {
				_audioManager = FindAnyObjectByType<AudioManager>();

				if (!_audioManager) {
					Logger.LogError(_logName, "No AudioManager found in the scene!");
				}
			}
			return _audioManager;
		}
	}

	protected void Awake() {
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
		EventBus.Instance.Subscribe<string>(EventType.AUDIO_PLAY, e => Play(e));
		EventBus.Instance.Subscribe<string>(EventType.AUDIO_STOP, e => Stop(e));
		EventBus.Instance.Subscribe<(string Name, float Duration)>(EventType.AUDIO_PLAY, e => Play(e.Name, e.Duration));
		EventBus.Instance.Subscribe<(string Name, float Duration)>(EventType.AUDIO_STOP, e => Stop(e.Name, e.Duration));
		EventBus.Instance.Subscribe<AudioType>(EventType.AUDIO_STOP_ALL, StopAll);
		EventBus.Instance.Subscribe(EventType.AUDIO_STOP_ALL, StopAll);


		UpdateAudioComponents(true);
	}

	protected void UpdateAudioComponents(bool firstUpdate = false) {
		Sounds.ForEach(sound => {
			// Ensure the sound has a valid name
			if (string.IsNullOrEmpty(sound.ClipName) && sound.Clip != null) {
				sound.ClipName = sound.Clip.name;
			}
			// Load settings into the AudioSource
			if (sound.Source == null) {
				sound.Source = gameObject.AddComponent<AudioSource>();
			}
			sound.Source.outputAudioMixerGroup = ConvertAudioTypeToMixer(sound.SoundType);
			sound.Source.clip = sound.Clip;
			sound.Source.volume = sound.Volume;
			sound.Source.pitch = sound.Pitch;
			sound.Source.playOnAwake = sound.PlayOnAwake;
			sound.Source.loop = sound.Loop;

			// Play the startup sounds
			if (sound.PlayOnAwake && firstUpdate) {
				sound.Source.Play();
			}
		});
	}

	protected void Update() {
		// Allows using the sliders in the list instead of manually looking for the AudioSource
		if (_updateGeneratedSources) {
			UpdateAudioComponents();
		}
	}

	public void AddSound(SoundObject sound) {
		if (!Sounds.Contains(sound)) {
			Sounds.Add(sound);
		}
	}
	public void RemoveSound(SoundObject sound) {
		if (Sounds.Contains(sound)) {
			Sounds.Remove(sound);
		}
	}

	protected void Play(string clipName, float duration = 0f) {
		SoundObject sound = Sounds.Find(s => s.ClipName == clipName);
		if (sound == null) {
			Logger.LogWarning(_logName, "Sound with name " + clipName + " not found!");
			return;
		}
		StopRunningCoroutines(clipName);
		_runningCoroutines[clipName] = StartCoroutine(FadeIn(sound, duration));
		sound.Source.Play();
	}

	protected void Stop(string clipName, float duration = 0f) {
		SoundObject sound = Sounds.Find(s => s.ClipName == clipName);
		if (sound == null) {
			Logger.LogWarning(_logName, "Sound with name " + clipName + " not found!");
			return;
		}
		_runningCoroutines[clipName] = StartCoroutine(FadeOut(sound, duration));
	}




	protected void StopAll() {
		StopAllCoroutines();
		Sounds.ForEach(sound => {
			Stop(sound.ClipName);
			sound.Source.volume = sound.Volume;
		});
	}

	protected void StopAll(AudioType type) {
		Sounds.Where(sound => sound.SoundType == type).ToList().ForEach(sound => {
			Stop(sound.ClipName);
		});
	}



	#region Coroutines
	IEnumerator FadeIn(SoundObject sound, float duration) {
		// Set audio output volume to 0
		sound.Source.volume = 0;

		// Increase the volume until it is above equal to the target.
		while (sound.Source.volume < sound.Volume) {
			sound.Source.volume += Time.deltaTime / duration;
			yield return null;
		}

		// Ensure the volume is set to the target volume
		sound.Source.volume = sound.Volume;
		_runningCoroutines[sound.ClipName] = null;
	}

	IEnumerator FadeOut(SoundObject sound, float duration) {
		float startVolume = sound.Volume;

		while (sound.Source.volume > 0) {
			sound.Source.volume -= startVolume * Time.deltaTime / duration;
			yield return null;
		}

		sound.Source.Stop();
		sound.Source.volume = startVolume;
		_runningCoroutines[sound.ClipName] = null;
	}

	private void StopRunningCoroutines(string name) {
		if (_runningCoroutines.TryGetValue(name, out Coroutine coroutine)) {
			if (coroutine != null) {
				StopCoroutine(coroutine);
			}
		}
	}

	#endregion

	private AudioMixerGroup ConvertAudioTypeToMixer(AudioType type) {
		return type switch {
			AudioType.Master => MasterMixer,
			AudioType.Music => MusicMixer,
			AudioType.FX => FXMixer,
			_ => null,
		};
	}

}