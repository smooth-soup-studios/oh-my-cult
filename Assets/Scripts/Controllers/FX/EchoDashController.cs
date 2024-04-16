using System.Collections;
using UnityEngine;

public class EchoDashController : MonoBehaviour {
	public GameObject EchoGhostPrefab;
	public float EchoDelay = 0.05f;
	public float EchoLifetime = 0.5f;

	bool _doEcho = false;

	IEnumerator EchoCoroutine() {
		while (_doEcho) {
			GameObject echo = Instantiate(EchoGhostPrefab, transform.position, transform.rotation);

			echo.GetComponent<EchoGhostController>().Lifetime = EchoLifetime;

			SpriteRenderer echoRenderer = echo.GetComponent<SpriteRenderer>();
			SpriteRenderer playerRenderer = GetComponent<SpriteRenderer>();

			echoRenderer.sprite = playerRenderer.sprite;
			echoRenderer.transform.localScale = playerRenderer.transform.localScale;
			echoRenderer.flipX = playerRenderer.flipX;
			echoRenderer.flipY = playerRenderer.flipY;
			echoRenderer.color = new Color(1, 1, 1, 0.5f);

			yield return new WaitForSeconds(EchoDelay);
		}
	}

	public void StartEcho() {
		_doEcho = true;

		StartCoroutine(EchoCoroutine());
	}

	public void StopEcho() {
		StopCoroutine(EchoCoroutine());

		_doEcho = false;
	}
}