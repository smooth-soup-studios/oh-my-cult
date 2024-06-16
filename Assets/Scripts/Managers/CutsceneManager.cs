using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour {
	private Animator _animator;


	void Start() {
		_animator = FindAnyObjectByType<Animator>();
		StartCoroutine(PlayCutscene());
	}

	public IEnumerator PlayCutscene() {
		float duration = _animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
		yield return new WaitForSeconds(duration);
		SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
	}

}
