using UnityEngine;

namespace Managers {
public class InputManager : MonoBehaviour {
	public static InputManager Instance { get; set; }
	Controls Controls { get; set; }

	void Awake() {
		if (Instance != null && Instance != this) {
			Controls?.Disable();
			Destroy(gameObject);
			return;
		}

		Instance = this;
		DontDestroyOnLoad(gameObject);
		Controls = new Controls();
	}

	void OnEnable() {
		Controls?.Enable();
	}

	void OnDisable() {
		Controls?.Disable();
	}
}
}