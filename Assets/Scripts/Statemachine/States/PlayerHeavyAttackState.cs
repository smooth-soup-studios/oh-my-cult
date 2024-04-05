using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeavyAttackState : BaseState
{
	float _speed = 10;
	private bool _onHeavyAttack = true;
	public PlayerHeavyAttackState(string name, StateMachine stateMachine) : base(name, stateMachine) { }

public override void EnterState() {

		StateMachine.StartCoroutine(AttackSpeed());
		StateMachine.Weapon.HeavyAttack();
	}

	public override void UpdateState() {
		StateMachine.transform.Translate(_speed * Time.deltaTime * Movement);
		if (!_onHeavyAttack) {
			StateMachine.SwitchState("Idle");
		}
		else if (Movement == Vector2.zero) {
			StateMachine.SwitchState("Move");
		}

	}
		public override void ExitState() {
	}
		private IEnumerator AttackSpeed() {
		_onHeavyAttack = true;
		yield return new WaitForSecondsRealtime(StateMachine.Weapon.AttackSpeed);
		_onHeavyAttack = false;
	}
}
