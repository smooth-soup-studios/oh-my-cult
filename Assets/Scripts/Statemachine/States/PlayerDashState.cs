using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDashState : BaseState {
	private float _dashSpeed = 25;
	private bool _dash = true;
	public PlayerDashState(string name, StateMachine stateMachine) : base(name, stateMachine) {
	}
	public override void EnterState() {
		StateMachine.StartCoroutine(dashTime());
	}

	public override void UpdateState() {

		StateMachine.transform.Translate(Movement * _dashSpeed * Time.deltaTime);
		if (!_dash) {
			StateMachine.SwitchState("Move");
		}
	}

	public override void ExitState() {
	}


	IEnumerator dashTime() {
		_dash = true;
		yield return new WaitForSecondsRealtime(0.25f);
		_dash = false;
	}
}
