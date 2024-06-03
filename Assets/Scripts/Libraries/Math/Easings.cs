using UnityEngine;

/// <summary>
///	Static class containing easing functions for use in tweens.
///	<para/>
///	All functions take a float x in the range [0, 1] and return a float in the range [0, 1] with the easing applied.
/// <para/>
/// Easings from https://easings.net/
/// </summary>
public static class Easings {
	private const float _c1 = 1.70158f;
	private const float _c2 = _c1 * 1.525f;
	private const float _c3 = _c1 + 1;
	private const float _c4 = 2 * Mathf.PI / 3;
	private const float _c5 = 2 * Mathf.PI / 4.5f;
	private const float _n1 = 7.5625f;
	private const float _d1 = 2.75f;

	// Linear
	public static float Linear(float x) => x;

	// Sine
	public static float EaseInSine(float x) => 1 - Mathf.Cos(x * Mathf.PI / 2);
	public static float EaseOutSine(float x) => Mathf.Sin(x * Mathf.PI / 2);
	public static float EaseInOutSine(float x) => -(Mathf.Cos(Mathf.PI * x) - 1) / 2;

	// Quadratic
	public static float EaseInQuad(float x) => x * x;
	public static float EaseOutQuad(float x) => 1 - (1 - x) * (1 - x);
	public static float EaseInOutQuad(float x) => x < 0.5 ? 2 * x * x : 1 - Mathf.Pow(-2 * x + 2, 2) / 2;

	// Cubic
	public static float EaseInCubic(float x) => x * x * x;
	public static float EaseOutCubic(float x) => 1 - Mathf.Pow(1 - x, 3);
	public static float EaseInOutCubic(float x) => x < 0.5 ? 4 * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 3) / 2;

	// Quartic
	public static float EaseInQuart(float x) => x * x * x * x;
	public static float EaseOutQuart(float x) => 1 - Mathf.Pow(1 - x, 4);
	public static float EaseInOutQuart(float x) => x < 0.5 ? 8 * x * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 4) / 2;

	// Quintic
	public static float EaseInQuint(float x) => x * x * x * x * x;
	public static float EaseOutQuint(float x) => 1 - Mathf.Pow(1 - x, 5);
	public static float EaseInOutQuint(float x) => x < 0.5 ? 16 * x * x * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 5) / 2;

	// Exponential
	public static float EaseInExpo(float x) => x == 0 ? 0 : Mathf.Pow(2, 10 * x - 10);
	public static float EaseOutExpo(float x) => x == 1 ? 1 : 1 - Mathf.Pow(2, -10 * x);
	public static float EaseInOutExpo(float x) => x == 0
		? 0
		: x == 1
		? 1
		: x < 0.5 ? Mathf.Pow(2, 20 * x - 10) / 2
		: (2 - Mathf.Pow(2, -20 * x + 10)) / 2;

	// Circular
	public static float EaseInCirc(float x) => 1 - Mathf.Sqrt(1 - Mathf.Pow(x, 2));
	public static float EaseOutCirc(float x) => Mathf.Sqrt(1 - Mathf.Pow(x - 1, 2));
	public static float EaseInOutCirc(float x) => x < 0.5
		? (1 - Mathf.Sqrt(1 - Mathf.Pow(2 * x, 2))) / 2
		: (Mathf.Sqrt(1 - Mathf.Pow(-2 * x + 2, 2)) + 1) / 2;

	// Back
	public static float EaseInBack(float x) => _c3 * x * x * x - _c1 * x * x;
	public static float EaseOutBack(float x) => 1 + _c3 * Mathf.Pow(x - 1, 3) + _c1 * Mathf.Pow(x - 1, 2);
	public static float EaseInOutBack(float x) => x < 0.5
		  ? Mathf.Pow(2 * x, 2) * ((_c2 + 1) * 2 * x - _c2) / 2
		  : (Mathf.Pow(2 * x - 2, 2) * ((_c2 + 1) * (x * 2 - 2) + _c2) + 2) / 2;

	// Elastic
	public static float EaseInElastic(float x) => x == 0
		  ? 0
		  : x == 1
		  ? 1
		  : -Mathf.Pow(2, 10 * x - 10) * Mathf.Sin((x * 10 - 10.75f) * _c4);
	public static float EaseOutElastic(float x) => x == 0
		  ? 0
		  : x == 1
		  ? 1
		  : Mathf.Pow(2, -10 * x) * Mathf.Sin((x * 10 - 0.75f) * _c4) + 1;
	public static float EaseInOutElastic(float x) => x == 0
		  ? 0
		  : x == 1
		  ? 1
		  : x < 0.5
		  ? -(Mathf.Pow(2, 20 * x - 10) * Mathf.Sin((20 * x - 11.125f) * _c5)) / 2
		  : Mathf.Pow(2, -20 * x + 10) * Mathf.Sin((20 * x - 11.125f) * _c5) / 2 + 1;

	// Bounce
	public static float EaseInBounce(float x) => 1 - EaseOutBounce(1 - x);
	public static float EaseOutBounce(float x) {
		if (x < 1 / _d1) {
			return _n1 * x * x;
		}
		else if (x < 2 / _d1) {
			return _n1 * (x -= 1.5f / _d1) * x + 0.75f;
		}
		else if (x < 2.5 / _d1) {
			return _n1 * (x -= 2.25f / _d1) * x + 0.9375f;
		}
		else {
			return _n1 * (x -= 2.625f / _d1) * x + 0.984375f;
		}
	}
	public static float EaseInOutBounce(float x) => x < 0.5
		? (1 - EaseOutBounce(1 - 2 * x)) / 2
		: (1 + EaseOutBounce(2 * x - 1)) / 2;
}