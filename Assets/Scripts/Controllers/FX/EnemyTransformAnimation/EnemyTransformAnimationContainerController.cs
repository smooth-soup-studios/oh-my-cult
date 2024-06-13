using UnityEngine;

public class EnemyTransformAnimationContainerController : MonoBehaviour {
	public float Duration { get => _totalTween.Duration; }
	[SerializeField] private TransformCloudController _cloudClockwiseController;
	[SerializeField] private TransformCloudController _cloudCounterClockwiseController;
	[SerializeField] private TransformFaceController _face;
	[SerializeField] private TransformLightController _glow;

	private TimedTween _totalTween;

	void Awake() {
		_totalTween = new() {
			From = 0,
			To = 1,
			Duration = 1.25f,
		};

		_cloudClockwiseController.TotalTween =
		_cloudCounterClockwiseController.TotalTween =
		_face.TotalTween =
		_glow.TotalTween =
		_totalTween;

		Destroy(gameObject, Duration);
	}
}
