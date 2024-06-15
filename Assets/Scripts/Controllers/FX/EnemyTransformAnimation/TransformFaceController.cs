using UnityEngine;

public class TransformFaceController : MonoBehaviour {
	[HideInInspector]
	public TimedTween TotalTween;

	private SpriteRenderer _spriteRenderer;

	private void Awake() {
		_spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void Start() {
		Update();
	}

	private readonly float _firstHalfDurationMult = .75f;
	private float _secondHalfDurationMult { get => 1 - _firstHalfDurationMult; }
	private float _d1 { get => TotalTween.Duration * _firstHalfDurationMult; }
	private float _d2 { get => TotalTween.Duration * _secondHalfDurationMult; }
	private float _d { get => TotalTween.Progress < _firstHalfDurationMult ? _d1 : _d2; }
	private float _ts { get => TotalTween.Progress < _firstHalfDurationMult ? TotalTween.TStart : TotalTween.TStart + _d1; }
	private readonly float _scaleMid = .8f;

	private void Update() {
		TimedTween scaleTween = new() { Duration = _d, From = .5f, To = _scaleMid, TStart = _ts, EasingFunction = Easings.EaseOutCubic };
		TimedTween opacityTween = new() { Duration = _d, From = 0, To = 1, TStart = _ts, EasingFunction = Easings.EaseOutCubic };

		if (TotalTween.Progress > _firstHalfDurationMult) {
			scaleTween.From = _scaleMid;
			scaleTween.To = .75f;
			scaleTween.EasingFunction = opacityTween.EasingFunction = Easings.EaseInCubic;

			opacityTween.From = 1;
			opacityTween.To = 0;
		}

		_spriteRenderer.color = new Color(1, 1, 1, opacityTween.GetClamped());
		transform.localScale = new Vector3(scaleTween.GetClamped(), scaleTween.GetClamped(), 1);
	}
}
