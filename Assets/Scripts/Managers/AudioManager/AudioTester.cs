using UnityEngine;

public class AudioTester : MonoBehaviour {
	public string Sound;

	private void Update() {
#if UNITY_EDITOR
		if (Input.GetKeyDown(KeyCode.LeftBracket)) {
			EventBus.Instance.TriggerEvent(EventType.AUDIO_PLAY, (Sound, 10f));
		}
		if (Input.GetKeyDown(KeyCode.RightBracket)) {
			EventBus.Instance.TriggerEvent(EventType.AUDIO_STOP, (Sound, 5f));
		}
		if (Input.GetKeyDown(KeyCode.Backslash)) {
			EventBus.Instance.TriggerEvent(EventType.AUDIO_STOP_ALL);
		}
#endif
	}
}