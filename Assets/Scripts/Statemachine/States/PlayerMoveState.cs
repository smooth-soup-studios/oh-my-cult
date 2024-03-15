using UnityEngine;

public class PlayerMoveState : BaseState {

	bool _dash = false;
	float _speed = 10;
	public PlayerMoveState(string name, StateMachine stateMachine) : base(name, stateMachine) {
	}

	public override void EnterState() {
		EventBus.Subscribe<bool>(EventType.DASH, OnDash);
	}

	public override void UpdateState() {
		StateMachine.transform.Translate(Movement * _speed * Time.deltaTime);
		// if(_dash){
		// 	StateMachine.SwitchState("Dash");
		// }
	}

	public override void ExitState() {
		EventBus.Unsubscribe<bool>(EventType.DASH, OnDash);
	}

	private void OnDash(bool dash){
		_dash = dash;
	}
}