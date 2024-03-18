using UnityEngine;

public class PlayerMoveState : BaseState {

	bool _dash = false;
	float _speed = 10;
	public PlayerMoveState(string name, StateMachine stateMachine) : base(name, stateMachine) {
	}

	public override void EnterState() {
		EventBus.Instance.Subscribe<bool>(EventType.DASH, OnDash);

	}

	public override void UpdateState() {
		StateMachine.transform.Translate(_speed * Time.deltaTime * Movement);
		if (Movement == Vector2.zero) {
			StateMachine.SwitchState("Idle");
		}
		if (_dash) {
			_dash = false;
			StateMachine.SwitchState("Dash");
		}
	}

	public override void ExitState() {
		EventBus.Instance.Unsubscribe<bool>(EventType.DASH, OnDash);
	}

	private void OnDash(bool dash) {
		_dash = dash;
	}
}