using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FireController : MonoBehaviour
{
	[SerializeField] private Light2D _light2D;
	[Range(1.0f, 15.0f)]
	[SerializeField] private float _minIntensity;
	[Range(1.0f, 15.0f)]
	[SerializeField] private float _maxIntensity;
	private float _random;

    void Start()
    {
		_random = Random.Range(0f, 65535.0f);
    }

    void Update()
    {
		float noise = Mathf.PerlinNoise(_random, Time.time);
		_light2D.intensity = Mathf.Lerp(_minIntensity, _maxIntensity, noise);
    }
}
