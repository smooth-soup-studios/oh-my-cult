using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

public class BossRoarState : BossBaseState {
	public BossRoarState(Boss boss, string name) : base(boss, name) { }
	private bool _switchState = false;

	private bool _attackCooldown = false;
	public override void EnterState() {
		_switchState = false;
		Boss.Animator.Play("Boss_Roar");
		// Boss.BossAttacks.RoarAttack();
		SoundManager.Instance.PlayClip(Boss.RoarSoundClip, Boss.transform, 1f);
		Boss.StartCoroutine(AttackCooldown());
		Boss.StartCoroutine(SwitchState());
	}
	public override void UpdateState() {
		if (_attackCooldown) {
			Boss.BossAttacks.RoarAttack();
			Logger.Log("attack", "Attack");
			_attackCooldown = false;
		}

		if (_switchState) {
			Boss.SwitchState("Idle");
		}
	}
	public override void ExitState() { }
	IEnumerator SwitchState() {
		yield return new WaitForSecondsRealtime(Boss.Stats.RoarTime);
		_switchState = true;
	}
	IEnumerator AttackCooldown() {
		yield return new WaitForSecondsRealtime(Boss.Stats.SlamTime / 2f);
		_attackCooldown = true;
	}

}
