using System.Collections;
using UnityEngine;
using Managers;

public class PlayerMoveState : BaseState {

	bool _dash = false;
	bool _attack = false;
	bool _dashCooldown = false;
	bool _heavyAttack = false;
	bool _walkSound = false;

	public PlayerMoveState(string name, StateMachine stateMachine) : base(name, stateMachine) { }

	public override void EnterState() {
		EventBus.Instance.Subscribe<bool>(EventType.DASH, OnDash);
		EventBus.Instance.Subscribe<bool>(EventType.ATTACK, OnAttack);
		EventBus.Instance.Subscribe<bool>(EventType.HEAVYATTACK, OnAttack);
	}

	public override void UpdateState() {
		StateMachine.HandleMovement(StateMachine.BaseSpeed * StateMachine.SpeedModifier * Time.deltaTime * Movement.normalized);
		StateMachine.PlayerAnimator.Play("PlayerRun", MovementDirection);

		if(!_walkSound){
			SoundManager.Instance.PlayClip(StateMachine.RunSoundClip, StateMachine.transform, 1f);
			StateMachine.StartCoroutine(WalkSpeed());
		}

		if (Movement == Vector2.zero) {
			StateMachine.SwitchState("Idle");
		}
		else if (!_dashCooldown && _dash) {
			StateMachine.StartCoroutine(DashCooldown());
			_dash = false;
			StateMachine.SwitchState("Dash");
		}
		else if (_attack) {
			StateMachine.SwitchState("Attack");
			_attack = false;
		}
		else if (_heavyAttack) {
			StateMachine.SwitchState("HeavyAttack");
			_heavyAttack = false;
		}
	}

	public override void ExitState() {
		EventBus.Instance.Unsubscribe<bool>(EventType.DASH, OnDash);
		EventBus.Instance.Unsubscribe<bool>(EventType.ATTACK, OnAttack);
		EventBus.Instance.Unsubscribe<bool>(EventType.HEAVYATTACK, OnAttack);
	}

	private void OnDash(bool dash) {
		if (!_dashCooldown) {
			_dash = dash;
		}
	}
	public IEnumerator DashCooldown() {
		_dashCooldown = true;
		yield return new WaitForSecondsRealtime(1.25f);
		_dashCooldown = false;
	}
	private void OnAttack(bool attack) {
		_attack = attack;
	}
	private void OnHeavyAttack(bool heavyAttack) {
		_heavyAttack = heavyAttack;
	}

	private IEnumerator WalkSpeed() {
		_walkSound = true;
		yield return new WaitForSecondsRealtime(StateMachine.RunSoundClip.length);
		_walkSound = false;
	}
}