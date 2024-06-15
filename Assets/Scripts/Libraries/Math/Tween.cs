using System;
using UnityEngine;

public class Tween {
	public float From = 0;
	public float To = 0;

	public Func<float, float> EasingFunction = Easings.Linear;

	public float Get(float t) => Mathf.Lerp(From, To, EasingFunction(t));
}

public class TimedTween : Tween {
	public float Duration = 1;
	public float TStart = Time.time;

	public float TEnd {
		get => TStart + Duration;
		set => Duration = value - TStart;
	}

	public float TProgress => Time.time - TStart;

	public float Progress => (Time.time - TStart) / Duration;

	public bool Finished => Time.time >= TEnd;

	public float Get() {
		if (Duration == 0)
			return To;

		float t = Progress;
		return Get(t);
	}

	public float GetClamped() {
		if (Duration == 0)
			return To;

		float t = Mathf.Clamp01(Progress);
		return Get(t);
	}
}