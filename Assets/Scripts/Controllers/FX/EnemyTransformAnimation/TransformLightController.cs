using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TransformLightController : MonoBehaviour {
	[HideInInspector]
	public TimedTween TotalTween;

	private Light2D _light;

	private void Awake() {
		_light = GetComponent<Light2D>();
	}

	private void Start() {
		Update();
	}

	private readonly float _firstHalfDurationMult = .5f;
	private float _secondHalfDurationMult { get => 1 - _firstHalfDurationMult; }
	private float _d1 { get => TotalTween.Duration * _firstHalfDurationMult; }
	private float _d2 { get => TotalTween.Duration * _secondHalfDurationMult; }
	private float _d { get => TotalTween.Progress < _firstHalfDurationMult ? _d1 : _d2; }
	private float _ts { get => TotalTween.Progress < _firstHalfDurationMult ? TotalTween.TStart : TotalTween.TStart + _d1; }
	private readonly float _rangeMid = 5f;
	private readonly float _intensityMid = 5f;

	private void Update() {
		TimedTween rangeTween = new() { Duration = _d, From = _light.pointLightInnerRadius, To = _rangeMid, TStart = _ts, EasingFunction = Easings.EaseOutCubic };
		TimedTween intensityTween = new() { Duration = _d, From = 0, To = _intensityMid, TStart = _ts, EasingFunction = Easings.EaseOutCubic };

		if (TotalTween.Progress > _firstHalfDurationMult) {
			rangeTween.From = _rangeMid;
			rangeTween.To = _rangeMid * .5f;
			rangeTween.EasingFunction = intensityTween.EasingFunction = Easings.EaseInCubic;

			intensityTween.From = _intensityMid;
			intensityTween.To = 0;
		}

		_light.pointLightOuterRadius = rangeTween.GetClamped();
		_light.intensity = intensityTween.GetClamped();
	}
}
