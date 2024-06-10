using System.Collections;
using UnityEngine;

public class BossSlamAttack : BossBaseState {
	private bool _switchState = false;
	private bool _firstSlam = false;
	private bool _secondSlam = false;

	public BossSlamAttack(Boss boss, string name) : base(boss, name) { }
	public override void EnterState() {
		_switchState = false;
		Boss.BossAnimation.SetBool("SlamAttack", true);
		Boss.StartCoroutine(FirstSlam());
		Boss.StartCoroutine(SwitchState());
	}
	public override void UpdateState() {
		if (_firstSlam) {

			Boss.BossAttacks.Attack(Boss.Direction, BossAttackType.SLAM);
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
		_secondSlam = false;
		Boss.BossAnimation.SetBool("SlamAttack", false);
	}

	IEnumerator SwitchState() {
		yield return new WaitForSecondsRealtime(1.05f);
		_switchState = true;
	}
	IEnumerator FirstSlam() {
		yield return new WaitForSecondsRealtime(0.6f);
		_firstSlam = true;
	}



}