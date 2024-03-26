using UnityEngine;

public class PlayerIdleState : BaseState {

	private bool _attack = false;
	public PlayerIdleState(string name, StateMachine stateMachine) : base(name, stateMachine) {
	}


	public override void EnterState() {
		EventBus.Instance.Subscribe<bool>(EventType.ATTACK, OnAttack);

	}

	public override void UpdateState() {
		if (Movement != Vector2.zero) {
			StateMachine.SwitchState("Move");
		}
		if (_attack) {
			_attack = false;
			StateMachine.SwitchState("Attack");
		}
	}

	public override void ExitState() {
		EventBus.Instance.Unsubscribe<bool>(EventType.ATTACK, OnAttack);

	}

	private void OnAttack(bool value) {
		_attack = value;
	}


}