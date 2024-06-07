using UnityEditor;
using UnityEngine;

public class CreepEyesController : MonoBehaviour {
	private SpriteRenderer _spriteRenderer;

	public Vector2 SpawnCenter = new(0, 0);

	// Everything below this value is definitely spawned. Afterwards is a gradient.
	public float SpawnRadiusMin = 64f;
	/// Everything above this value is despawned on awake.
	public float SpawnRadiusMax = 4096f;

	[Range(0f, 2f)]
	public float SpawnChanceFactor = 0.5f;
	public float OscillateTime = 10f;

	private float _oscillateOffset = 0f;

	private void Awake() {
		float dist = Vector2.Distance(SpawnCenter, transform.position);
		if (dist > SpawnRadiusMax) {
			Destroy(gameObject);
		}

		float normalisedDist = Mathf.InverseLerp(SpawnRadiusMin, SpawnRadiusMax, dist);

		if (Random.Range(0f, 1f) * SpawnChanceFactor < normalisedDist) {
			Destroy(gameObject);
		}

		_spriteRenderer = GetComponent<SpriteRenderer>();
		_oscillateOffset = Random.Range(0f, Mathf.PI * 2f);

		transform.position += new Vector3(Random.Range(-2f, 2f), 5 + Random.Range(-2f, 2f));
	}

	private void Update() {
		float opacity = Mathf.Sin(Time.time / OscillateTime * Mathf.PI + _oscillateOffset) * 0.5f + 0.5f;
		_spriteRenderer.color = new Color(opacity, opacity, opacity, opacity);
	}

#if UNITY_EDITOR
	private void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(SpawnCenter, SpawnRadiusMin);
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(SpawnCenter, SpawnRadiusMax);

		float dist = Vector2.Distance(SpawnCenter, transform.position);
		float normalisedDist = Mathf.Lerp(0f, 1f, Mathf.InverseLerp(SpawnRadiusMin, SpawnRadiusMax, dist));

		Handles.color = Color.red;
		Handles.Label(transform.position, $"nd: {normalisedDist}");
	}
#endif
}