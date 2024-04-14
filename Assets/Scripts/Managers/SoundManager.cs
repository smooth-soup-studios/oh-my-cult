using UnityEngine;

namespace Managers {
public class SoundManager : MonoBehaviour {
	public static SoundManager Instance;
	[SerializeField] AudioSource _soundObject;
	void Awake() {
		if (Instance != null && Instance != this) {
			Destroy(gameObject);
			return;
		}

		Instance = this;
		DontDestroyOnLoad(gameObject);
	}

	public void PlayClip(AudioClip audioClip, Transform spawnTransform, float volume) {
		AudioSource audioSource = Instantiate(_soundObject, spawnTransform.position, Quaternion.identity);
		audioSource.clip = audioClip;
		audioSource.volume = volume;
		audioSource.Play();
		float clipLength = audioSource.clip.length;

		Destroy(audioSource.gameObject, clipLength);
	}

}
}