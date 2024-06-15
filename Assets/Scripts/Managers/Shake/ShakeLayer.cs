using System;
using UnityEngine;

/// <summary>
/// Represents a layer of screen shake.
/// <para/>
/// All functions use the ScreenShakeManager.DefaultEasingFunction. All functions without the duration parameter use the ScreenShakeManager.DefaultDuration.
/// <para/>
/// For VibrationManager, amplitude is the low frequency motor and frequency is the high frequency motor.
/// </summary>
[Serializable]
public class ShakeLayer {
	public ShakeLayer() {
		AmpTween = new TimedTween();
		FreqTween = new TimedTween();
	}

	public TimedTween AmpTween { get; private set; }
	public TimedTween FreqTween { get; private set; }
	public string Name = Guid.NewGuid().ToString();
	/// <summary>
	///  If true, the shake will be removed after the duration of both tweens (AmpTween & FreqTween) has passed.
	/// </summary>
	public bool Temporary = false;

	public bool ShouldRemove => Temporary && AmpTween.Finished && FreqTween.Finished;

	/// <summary>
	/// Immediately sets the shake amplitude and frequency.
	/// <para/>
	/// Handy for impacted shakes (high --> low). In such a case, consider also calling `StopShakeRamped` directly after calling this.
	/// </summary>
	/// <param name="amp">The target amplitude of the shake in units</param>
	/// <param name="freq">The target frequency of the shake in Hz</param>
	public void SetShake(float amp, float freq) => SetShakeRamped(amp, freq, 0);

	/// <summary>
	/// Sets the shake amplitude and frequency, ramped over a duration of 500 milliseconds, from the current amplitude and frequency.
	/// </summary>
	/// <param name="amp">The target amplitude of the shake in units</param>
	/// <param name="freq">The target frequency of the shake in Hz</param>
	public void SetShakeRamped(float amp, float freq) => SetShakeRamped(amp, freq, ScreenShakeManager.DefaultDuration);

	/// <summary>
	/// Sets the shake amplitude and frequency, ramped over a duration of `duration` seconds, from the current amplitude and frequency.
	/// <para/>
	/// Handy for chaining shakes.
	/// </summary>
	/// <param name="amp">The target amplitude of the shake in units</param>
	/// <param name="freq">The target frequency of the shake in Hz</param>
	/// <param name="duration">The duration of the ramp in seconds</param>
	public void SetShakeRamped(float amp, float freq, float duration) => SetShakeRamped(AmpTween.GetClamped(), FreqTween.GetClamped(), amp, freq, duration);

	/// <summary>
	/// Sets the shake amplitude and frequency, ramped over a duration of `duration` seconds, from `ampFrom` and `freqFrom`.
	/// <para/>
	/// Handy for impacted shakes (high --> low). In such a case, consider passing `AmbientShakeAmplitude` and `AmbientShakeFrequency` to this function as well.
	/// </summary>
	/// <param name="ampFrom">The starting amplitude of the shake in units</param>
	/// <param name="freqFrom">The starting frequency of the shake in Hz</param>
	/// <param name="ampTo">The target amplitude of the shake in units</param>
	/// <param name="freqTo">The target frequency of the shake in Hz</param>
	/// <param name="duration">The duration of the ramp in seconds</param>
	public void SetShakeRamped(float ampFrom, float freqFrom, float ampTo, float freqTo, float duration) {
		AmpTween.From = ampFrom;
		AmpTween.To = ampTo;
		AmpTween.TStart = Time.time;
		AmpTween.Duration = duration;
		AmpTween.EasingFunction = ScreenShakeManager.DefaultEasingFunction;

		FreqTween.From = freqFrom;
		FreqTween.To = freqTo;
		FreqTween.TStart = Time.time;
		FreqTween.Duration = duration;
		FreqTween.EasingFunction = ScreenShakeManager.DefaultEasingFunction;
	}

	/// <summary>
	/// Immediately stops the screen shake.
	/// </summary>
	public void StopShake() => SetShake(0, 0);

	/// <summary>
	/// Stops the screen shake over a duration of 500 milliseconds.
	/// </summary>
	public void StopShakeRamped() => StopShakeRamped(ScreenShakeManager.DefaultDuration);

	/// <summary>
	/// Stops the screen shake over a duration of `duration` seconds.
	/// </summary>
	/// <param name="duration">The duration of the ramp in seconds</param>
	public void StopShakeRamped(float duration) => SetShakeRamped(0, 0, duration);

	/// <summary>
	/// Sets the shake amplitude and frequency, then stops the shake over a duration of 500 milliseconds.
	/// <para/>
	/// Handy for impacted shakes (high --> low).
	/// </summary>
	/// <param name="amp">The target amplitude of the shake in units</param>
	/// <param name="freq">The target frequency of the shake in Hz</param>
	public void SetShakeThenStop(float amp, float freq) {
		SetShake(amp, freq);
		StopShakeRamped();
	}
	/// <summary>
	/// Sets the shake amplitude and frequency, then stops the shake over a duration of [duration] seconds.
	/// <para/>
	/// Handy for impacted shakes (high --> low).
	/// </summary>
	/// <param name="amp">The target amplitude of the shake in units</param>
	/// <param name="freq">The target frequency of the shake in Hz</param>
	/// <param name="duration">The duration of the ramp in seconds</param>
	public void SetShakeThenStop(float amp, float freq, float duration) {
		SetShake(amp, freq);
		StopShakeRamped(duration);
	}
}

public class AmbientScreenShakeLayer : ShakeLayer {
	public void AmbientShake() => SetShake(ScreenShakeManager.AmbientShakeAmplitude, ScreenShakeManager.AmbientShakeFrequency);
	public void AmbientShakeRamped() => SetShakeRamped(ScreenShakeManager.AmbientShakeAmplitude, ScreenShakeManager.AmbientShakeFrequency);
	public void AmbientShakeRamped(float duration) => SetShakeRamped(ScreenShakeManager.AmbientShakeAmplitude, ScreenShakeManager.AmbientShakeFrequency, duration);
}