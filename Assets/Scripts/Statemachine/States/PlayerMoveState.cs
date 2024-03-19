using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : BaseState {

	bool _dash = false;
	float _speed = 10;
	bool _dashCooldown = false;
	public PlayerMoveState(string name, StateMachine stateMachine) : base(name, stateMachine) {
	}

	public override void EnterState() {
		EventBus.Subscribe<bool>(EventType.DASH, OnDash);

	}

	public override void UpdateState() {
		StateMachine.transform.Translate(_speed * Time.deltaTime * Movement);
		if (Movement == Vector2.zero) {
			StateMachine.SwitchState("Idle");
		}
		if (!_dashCooldown && _dash) {
			StateMachine.StartCoroutine(DashCooldown());
			_dash = false;
			StateMachine.SwitchState("Dash");
		}
	}

	public override void ExitState() {
		EventBus.Unsubscribe<bool>(EventType.DASH, OnDash);
	}

	private void OnDash(bool dash) {
		_dash = dash;
	}
	public IEnumerator DashCooldown() {
		_dashCooldown = true;
		yield return new WaitForSecondsRealtime(1.25f);
		_dashCooldown = false;
	}
}