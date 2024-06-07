using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScreenShakeManager : MonoBehaviour {
	private const string _logName = "ScreenShakeManager";
	[Header("Settings")]
	[SerializeField] private bool _useAmbientShake = true;
	public static ScreenShakeManager Instance { get; private set; }
	Cinemachine.CinemachineVirtualCamera _virtualCamera;

	public AmbientScreenShakeLayer AmbientLayer { get; private set; }
	protected List<ShakeLayer> Layers = new();

	public static float DefaultDuration = .5f;
	public static Func<float, float> DefaultEasingFunction = Easings.Linear;
	public static float AmbientShakeAmplitude = 0.1f;
	public static float AmbientShakeFrequency = 0.1f;

	void Awake() {
		if (!TryGetComponent(out _virtualCamera)) {
			Logger.LogError(_logName, $"No virtual camera found on {name}!");
		}

		if (Instance == null) {
			Instance = this;
		}
		else {
			Destroy(gameObject);
		}

		// In Awake(), because Unity doesn't allow initializing with Time.time (from ScreenShakeLayer().*Tween().TStart) in the constructor/declaration
		AmbientLayer = new AmbientScreenShakeLayer();
		if (_useAmbientShake) {
			AmbientLayer.AmbientShake();
		}
	}

	void Update() {
		CleanupLayers();
		SetCinemachineShake();
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
	public ShakeLayer GetOrAddLayer(string name) => GetOrAddLayer(name, false);
	public ShakeLayer GetOrAddLayer(string name, bool temporary) {
		ShakeLayer layer = Layers.FirstOrDefault(l => l.Name == name);

		if (layer == null) {
			layer = new ShakeLayer() {
				Name = name,
				Temporary = temporary
			};
			Layers.Add(layer);
		}

		return layer;
	}
	public void RemoveLayer(string name) {
		ShakeLayer layer = Layers.FirstOrDefault(l => l.Name == name);
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