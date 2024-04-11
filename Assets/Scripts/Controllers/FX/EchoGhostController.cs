using UnityEngine;

public class EchoGhostController : MonoBehaviour {
	[HideInInspector] public float Lifetime = 0.5f;
	private float _startTime;

	void Start() {
		_startTime = Time.time;
		Destroy(gameObject, Lifetime);
	}

	void Update() {
		if (GetComponent<SpriteRenderer>().color.a > 0) {
			GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Mathf.Lerp(0.5f, 0, (Time.time - _startTime) / Lifetime));
		}
	}
}