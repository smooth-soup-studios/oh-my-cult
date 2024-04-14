using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChargeState : EnemyBaseState {
	public EnemyChargeState(Enemy enemy, string name) : base(enemy, name) { }


	public override void EnterState() {
		Logger.Log(Name, "Charge");


	}

	public override void UpdateState() {
		if (Enemy.PlayerDetect == true) {
			Charge();
		}
		Enemy.CheckForMeleeRange();
		if (Enemy.PlayerDetect == false) {
			Enemy.SwitchState("Patrol");
		}
		if (Enemy.AttackMelee == true ) {
			Enemy.SwitchState("Attack");
		}
	}

	public override void ExitState() {
	}

	void Charge() {
		Enemy.Agent.destination = Vector3.MoveTowards(Enemy.transform.position, Enemy.Player.position, Enemy.Stats.ChargeSpeed * Time.deltaTime * 2);
		Enemy.Agent.destination = Enemy.Player.position;
	}


}
