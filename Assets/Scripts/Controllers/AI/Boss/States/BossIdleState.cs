using System.Collections;
using UnityEngine;

public class BossIdleState : BossBaseState {
	public BossIdleState(Boss boss, string name) : base(boss, name) { }
	private bool _switchState = false;
	public int StateCounter = 0;

	public override void EnterState() {
		_switchState = false;
		// Boss.StateCounter = Random.Range(0, Boss.States.Count - 1);
		StateCounter = Boss.GetRendomValue(Boss.WeightedValues);
		Boss.StartCoroutine(SwitchTime());
	}

	public override void UpdateState() {
		if (_switchState) {
			switch (StateCounter) {
				case 0:
					Boss.BossAttacks.FlashSlam(Boss.Direction, BossAttackType.SLAM);
					Boss.SwitchState("Slam");
					break;
				case 1:
					Boss.BossAttacks.FlashRoar(Boss.Direction, BossAttackType.ROAR);
					Boss.SwitchState("Roar");
					break;
			}
		}

		// else if (Vector2.Distance(Boss.Player.transform.position, Boss.transform.position) >= 4f) {
		// 	Boss.SwitchState("Move");
		// }
		else if (Vector2.Distance(Boss.Player.transform.position, Boss.transform.position) >= 4f && Boss.WaitForWalking == false) {
			Boss.SwitchState("Move");
		}

		//TODO Need Fixing
		// else if (Vector2.Distance(Boss.Player.transform.position, Boss.transform.position) >= 6f && Boss.WaitForWalking == false) {
		// 	if (Vector2.Distance(Boss.Player.transform.position, Boss.transform.position) <= 4f) {
		// 		Boss.SwitchState("Move");
		// 	}
		// 	else {
		// 		Boss.SwitchState("Charge");
		// 	}
		// }

		Boss.Movement = (Boss.Player.transform.position - Boss.transform.position).normalized;
		Boss.BossAnimation.SetFloat("X", Boss.Movement.x);
		Boss.BossAnimation.SetFloat("Y", Boss.Movement.y);

	}
	public override void ExitState() {
	}

	IEnumerator SwitchTime() {
		yield return new WaitForSeconds(1f);
		_switchState = true;
	}


}
