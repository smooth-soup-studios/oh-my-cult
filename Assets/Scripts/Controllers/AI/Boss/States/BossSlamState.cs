using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSlamAttack : BossBaseState {
	private bool _switchState = false;
	private bool _attackCooldown = false;



	public BossSlamAttack(Boss boss, string name) : base(boss, name) { }
	public override void EnterState() {
		_switchState = false;
		_attackCooldown = false;
		Boss.Animator.Play("Boss_Slam");
		Boss.BossAttacks.SlamAttack();
		Boss.StartCoroutine(AttackCooldown());
		Boss.StartCoroutine(SwitchState());

	}
	public override void UpdateState() {
		if (_attackCooldown) {
			Boss.BossAttacks.SlamAttack();
			_attackCooldown = false;
		}

		if (_switchState) {
			Boss.SwitchState("Idle");
		}
	}
	public override void ExitState() {
		_attackCooldown = false;
	}

	IEnumerator SwitchState() {
		yield return new WaitForSecondsRealtime(10f);
		_switchState = true;
	}

	IEnumerator AttackCooldown() {
		yield return new WaitForSecondsRealtime(Boss.Stats.SlamTime/2f);
		_attackCooldown = true;
	}

}