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
		Boss.transform.position = Vector3.MoveTowards(Boss.transform.position, Boss.Player.position, 5 * Time.deltaTime * 2);

	}
	public override void ExitState() {

	}
	IEnumerator SwitchState() {
		yield return new WaitForSecondsRealtime(Boss.Stats.RoarTime);
		_switchState = true;
	}

}
