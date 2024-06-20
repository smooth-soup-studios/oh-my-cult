using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour {
	private Animator _animator;
	public VibrationManager VibrationManager;


	void Start() {
		_animator = FindAnyObjectByType<Animator>();
		VibrationManager.VibrationEnabled = false;
		StartCoroutine(PlayCutscene());

	}

	public IEnumerator PlayCutscene() {
		float duration = _animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
		yield return new WaitForSeconds(duration);
		VibrationManager.VibrationEnabled = true;
		VibrationManager.StopAllLayers();
		SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
	}

}
