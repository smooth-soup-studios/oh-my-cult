using System.Collections;
using UnityEngine;
using Managers;

public class PlayerMoveState : BaseState {

	bool _dash = false;
	bool _attack = false;
	bool _dashCooldown = false;
	bool _heavyAttack = false;
	private bool _interact = false;

	public PlayerMoveState(string name, StateMachine stateMachine) : base(name, stateMachine) { }

	public override void EnterState() {
		EventBus.Instance.Subscribe<bool>(EventType.DASH, OnDash);
		EventBus.Instance.Subscribe<bool>(EventType.USE_PRIMARY, OnAttack);
		EventBus.Instance.Subscribe<bool>(EventType.USE_SECONDARY, OnHeavyAttack);
		EventBus.Instance.Subscribe<bool>(EventType.INTERACT, OnInteract);

	}

	public override void UpdateState() {
		StateMachine.HandleMovement(StateMachine.BaseSpeed * StateMachine.SpeedModifier * Time.deltaTime * Movement.normalized);
		//TODO: Replace the line this is on with call to WWISE event for moving
		if (Movement == Vector2.zero) {
			StateMachine.SwitchState("Idle");
			return;
		}
		else if (!_dashCooldown && _dash) {
			StateMachine.StartCoroutine(DashCooldown());
			_dash = false;
			StateMachine.SwitchState("Dash");
			return;
		}
		else if (_interact) {
			_interact = false;
			StateMachine.SwitchState("Interact");
			return;
		}
		else if (_attack) {
			StateMachine.SwitchState("Attack");
			_attack = false;
			return;
		}
		else if (_heavyAttack) {
			StateMachine.SwitchState("HeavyAttack");
			_heavyAttack = false;
			return;
		}

		StateMachine.PlayerAnimator.Play("PlayerRun", MovementDirection);
	}

	public override void ExitState() {
		EventBus.Instance.Unsubscribe<bool>(EventType.DASH, OnDash);
		EventBus.Instance.Unsubscribe<bool>(EventType.USE_PRIMARY, OnAttack);
		EventBus.Instance.Unsubscribe<bool>(EventType.USE_SECONDARY, OnAttack);
		EventBus.Instance.Unsubscribe<bool>(EventType.INTERACT, OnInteract);
	}

	private void OnDash(bool dash) {
		if (!_dashCooldown) {
			_dash = dash;
		}
	}
	public IEnumerator DashCooldown() {
		_dashCooldown = true;
		yield return new WaitForSecondsRealtime(PlayerDashState.DashCooldown);
		_dashCooldown = false;
	}
	private void OnAttack(bool attack) {
		_attack = attack;
	}
	private void OnHeavyAttack(bool heavyAttack) {
		_heavyAttack = heavyAttack;
	}

	private void OnInteract(bool value) {
		_interact = value;
	}
}