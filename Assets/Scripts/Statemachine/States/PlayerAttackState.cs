using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAttackState : BaseState {
	float _speed = 10;
	private bool _onAttack = true;
	public PlayerAttackState(string name, StateMachine stateMachine) : base(name, stateMachine) { }
	public override void EnterState() {

		StateMachine.StartCoroutine(AttackSpeed());
		StateMachine.Weapon.Attack();
	}

	public override void UpdateState() {
		StateMachine.transform.Translate(_speed * Time.deltaTime * Movement);
		if (!_onAttack) {
			StateMachine.SwitchState("Idle");
		}
		else if (Movement == Vector2.zero) {
			StateMachine.SwitchState("Move");
		}

	}

	public override void ExitState() {
		Debug.Log("Byeee");
	}

	private IEnumerator AttackSpeed() {
		_onAttack = true;
		yield return new WaitForSecondsRealtime(StateMachine.Weapon.AttackSpeed);
		_onAttack = false;
	}
}
