using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FireController : MonoBehaviour {
	[SerializeField] private Light2D _light2D;
	[Range(0.0f, 15.0f)]
	[SerializeField] private float _minIntensity;
	[Range(1.0f, 15.0f)]
	[SerializeField] private float _maxIntensity;
	private float _random;

	private float _spawnTime = 0.0f;

	void Start() {
		_random = Random.Range(0f, 65535.0f);
		_spawnTime = Time.time;
	}

	void Update() {
		// float noise = Mathf.PerlinNoise(_random, Time.time);
		// _light2D.intensity = Mathf.Lerp(_minIntensity, _maxIntensity, noise);
		// Temp fix
		_light2D.intensity = Mathf.Lerp(_minIntensity, _maxIntensity, Mathf.Clamp01((Time.time - _spawnTime) / 1f));
	}
}
