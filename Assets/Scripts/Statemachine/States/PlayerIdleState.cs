using UnityEngine;

public class PlayerIdleState : BaseState {

	private bool _attack = false;
	private bool _heavyAttack = false;
	public PlayerIdleState(string name, StateMachine stateMachine) : base(name, stateMachine) {
	}


	public override void EnterState() {
		EventBus.Instance.Subscribe<bool>(EventType.ATTACK, OnAttack);
		EventBus.Instance.Subscribe<bool>(EventType.HEAVYATTACK, OnHeavyAttack);

	}

	public override void UpdateState() {
		if (Movement != Vector2.zero) {
			StateMachine.SwitchState("Move");
		}
		if (_attack) {
			_attack = false;
			StateMachine.SwitchState("Attack");
		}
		if (_heavyAttack) {
			_heavyAttack = false;
			StateMachine.SwitchState("HeavyAttack");
		}
	}

	public override void ExitState() {
		EventBus.Instance.Unsubscribe<bool>(EventType.ATTACK, OnAttack);
		EventBus.Instance.Subscribe<bool>(EventType.HEAVYATTACK, OnHeavyAttack);

	}

	private void OnAttack(bool value) {
		_attack = value;
	}
	private void OnHeavyAttack(bool value) {
		_heavyAttack = value;
	}


}