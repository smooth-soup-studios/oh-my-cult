using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSlamAttack : BossBaseState {
	private bool _switchState = false;
	public BossSlamAttack(Boss boss, string name) : base(boss, name) { }
	public override void EnterState() {
		_switchState = false;
		Boss.Animator.Play("Boss_Slam");
		Boss.BossAttacks.SlamAttack();
		Boss.StartCoroutine(SwitchState());
	}
	public override void UpdateState() {
		if (_switchState) {
			Boss.SwitchState("Idle");
		}
	}
	public override void ExitState() { }

	IEnumerator SwitchState() {
		yield return new WaitForSecondsRealtime(Boss.Stats.SlamTime);
		_switchState = true;
	}

}