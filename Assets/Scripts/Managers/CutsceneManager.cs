using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour {
	// Start is called before the first frame update


	void Start() {
		// StartCoroutine(OutroCutscene());
		StartCoroutine(IntroCutscene());
	}

	public IEnumerator IntroCutscene() {
		Logger.Log("Play", "intro");
		yield return new WaitForSeconds(14f);
		SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
	}
	public IEnumerator OutroCutscene() {
		Logger.Log("Play", "Outro");
		yield return new WaitForSeconds(3f);
		SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
