using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour {
	private string _logname = "InputManager";
	public static InputManager Instance;

	private void Awake() {
		if (Instance == null) {
			Instance = this;
		}
		else {
			Logger.LogWarning("InputManager", "Multiple Instances found! Exiting..");
			Destroy(this);
			return;
		}
		DontDestroyOnLoad(Instance);
	}

	public void OnMove(InputValue value) {
		EventBus.TriggerEvent(EventType.MOVEMENT, value.Get<Vector2>());
	}

	public void OnAttack(InputValue value) {
		EventBus.TriggerEvent(EventType.ATTACK, value.isPressed);
	}

	public void OnDash(InputValue value) {
		EventBus.TriggerEvent(EventType.DASH, value.isPressed);
	}

}