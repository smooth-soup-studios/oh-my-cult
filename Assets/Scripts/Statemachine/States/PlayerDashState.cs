using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerDashState : BaseState {
	private float _dashSpeedModifier = 2.5f;
	private bool _dash = true;
	public PlayerDashState(string name, StateMachine stateMachine) : base(name, stateMachine) {
	}
	public override void EnterState() {
		StateMachine.StartCoroutine(dashTime());
		StateMachine.EchoDashController.StartEcho();
	}

	public override void UpdateState() {

		StateMachine.transform.Translate(StateMachine.BaseSpeed * _dashSpeedModifier * StateMachine.SpeedModifier * Time.deltaTime * Movement);
		if (!_dash) {
			StateMachine.SwitchState("Move");
		}
	}

	public override void ExitState() {
		StateMachine.EchoDashController.StopEcho();
	}


	IEnumerator dashTime() {
		_dash = true;
		yield return new WaitForSecondsRealtime(0.25f);
		_dash = false;
	}
}
