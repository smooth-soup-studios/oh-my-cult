using UnityEngine;

public class PlayerIdleState : BaseState {

	private bool _attack = false;
	private bool _heavyAttack = false;
	private bool _interact = false;
	public PlayerIdleState(string name, StateMachine stateMachine) : base(name, stateMachine) {
	}


	public override void EnterState() {
		EventBus.Instance.Subscribe<bool>(EventType.USE_PRIMARY, OnAttack);
		EventBus.Instance.Subscribe<bool>(EventType.USE_SECONDARY, OnHeavyAttack);
		EventBus.Instance.Subscribe<bool>(EventType.INTERACT, OnInteract);
	}

	public override void UpdateState() {
		if (Movement != Vector2.zero) {
			StateMachine.SwitchState("Move");
			return;
		}
		else if (_interact) {
			_interact = false;
			StateMachine.SwitchState("Interact");
			return;
		}
		else if (_attack) {
			_attack = false;
			StateMachine.SwitchState("Attack");
			return;
		}
		else if (_heavyAttack) {
			_heavyAttack = false;
			StateMachine.SwitchState("HeavyAttack");
			return;
		}
		StateMachine.PlayerAnimator.Play("PlayerIdle", MovementDirection);
	}

	public override void ExitState() {
		EventBus.Instance.Unsubscribe<bool>(EventType.USE_PRIMARY, OnAttack);
		EventBus.Instance.Unsubscribe<bool>(EventType.USE_SECONDARY, OnHeavyAttack);
		EventBus.Instance.Unsubscribe<bool>(EventType.INTERACT, OnInteract);

	}

	private void OnAttack(bool value) {
		_attack = value;
	}
	private void OnHeavyAttack(bool value) {
		_heavyAttack = value;
	}

	private void OnInteract(bool value) {
		_interact = value;
	}


}