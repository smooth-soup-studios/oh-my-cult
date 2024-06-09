using System.Linq;
using UnityEngine;

public class AudioTrigger : MonoBehaviour {
	[SerializeField] private bool _stopWhenExit = false;
	[SerializeField] private string[] _playOnPlayerEnter;
	private void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag("Player")) {
			_playOnPlayerEnter.ToList().ForEach(sound => {
				EventBus.Instance.TriggerEvent(EventType.AUDIO_PLAY, sound);
			});
		}
	}

	private void OnTriggerExit2D(Collider2D other) {
		if (other.CompareTag("Player") && _stopWhenExit) {
			_playOnPlayerEnter.ToList().ForEach(sound => {
				EventBus.Instance.TriggerEvent(EventType.AUDIO_STOP, sound);
			});
		}
	}
}