using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class PlayerLightController : MonoBehaviour {
	private Light2D _playerLight;

	void Awake() {
		_playerLight = GetComponent<Light2D>();
	}

	void Update() {
		// TEMP FIX! Should be in some manager or sumn later.
		_playerLight.enabled = SceneManager.GetActiveScene().name == SceneDefs.VillageLevel || SceneManager.GetActiveScene().name.Contains("lighting", StringComparison.OrdinalIgnoreCase);
	}
}
