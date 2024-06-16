using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour {
	// Start is called before the first frame update
	public TransportDestination TransportDestination;

	void Start() {
		GameObject plr = GameObject.FindGameObjectWithTag("Player");
		StateMachine plrsm = plr.GetComponent<StateMachine>();
		if () {
			StartCoroutine(ChurchCutscene());
			return;
		}
		// StartCoroutine(OutroCutscene());
		StartCoroutine(IntroCutscene());
	}

	public IEnumerator IntroCutscene() {
		Logger.Log("Play", "Outro");
		yield return new WaitForSeconds(14f);
		SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
	}
	public IEnumerator OutroCutscene() {
		yield return new WaitForSeconds(3f);
		SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
	}
	public IEnumerator ChurchCutscene() {

		yield return new WaitForSeconds(6.8f);
		SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
