using UnityEngine;

public class PlayerIdleState : BaseState {
	public PlayerIdleState(string name, StateMachine stateMachine) : base(name, stateMachine) {
	}


	public override void EnterState() {
	}

	public override void UpdateState() {
		if (Movement != Vector2.zero) {
			StateMachine.SwitchState("Move");
		}
	}

	public override void ExitState() {
	}


}