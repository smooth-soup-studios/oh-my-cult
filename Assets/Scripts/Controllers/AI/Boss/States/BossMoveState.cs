using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMoveState : BossBaseState {
	public BossMoveState(Boss boss, string name) : base(boss, name) { }
	private bool _switchState = false;
	private Transform _transform;
	private float _radius = 20f;
	public override void EnterState() {
		_switchState = false;
		Boss.StartCoroutine(SwitchState());
	}
	public override void UpdateState() {
		Vector3 movePos = Boss.Player.transform.position;
		Vector3.MoveTowards(movePos, _transform.position, _radius);

	}
	public override void ExitState() {

	}
	IEnumerator SwitchState() {
		yield return new WaitForSecondsRealtime(Boss.Stats.RoarTime);
		_switchState = true;
	}

}
