using UnityEngine;

public class BossMoveState : BossBaseState {
	public BossMoveState(Boss boss, string name) : base(boss, name) { }
	private float _radius = 4f;

	public override void EnterState() {
		Boss.BossAnimation.SetBool("IsWalking", true);
	}
	public override void UpdateState() {
		Vector3 movePos = Boss.Player.position;
		movePos = Vector3.MoveTowards(movePos, Boss.transform.position, _radius);
		Boss.Agent.SetDestination(movePos);


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
