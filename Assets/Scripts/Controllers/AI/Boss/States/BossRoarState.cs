using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class BossRoarState : BossBaseState {
	public BossRoarState(Boss boss, string name) : base(boss, name) { }
	private bool _switchState = false;
	public override void EnterState() {
		_switchState = false;
		Boss.Animator.Play("Boss_Roar");
		Boss.BossAttacks.RoarAttack();
		//TODO: Replace the line this is on with call to WWISE event ( roar )
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
