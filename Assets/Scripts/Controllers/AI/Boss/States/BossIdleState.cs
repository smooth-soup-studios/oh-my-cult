using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleState : BossBaseState {
	public BossIdleState(Boss boss, string name) : base(boss, name) { }
	private bool _switchState = false;
	public override void EnterState() {
		_switchState = false;
		// Boss.StateCounter = Random.Range(0, Boss.States.Count - 1);
		Boss.StateCounter = Boss.GetRendomValue(Boss.WeightedValues);
		Boss.StartCoroutine(SwitchTime());
	}
	public override void UpdateState() {
		if (_switchState) {
			// 	switch (Boss.StateCounter) {
			// 	case 0:
			// 		Boss.SwitchState("Slam");
			// 		break;
			// 	case 1:
			// 		Boss.SwitchState("Charge");
			// 		break;
			// 	case 2:
			// 		Boss.SwitchState("Roar");
			// 		break;
			// 	case 3:
			// 		Boss.SwitchState("Move");
			// 		break;
			// }
			Boss.SwitchState("Slam");
			Boss.BossAttacks.Flash(Boss.Direction, BossAttackType.SLAm);
		}
		else if (Vector2.Distance(Boss.Player.transform.position, Boss.transform.position) >= 100f) {
			Boss.SwitchState("Move");
		}
		Boss.Movement = (Boss.Player.transform.position - Boss.transform.position).normalized;
		Boss.BossAnimation.SetFloat("X", Boss.Movement.x);
		Boss.BossAnimation.SetFloat("Y", Boss.Movement.y);

	}
	public override void ExitState() {
		Boss.StartCoroutine(Boss.FlashRed());
	}

	IEnumerator SwitchTime() {
		yield return new WaitForSecondsRealtime(3f);
		_switchState = true;
	}


}
