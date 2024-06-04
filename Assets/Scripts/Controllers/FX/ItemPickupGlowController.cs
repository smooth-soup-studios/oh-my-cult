using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
public class ItemPickupGlowController : MonoBehaviour {
	private Light2D _glowLight;

	public float GlowDuration = 1f;
	public float GlowRange = 20f;

	private float _glowFrom = 0f;
	private float _glowTo = 1f;
	private float _glowStart = 1f;

	void Awake() {
		_glowLight = GetComponent<Light2D>();
		_glowFrom = _glowLight.pointLightOuterRadius;
	}

	void Update() {
		_glowLight.pointLightOuterRadius = Mathf.Lerp(_glowFrom, _glowTo, (Time.time - _glowStart) / GlowDuration);
	}

	public void StartGlow() {
		_glowFrom = _glowLight.pointLightOuterRadius;
		_glowStart = Time.time;
		_glowTo = GlowRange;
	}

	public void StopGlow() {
		_glowFrom = _glowLight.pointLightOuterRadius;
		_glowStart = Time.time;
		_glowTo = 0f;
	}
}
