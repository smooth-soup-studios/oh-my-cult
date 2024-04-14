using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoarState : BossBaseState {
	public BossRoarState(Boss boss, string name) : base(boss, name) { }
	private bool _switchState = false;
	public override void EnterState() {
		_switchState = false;
		Boss.Animator.Play("Boss_Roar");
		Boss.BossAttacks.RoarAttack();
		Boss.StartCoroutine(SwitchState());
	}
	public override void UpdateState() {
		if (_switchState) {
			Boss.SwitchState("Idle");
		}
	}
	public override void ExitState() { }
	IEnumerator SwitchState() {
		yield return new WaitForSecondsRealtime(Boss.Stats.RoarTime);
		_switchState = true;
	}
}
