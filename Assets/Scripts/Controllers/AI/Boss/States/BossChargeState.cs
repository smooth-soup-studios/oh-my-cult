using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChargeState : BossBaseState {
	public BossChargeState(Boss boss, string name) : base(boss, name) { }
	private bool _charge;
	public override void EnterState() {
		Logger.Log(Name, "Charge");
		Boss.StartCoroutine(ChargeTime());
	}
	public override void UpdateState() {
		Boss.CheckForPlayer();
		if (_charge == true) {
			ChargeAttack();
			Logger.Log(Name, "Charging");
		}
		else if(Boss.Enemy == true){
			Boss.BossAttacks.ChargeAttack();
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
	}

	IEnumerator ChargeTime() {
		_charge = true;
		yield return new WaitForSeconds(Boss.Stats.ChargeTime);
		_charge = false;
	}
}
