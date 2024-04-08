using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class EnemyPatrolState : EnemyBaseState {

	public EnemyPatrolState(Enemy enemy, string name) : base(enemy, name) { }

	public override void EnterState() {
	}

	public override void UpdateState() {
		Enemy.CheckForObstacles();
		Enemy.CheckForPlayer();
		Enemy.EnemyMovement();
		if (Enemy.PlayerDetect == true) {
			Enemy.SwitchState("Detected");
		}
	}
	public override void ExitState() {
	}
}
