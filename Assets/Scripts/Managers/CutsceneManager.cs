using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour {
	// Start is called before the first frame update
	private enum CutScene {
		Intro = 14, // Name is humanname and number is duration in seconds.
		Church = 5,
		Outro = 3
	}

	[SerializeField] CutScene _cutScene;

	void Start() {
		StartCoroutine(PlayCutscene());
	}

	public IEnumerator PlayCutscene(){
		Logger.Log("Play", _cutScene.HumanName());
		yield return new WaitForSeconds((float)_cutScene);
		SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
	}

	// public IEnumerator IntroCutscene() {
	// 	Logger.Log("Play", "intro");
	// 	yield return new WaitForSeconds(14f);
	// 	SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
	// }
	// public IEnumerator OutroCutscene() {
	// 	Logger.Log("Play", "Outro");
	// 	yield return new WaitForSeconds(3f);
	// 	SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
	// }
}
