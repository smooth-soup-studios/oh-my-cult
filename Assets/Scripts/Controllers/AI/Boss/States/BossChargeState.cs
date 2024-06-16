using System.Collections;
using UnityEngine;

public class BossChargeState : BossBaseState {
	public BossChargeState(Boss boss, string name) : base(boss, name) { }


	public override void EnterState() {
		// Boss.StartCoroutine(ChargeTime());
		Boss.BossAnimation.SetBool("IsWalking", true);

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
		Boss.transform.position = Vector3.MoveTowards(Boss.transform.position, Boss.Player.position, Boss.Stats.ChargeSpeed * Time.deltaTime * 2);
		Boss.CheckForPlayer();
	}
	// IEnumerator ChargeTime() {
	// 	Boss.Charge = true;
	// 	yield return new WaitForSeconds(Boss.Stats.ChargeTime);
	// 	Boss.Charge = false;
	// }

}
