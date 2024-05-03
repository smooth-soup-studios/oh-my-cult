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

	public void OnHotbarSelect(InputValue value) {
		// Get the value from the button & convert to index
		EventBus.Instance.TriggerEvent(EventType.HOTBAR_SELECT, (int)value.Get<float>()-1);
	}
	public void OnHotbarSwitch(InputValue value) {
		// Is either 1 for next or -1 for prev
		EventBus.Instance.TriggerEvent(EventType.HOTBAR_SWITCH, (int)value.Get<float>());
	}
}