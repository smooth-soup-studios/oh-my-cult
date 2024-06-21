using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour {
	private Animator _animator;
	public VibrationManager VibrationManager;


	void Start() {
		_animator = FindAnyObjectByType<Animator>();
		StartCoroutine(PlayCutscene());

	}

	public IEnumerator PlayCutscene() {
		float duration = _animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
		VibrationManager.StopAllLayers();
		VibrationManager.VibrationEnabled = false;
		yield return new WaitForSeconds(duration);
		VibrationManager.VibrationEnabled = true;

		BlackFadeManager.Instance.Blacken(1f);
		yield return new WaitForSeconds(1f);
		BlackFadeManager.Instance.UnblackenOnLoad = true;

		SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
	}

}
