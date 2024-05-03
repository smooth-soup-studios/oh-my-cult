using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleState : BossBaseState {
	public BossIdleState(Boss boss, string name) : base(boss, name) { }
	private bool _switchState = false;
	public override void EnterState() {
		_switchState = false;
		Boss.StateCounter = Random.Range(0, Boss.States.Count - 1);
		Boss.Animator.Play("Boss_Idle");

		Boss.StartCoroutine(SwitchTime());
	}
	public override void UpdateState() {
		if (_switchState) {
			switch (Boss.StateCounter) {
				case 0:
					Boss.SwitchState("Slam");
					break;
				case 1:
					Boss.SwitchState("Charge");
					break;
				case 2:
					Boss.SwitchState("Roar");
					break;
			}
		}
	}
	public override void ExitState() {
		Boss.StartCoroutine(Boss.FlashRed()); 
	}

	IEnumerator SwitchTime() {
		yield return new WaitForSecondsRealtime(Boss.Stats.SwitchTime);
		_switchState = true;
	}
}
