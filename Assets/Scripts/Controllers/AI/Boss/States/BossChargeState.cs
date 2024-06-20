using System.Collections;
using UnityEngine;

public class BossChargeState : BossBaseState {
	public BossChargeState(Boss boss, string name) : base(boss, name) { }
	private float _radius = 4f;

	public override void EnterState() {
		// Boss.StartCoroutine(ChargeTime());
		Boss.BossAnimation.SetBool("IsWalking", true);
		Boss.Agent.speed = Boss.Stats.ChargeSpeed;
	}
	public override void UpdateState() {
		// Boss.CheckForPlayer();
		// if (Boss.Charge == true) {
		ChargeAttack();
		// }
		if (Boss.Enemy == true) {
			Boss.BossAttacks.FlashSlam(Boss.Direction, BossAttackType.SLAM);
			Boss.SwitchState("ChargeAttack");
		}
		// else {
		// 	Boss.SwitchState("Idle");
		// }
		Boss.Movement = (Boss.Player.transform.position - Boss.transform.position).normalized;
		Boss.BossAnimation.SetFloat("X", Boss.Movement.x);
		Boss.BossAnimation.SetFloat("Y", Boss.Movement.y);
	}
	public override void ExitState() {
		Boss.BossAnimation.SetBool("IsWalking", false);
	}
	private void ChargeAttack() {
		Vector3 movePos = Boss.Player.position;
		movePos = Vector3.MoveTowards(movePos, Boss.transform.position, _radius);
		Boss.Agent.SetDestination(movePos);
		Boss.CheckForPlayer();
	}
}
