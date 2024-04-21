using System.Collections;
using UnityEngine;

namespace Managers {
public class AudioManager : MonoBehaviour {
	public static AudioManager Instance;
	[SerializeField] AudioSource _soundObject;
	[SerializeField] AudioSource _musicObject;

	void Awake() {
		if (Instance != null && Instance != this) {
			Destroy(gameObject);
			return;
		}

		Instance = this;
		DontDestroyOnLoad(gameObject);
	}

	/// <summary> Plays a short SFX.
	/// <br/>
	/// 	<para>
	/// 		Usage:
	/// 		<example>
	/// 			<c> PlayClip(grassFootstep, player.transform, 1f) </c>
	/// 		</example>
	/// 	</para>
	/// </summary>
	/// <param name="audioClip">The name of the audioclip that should be played;</param>
	/// <param name="spawnTransform">The location where the source of the sound should be;</param>
	/// <param name="volume">The loudness of the SFX;</param>
	public void PlayClip(AudioClip audioClip, Transform spawnTransform, float volume) {
		AudioSource audioSource = Instantiate(_soundObject, spawnTransform.position, Quaternion.identity);
		audioSource.clip = audioClip;
		audioSource.volume = volume;
		audioSource.Play();
		float clipLength = audioSource.clip.length;

		Destroy(audioSource.gameObject, clipLength);
	}

	IEnumerator HandleMusicLoop(AudioSource source, float startLoop, float endLoop) {
		while (source.isPlaying) {
			if (source.time >= endLoop) {
				source.time = startLoop;
			}
			yield return null;
		}
	}

	/// <summary> Plays a new track after fading out from the current track, then proceed looping a section of the track.
	/// <br/>
	/// 	<para>
	/// 		Usage:
	/// 		<example>
	/// 			<c> PlayTrack(churchMusic, 1f, 1f, 0f, 33f) </c>
	/// 		</example>
	/// 	</para>
	/// </summary>
	/// <param name="newClip">The clip to be played;</param>
	/// <param name="fadeOutDuration">The time in seconds it takes to fade out the current track;</param>
	/// <param name="fadeInDuration">The time in seconds it takes to fade in the new track;</param>
	/// <param name="startLoop">The time in the track the new track should start at;</param>
	/// <param name="endLoop">The time in the track the new track should return to the start of the loop;</param>
	public void PlayTrack(AudioClip newClip, float fadeOutDuration, float fadeInDuration, float startLoop, float endLoop) {
		StartCoroutine(FadeTransition(_musicObject, newClip, fadeOutDuration, fadeInDuration, startLoop, endLoop));
	}

	IEnumerator FadeTransition(AudioSource musicSource, AudioClip newClip, float fadeOutDuration, float fadeInDuration, float startLoop, float endLoop) {
		yield return StartCoroutine(FadeVolume(musicSource, fadeOutDuration, 0f));

		musicSource.clip = newClip;
		musicSource.time = startLoop;
		musicSource.Play();
		yield return StartCoroutine(FadeVolume(musicSource, fadeInDuration, 1f));

		StartCoroutine(HandleMusicLoop(musicSource, startLoop, endLoop));
	}

	IEnumerator FadeVolume(AudioSource source, float duration, float targetVolume) {
		float startVolume = source.volume;
		float elapsed = 0;

		while (elapsed < duration) {
			elapsed += Time.deltaTime;
			source.volume = Mathf.Lerp(startVolume, targetVolume, elapsed / duration);
			yield return null;
		}
		source.volume = targetVolume;
	}
}
}