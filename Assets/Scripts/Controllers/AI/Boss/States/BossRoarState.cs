using System.Collections;
using UnityEngine;

public class BossRoarState : BossBaseState {
	public BossRoarState(Boss boss, string name) : base(boss, name) { }
	private bool _switchState = false;

	private bool _attackCooldown = false;
	public override void EnterState() {
		_switchState = false;
		Boss.BossAnimation.SetBool("RoarAttack", true);
		Boss.StartCoroutine(AttackCooldown());
		Boss.StartCoroutine(SwitchState());
	}
	public override void UpdateState() {
		Boss.CheckForPlayer();
		if (_attackCooldown) {
			Boss.BossAttacks.Attack(Boss.Direction, BossAttackType.ROAR);
			_attackCooldown = false;
		}

		if (_switchState) {
			Boss.SwitchState("Idle");
		}
		Boss.Movement = (Boss.Player.transform.position - Boss.transform.position).normalized;
		Boss.BossAnimation.SetFloat("X", Boss.Movement.x);
		Boss.BossAnimation.SetFloat("Y", Boss.Movement.y);
	}
	public override void ExitState() {
		Boss.BossAnimation.SetBool("RoarAttack", false);
	}
	IEnumerator SwitchState() {
		yield return new WaitForSecondsRealtime(1.04f);
		_switchState = true;
	}
	IEnumerator AttackCooldown() {
		yield return new WaitForSecondsRealtime(0.5f);
		_attackCooldown = true;
	}

}
