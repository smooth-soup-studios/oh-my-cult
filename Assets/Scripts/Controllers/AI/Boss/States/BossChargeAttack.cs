using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChargeAttack : BossBaseState {
	public BossChargeAttack(Boss boss, string name) : base(boss, name) { }
	private bool _switchState = false;
	private Transform _transform;
	private float _radius = 20f;
	public override void EnterState() {
		_switchState = false;
		Boss.StartCoroutine(SwitchState());
		Boss.StartCoroutine(Boss.FlashRed());
		Boss.Animator.Play("Boss_Slam");
	}
	public override void UpdateState() {
		Boss.BossAttacks.SlamAttack();
		if (_switchState){
			Boss.SwitchState("Idle");
		}

	}
	public override void ExitState() {
		Boss.Enemy = false;
	}
	IEnumerator SwitchState() {
		yield return new WaitForSecondsRealtime(Boss.Stats.SwitchTime);
		_switchState = true;
	}

}
