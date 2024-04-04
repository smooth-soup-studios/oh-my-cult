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

	public void OnAttack(InputValue value) {
		EventBus.Instance.TriggerEvent(EventType.ATTACK, value.isPressed);
	}

	public void OnDash(InputValue value) {
		EventBus.Instance.TriggerEvent(EventType.DASH, value.isPressed);
	}
		public void OnHeavyAttack(InputValue value) {
		EventBus.Instance.TriggerEvent(EventType.HEAVYATTACK, value.isPressed);
	}

}