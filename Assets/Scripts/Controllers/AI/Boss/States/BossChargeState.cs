using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChargeState : BossBaseState {
	public BossChargeState(Boss boss, string name) : base(boss, name) { }
	private bool _didAttack = false;
	private bool _attackCooldown = false;

	public override void EnterState() {
		Boss.StartCoroutine(ChargeTime());
		Boss.StartCoroutine(AttackCooldown());
		Boss.Animator.Play("Boss_Down");

	}
	public override void UpdateState() {
		Boss.CheckForPlayer();
		if (Boss.Charge == true) {
			ChargeAttack();
		}
		else if (Boss.Enemy == true) {

			if (_attackCooldown) {
				Boss.StartCoroutine(Boss.FlashRed());
				Boss.BossAttacks.ChargeAttack();
				Logger.Log("attack", "Attack");
				_attackCooldown = false;
			}
			Boss.StartCoroutine(AttackTime());
			// Boss.SwitchState("Idle");
		}
		else if (_didAttack == true) {
			Boss.SwitchState("Idle");
		}
		else {
			Boss.SwitchState("Idle");
		}
	}
	public override void ExitState() {
		Boss.Enemy = false;
	}

	private void ChargeAttack() {
		Boss.transform.position = Vector3.MoveTowards(Boss.transform.position, Boss.Player.position, Boss.Stats.ChargeSpeed * Time.deltaTime * 2);
		Boss.CheckForPlayer();
	}

	IEnumerator ChargeTime() {
		Boss.Charge = true;
		yield return new WaitForSeconds(Boss.Stats.ChargeTime);
		Boss.Charge = false;
	}

	IEnumerator AttackTime() {
		_didAttack = false;
		yield return new WaitForSeconds(0.1f);
		_didAttack = true;
	}
	IEnumerator AttackCooldown() {
		yield return new WaitForSecondsRealtime(Boss.Stats.SlamTime / 2f);
		_attackCooldown = true;
	}
}
