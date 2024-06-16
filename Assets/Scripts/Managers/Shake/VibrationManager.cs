using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class VibrationManager : MonoBehaviour {
	private const string _logName = "VibrationManager";
	public static VibrationManager Instance { get; private set; }

	public static bool VibrationEnabled = true;

	protected List<ShakeLayer> Layers = new();
	PlayerInput _playerInput;


	void Awake() {
		if (Instance == null) {
			Instance = this;
		}
		else {
			Destroy(gameObject);
		}
	}

	private void GetRefs() {
		EventBus busObject = FindObjectOfType<EventBus>();
		if (busObject) {
			_playerInput = busObject.GetComponent<PlayerInput>();
		}
;
	}
	void Update() {
		if (_playerInput) {
			VibrationEnabled = _playerInput.currentControlScheme.Contains("controller", System.StringComparison.InvariantCultureIgnoreCase);
		}
		else {
			GetRefs();
		}

		CleanupLayers();
		SetGamepadShake();
	}

	private void SetGamepadShake() {
		if (VibrationEnabled && Gamepad.current != null) {
			// float ampLow = Mathf.Clamp01(Layers.Sum(l => l.FreqTween.GetClamped() < 0.5f ? l.AmpTween.GetClamped() : 0));
			// float ampHigh = Mathf.Clamp01(Layers.Sum(l => l.FreqTween.GetClamped() >= 0.5f ? l.AmpTween.GetClamped() : 0));

			// float ampLow = Mathf.Clamp01(Layers.Sum(l => l.FreqTween.GetClamped() >= 0.5f ? l.AmpTween.GetClamped() : 0));
			// float ampHigh = Mathf.Clamp01(Layers.Sum(l => l.FreqTween.GetClamped() < 0.5f ? l.AmpTween.GetClamped() : 0));

			// Multiplying by timescale works for now lol, and sounds like a good solution for slow-mo where everything is usually muted in terms of audio
			float ampLow = Time.timeScale * Mathf.Clamp01(Layers.Sum(l => l.AmpTween.GetClamped()));
			float ampHigh = Time.timeScale * Mathf.Clamp01(Layers.Sum(l => l.FreqTween.GetClamped()));

			Gamepad.current.SetMotorSpeeds(ampLow, ampHigh);
		}
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

	public void StopAllLayers() {
		Layers.ForEach(l => l.StopShake());
	}
}

public static class VibrationLayerNames {
	public const string Consume = "Consume";
	public const string DoPrimaryDamage = "DoPrimaryDamage";
	public const string ReceivePrimaryDamage = "ReceivePrimaryDamage";
	public const string Dash = "Dash";
}