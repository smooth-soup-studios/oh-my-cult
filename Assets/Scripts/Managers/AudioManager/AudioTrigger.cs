using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioTrigger : MonoBehaviour {
	[SerializeField] private bool _stopWhenExit = false;
	[SerializeField] private StringFloatCombo[] _playOnSceneLoad;

	[SerializeField] private StringFloatCombo[] _playOnPlayerEnter;

	private void Start() {
		_playOnSceneLoad.ToList().ForEach(sound => {
			EventBus.Instance.TriggerEvent(EventType.AUDIO_PLAY, (sound.Name, sound.Fade));
		});
		SceneManager.sceneUnloaded += OnSceneULoaded;
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag("Player")) {
			_playOnPlayerEnter.ToList().ForEach(sound => {
				EventBus.Instance.TriggerEvent(EventType.AUDIO_PLAY, (sound.Name, sound.Fade));
			});
		}
	}

	private void OnTriggerExit2D(Collider2D other) {
		if (other.CompareTag("Player") && _stopWhenExit) {
			_playOnPlayerEnter.ToList().ForEach(sound => {
				EventBus.Instance.TriggerEvent(EventType.AUDIO_STOP, (sound.Name, sound.Fade));
			});
		}
	}

	protected void OnSceneULoaded(Scene scene) {
		if (_stopWhenExit) {
			_playOnSceneLoad.ToList().ForEach(sound => {
				EventBus.Instance.TriggerEvent(EventType.AUDIO_STOP, sound.Name);
			});
		}
		SceneManager.sceneUnloaded -= OnSceneULoaded;
	}
}

[Serializable]
struct StringFloatCombo {
	public string Name;
	public float Fade;
}