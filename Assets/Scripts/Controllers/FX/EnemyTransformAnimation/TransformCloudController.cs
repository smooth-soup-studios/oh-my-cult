using UnityEngine;

public class TransformCloudController : MonoBehaviour {
	public TimedTween TotalTween;
	public float Scale = 1f;
	public float DegreesPerSecond = 180f;
	public bool FlipDirection = false;

	private SpriteRenderer _spriteRenderer;

	private readonly float _firstHalfDurationMult = .25f;
	private float _secondHalfDurationMult { get => 1 - _firstHalfDurationMult; }
	private float _d1 { get => TotalTween.Duration * _firstHalfDurationMult; }
	private float _d2 { get => TotalTween.Duration * _secondHalfDurationMult; }
	private float _d { get => TotalTween.Progress < _firstHalfDurationMult ? _d1 : _d2; }
	private float _ts { get => TotalTween.Progress < _firstHalfDurationMult ? TotalTween.TStart : TotalTween.TStart + _d1; }

	private void Awake() {
		_spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void Start() {
		Update();
	}

	private void Update() {
		TimedTween scaleTween = new() { Duration = _d, From = .5f, To = Scale, TStart = _ts, EasingFunction = Easings.EaseOutCubic };
		TimedTween opacityTween = new() { Duration = _d, From = 0, To = 1, TStart = _ts, EasingFunction = Easings.EaseOutCubic };

		if (TotalTween.Progress > _firstHalfDurationMult) {
			scaleTween.From = Scale;
			scaleTween.To = .75f;
			scaleTween.EasingFunction = opacityTween.EasingFunction = Easings.EaseInCubic;

			opacityTween.From = 1;
			opacityTween.To = 0;
		}

		transform.Rotate(Vector3.forward, DegreesPerSecond * Time.deltaTime * (FlipDirection ? -1 : 1));
		_spriteRenderer.color = new Color(1, 1, 1, opacityTween.GetClamped());
		transform.localScale = new Vector3(scaleTween.GetClamped(), scaleTween.GetClamped(), 1);
	}

	private void OnValidate() {
		transform.localScale = new Vector3(Scale, Scale, 1);
	}
}