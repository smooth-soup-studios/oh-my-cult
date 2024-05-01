using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour {
	private static string _logname = "InputManager";
	public static InputManager Instance;

	private void Awake() {
		if (Instance == null) {
			Instance = this;
		}
		else {
			Logger.LogWarning(_logname, "Multiple Instances found! Exiting..");
			Destroy(this);
			return;
		}
		DontDestroyOnLoad(Instance);
	}

	public void OnMove(InputValue value) {
		EventBus.Instance.TriggerEvent(EventType.MOVEMENT, value.Get<Vector2>());
	}

	public void OnDash(InputValue value) {
		EventBus.Instance.TriggerEvent(EventType.DASH, value.isPressed);
	}

	public void OnInteract(InputValue value) {
		EventBus.Instance.TriggerEvent(EventType.INTERACT, value.isPressed);
	}

	public void OnPrimary(InputValue value) {
		EventBus.Instance.TriggerEvent(EventType.USE_PRIMARY, value.isPressed);
	}

	public void OnSecondary(InputValue value) {
		EventBus.Instance.TriggerEvent(EventType.USE_SECONDARY, value.isPressed);
	}

	public void OnToggleMenu(InputValue value) {
		EventBus.Instance.TriggerEvent(EventType.PAUSE, value.isPressed);
	}

}