using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectedState : EnemyBaseState {
	public PlayerDetectedState(Enemy enemy, string name) : base(enemy, name) { }

	public override void EnterState() {
	}

	public override void UpdateState() {
		Enemy.CheckForPlayer();
		Enemy.StartCoroutine(PlayerDetected());

		if (Enemy.PlayerDetect == false) {
			Enemy.SwitchState("Patrol");
		}
		else if (Enemy.PlayerDetect == true) {
			Enemy.SwitchState("Charge");
		}
	}
	public override void ExitState() {
	}

	IEnumerator PlayerDetected() {
		yield return new WaitForSeconds(Enemy.Stats.DetectionPauseTime);
		Enemy.StartCoroutine(PlayerNotDetected());
	}
	public IEnumerator PlayerNotDetected() {
		yield return new WaitForSeconds(1);
		Enemy.PlayerDetect = false;
	}
}
