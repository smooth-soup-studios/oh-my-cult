using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChargeAttack : BossBaseState {
	private bool _switchState = false;
	private bool _firstSlam = false;
	public BossChargeAttack(Boss boss, string name) : base(boss, name) { }
	public override void EnterState() {
		_switchState = false;
		Boss.BossAnimation.SetBool("ChargeAttack", true);
		Boss.StartCoroutine(ChargeFirstSlam());
		Boss.StartCoroutine(SwitchState());
	}
	public override void UpdateState() {
		if (_firstSlam) {
			Boss.BossAttacks.Attack(Boss.Direction, BossAttackType.SLAm);
			_firstSlam = false;
		}
		if (_switchState) {
			Boss.SwitchState("Idle");
		}
		Boss.Movement = (Boss.Player.transform.position - Boss.transform.position).normalized;
		Boss.BossAnimation.SetFloat("X", Boss.Movement.x);
		Boss.BossAnimation.SetFloat("Y", Boss.Movement.y);
	}
	public override void ExitState() {
		_firstSlam = false;
		Boss.BossAnimation.SetBool("ChargeAttack", false);
	}

	IEnumerator SwitchState() {
		yield return new WaitForSecondsRealtime(1f);
		_switchState = true;
	}
	IEnumerator ChargeFirstSlam() {
		yield return new WaitForSecondsRealtime(0.29f);
		_firstSlam = true;
	}



}