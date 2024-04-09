using System.Collections;
using UnityEngine;

public class EchoDashController : MonoBehaviour {
	public GameObject EchoGhostPrefab;
	public float EchoDelay = 0.05f;
	public float EchoLifetime = 0.5f;

	bool _doEcho = false;

	void Awake() {
		// _playerStateMachine = GetComponent<StateMachine>();
	}

	IEnumerator EchoCoroutine() {
		while (_doEcho) {
			GameObject echo = Instantiate(EchoGhostPrefab, transform.position, transform.rotation);

			echo.GetComponent<EchoGhostController>().Lifetime = EchoLifetime;

			echo.GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
			echo.GetComponent<SpriteRenderer>().transform.localScale = GetComponent<SpriteRenderer>().transform.localScale;
			echo.GetComponent<SpriteRenderer>().flipX = GetComponent<SpriteRenderer>().flipX;
			echo.GetComponent<SpriteRenderer>().flipY = GetComponent<SpriteRenderer>().flipY;
			echo.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);

			yield return new WaitForSeconds(EchoDelay);
		}
	}

	public void StartEcho() {
		_doEcho = true;

		StartCoroutine(EchoCoroutine());
	}

	public void StopEcho() {
		_doEcho = false;
	}
}