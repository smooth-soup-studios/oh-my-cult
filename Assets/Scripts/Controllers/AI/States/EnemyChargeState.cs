using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChargeState : EnemyBaseState {
	public EnemyChargeState(Enemy enemy, string name) : base(enemy, name) { }



	public override void EnterState() {

	}

	public override void UpdateState() {
		Logger.Log(Name, "Charge");
			Charge();
		Enemy.CheckForMeleeRange();
		if (Enemy.PlayerDetect == false) {
			Enemy.SwitchState("Patrol");
		}
		if(Enemy.AttackMelee == true){
			Enemy.SwitchState("Attack");
		}
	}


	public override void ExitState() {

	}

	void Charge() {
		Enemy.TargetPosition = GameObject.Find("PlayerBlob").transform.position;
		Enemy.StartingPosition = Enemy.transform.position;
		Enemy.transform.position = Vector2.MoveTowards(Enemy.transform.position, Enemy.TargetPosition, Enemy.Stats.Speed * Time.deltaTime);
	}
}
