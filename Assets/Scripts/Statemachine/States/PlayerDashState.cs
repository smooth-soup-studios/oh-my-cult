using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDashState : BaseState {
	private float _dashSpeed = 25;
	private bool _cooldown = true;
	private bool _dash = true;
	public PlayerDashState(string name, StateMachine stateMachine) : base(name, stateMachine) {
	}
	public override void EnterState() {
		// StateMachine.StartCoroutine(dashCooldown());
		StateMachine.StartCoroutine(dashTime());
	}

	public override void UpdateState() {
		if (_dash) {
			StateMachine.transform.Translate(Movement * _dashSpeed * Time.deltaTime);
		}
		if (!_dash) {
			StateMachine.SwitchState("Move");
		}
	}

	public override void ExitState() {
	}

	// IEnumerator dashCooldown() {
	// 	_cooldown = true;
	// 	yield return new WaitForSecondsRealtime(0.25f);
	// 	_cooldown = false;
	// }
	IEnumerator dashTime() {
		_dash = true;
		yield return new WaitForSecondsRealtime(0.25f);
		_dash = false;
	}
}
