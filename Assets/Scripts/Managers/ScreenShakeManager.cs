using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScreenShakeManager : MonoBehaviour {
	private const string _logName = "ScreenShakeManager";
	public static ScreenShakeManager Instance { get; private set; }
	Cinemachine.CinemachineVirtualCamera _virtualCamera;

	public AmbientScreenShakeLayer AmbientLayer { get; private set; }
	public List<ScreenShakeLayer> Layers = new();

	public static float DefaultDuration = .5f;
	public static Func<float, float> DefaultEasingFunction = Easings.Linear;
	public static float AmbientShakeAmplitude = 0.5f;
	public static float AmbientShakeFrequency = 0.1f;

	void Awake() {
		if (!TryGetComponent(out _virtualCamera)) {
			Logger.LogError(_logName, _logName);
		}

		if (Instance == null) {
			Instance = this;
		}
		else {
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);

		// In Awake(), because Unity doesn't allow initializing with Time.time (from ScreenShakeLayer().*Tween().TStart) in the constructor/declaration
		AmbientLayer = new AmbientScreenShakeLayer();
		AmbientLayer.AmbientShake();
	}

	void Update() {
		CleanupLayers();
		SetCinemachineShake();

		if (Input.GetKeyUp(KeyCode.H)) {
			Logger.Log(_logName, "H'd");
			GetOrAddLayer("yes").SetShake(500, 500);
			GetOrAddLayer("yes").StopShakeRamped(5);
		}
		if (Input.GetKeyUp(KeyCode.J)) {
			Logger.Log(_logName, "J'd");
			GetOrAddLayer("yes").StopShakeRamped();
		}
		if (Input.GetKeyUp(KeyCode.K)) {
			Logger.Log(_logName, "K'd");
			GetOrAddLayer("yes").SetShakeRamped(1, 20);
		}
		if (Input.GetKeyUp(KeyCode.U)) {
			Logger.Log(_logName, "U'd");
			GetOrAddLayer("two").StopShakeRamped();
		}
		if (Input.GetKeyUp(KeyCode.I)) {
			Logger.Log(_logName, "I'd");
			GetOrAddLayer("two").SetShakeRamped(2, 2);
		}
	}

	private void SetCinemachineShake() {
		Cinemachine.CinemachineBasicMultiChannelPerlin perlin = _virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();

		float combinedAmp = AmbientLayer.AmpTween.GetClamped() + Layers.Sum(l => l.AmpTween.GetClamped());
		float combinedFreq = AmbientLayer.FreqTween.GetClamped() + Layers.Sum(l => l.FreqTween.GetClamped());

		perlin.m_AmplitudeGain = combinedAmp;
		perlin.m_FrequencyGain = combinedFreq;
	}

	private void CleanupLayers() => Layers.RemoveAll(l => l.ShouldRemove);

	public bool LayerExists(string name) => Layers.Any(l => l.Name == name);
	public bool LayerExists(int index) => index >= 0 && index < Layers.Count;
	public ScreenShakeLayer GetOrAddLayer(string name) => GetOrAddLayer(name, false);
	public ScreenShakeLayer GetOrAddLayer(string name, bool temporary) {
		ScreenShakeLayer layer = Layers.FirstOrDefault(l => l.Name == name);

		if (layer == null) {
			layer = new ScreenShakeLayer() {
				Name = name,
				Temporary = temporary
			};
			Layers.Add(layer);
		}

		return layer;
	}
	public void RemoveLayer(string name) {
		ScreenShakeLayer layer = Layers.FirstOrDefault(l => l.Name == name);
		if (layer != null) {
			Layers.Remove(layer);
		}
		else {
			Logger.LogWarning(_logName, $"Layer with name '{name}' does not exist");
		}
	}
	public void RemoveLayer(int index) {
		if (LayerExists(index)) {
			Layers.RemoveAt(index);
		}
		else {
			Logger.LogWarning(_logName, $"Layer with index '{index}' does not exist");
		}
	}

	public void StopAllLayers(bool includeAmbient = false) {
		if (includeAmbient)
			AmbientLayer.StopShake();
		Layers.ForEach(l => l.StopShake());
	}
}

/// <summary>
/// Represents a layer of screen shake.
/// <para/>
/// All functions use the ScreenShakeManager.DefaultEasingFunction. All functions without the duration parameter use the ScreenShakeManager.DefaultDuration.
/// </summary>
public class ScreenShakeLayer {
	public ScreenShakeLayer() {
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
}

public class AmbientScreenShakeLayer : ScreenShakeLayer {
	public void AmbientShake() => SetShakeRamped(ScreenShakeManager.AmbientShakeAmplitude, ScreenShakeManager.AmbientShakeFrequency, 0);
	public void AmbientShakeRamped() => SetShakeRamped(ScreenShakeManager.AmbientShakeAmplitude, ScreenShakeManager.AmbientShakeFrequency);
	public void AmbientShakeRamped(float duration) => SetShakeRamped(ScreenShakeManager.AmbientShakeAmplitude, ScreenShakeManager.AmbientShakeFrequency, duration);
}