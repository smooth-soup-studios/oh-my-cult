using UnityEngine;

public class BossMoveState : BossBaseState {
	public BossMoveState(Boss boss, string name) : base(boss, name) { }

	public override void EnterState() {
		Boss.BossAnimation.SetBool("IsWalking", true);
		Boss.StartCoroutine(Boss.Flash());
	}
	public override void UpdateState() {
		Boss.transform.position = Vector3.MoveTowards(Boss.transform.position, Boss.Player.position, 1 * Time.deltaTime * 2);
		Boss.Movement = (Boss.Player.transform.position - Boss.transform.position).normalized;
		Boss.BossAnimation.SetFloat("X", Boss.Movement.x);
		Boss.BossAnimation.SetFloat("Y", Boss.Movement.y);
		if (Vector2.Distance(Boss.Player.transform.position, Boss.transform.position) >= 10f) {
			Boss.SwitchState("Charge");
		}
		if (Vector2.Distance(Boss.Player.transform.position, Boss.transform.position) <= 5f) {
			Boss.SwitchState("Idle");
		}

	}
	public override void ExitState() {
		Boss.BossAnimation.SetBool("IsWalking", false);
	}


}
