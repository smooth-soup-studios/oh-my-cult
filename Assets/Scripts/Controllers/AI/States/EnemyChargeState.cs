using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChargeState : EnemyBaseState {
	public EnemyChargeState(Enemy enemy, string name) : base(enemy, name) { }



	public override void EnterState() {
		Logger.Log(Name, "Charge");

	}

	public override void UpdateState() {

		Charge();
		Enemy.CheckForMeleeRange();
		if (Enemy.PlayerDetect == false) {
			Enemy.SwitchState("Patrol");
		}
		if (Enemy.AttackMelee == true) {
			Enemy.SwitchState("Attack");
		}
		// Enemy.StartCoroutine(PlayerDetected());

	}


	public override void ExitState() {
		// Enemy.PlayerDetect = false;
	}

	void Charge() {
		Enemy.TargetPosition = GameObject.Find("PlayerBlob").transform.position;
		Enemy.StartingPosition = Enemy.transform.position;
		Enemy.transform.position = Vector2.MoveTowards(Enemy.transform.position, Enemy.TargetPosition, Enemy.Stats.Speed * Time.deltaTime);
	}
	// IEnumerator PlayerDetected() {
	// 	Enemy.Rb.velocity = Vector2.zero;

	// 	Logger.Log(Name, "End Charge");
	// 	yield return new WaitForSeconds(Enemy.Stats.DetectionPauseTime);
	// 	Enemy.StartCoroutine(PlayerNotDetected());
	// }
	// public IEnumerator PlayerNotDetected() {
	// 	yield return new WaitForSeconds(1);
	// 	Enemy.PlayerDetect = false;
	// }
}
