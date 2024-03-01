using UnityEngine;

namespace Managers {
public class InputManager : MonoBehaviour {
	public static InputManager Instance { get; set; }
	Controls Controls { get; set; }
	public Vector2 Move { get; private set; }

	void Awake() {
		if (Instance != null && Instance != this) {
			Controls?.Disable();
			Destroy(gameObject);
			return;
		}

		Instance = this;
		DontDestroyOnLoad(gameObject);
		Controls = new Controls();

		Controls.Player.Move.performed += ctx => Move = ctx.ReadValue<Vector2>();
		Controls.Player.Move.canceled += ctx => Move = Vector2.zero;
	}

	void OnEnable() {
		Controls?.Enable();
	}

	void OnDisable() {
		Controls?.Disable();
	}
}
}