using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EnemyPatrolState : EnemyBaseState {

	public EnemyPatrolState(Enemy enemy, string name) : base(enemy, name) { }

	public EnemyStatsSO Stats;

	public override void EnterState() {
	}

	public override void UpdateState() {
		Enemy.CheckForPlayer();
		EnemyMovement();
		if (Enemy.PlayerDetect == true) {
			Enemy.SwitchState("Detected");
		}
	}
	public override void ExitState() {
	}

	private void EnemyMovement() {
		// if (Enemy.Agent.destination == Enemy.Route[Enemy.RouteIndex].position) {
		// 	// Move towards next point.
		// 	// Enemy.Agent.destination = Vector3.MoveTowards(Enemy.transform.position, Enemy.Route[Enemy.RouteIndex].position, Enemy.Stats.Speed * Time.deltaTime);
		// 	Enemy.Agent.destination = Enemy.Route[Enemy.RouteIndex].position;
		// }

	Enemy.Agent.destination = Enemy.Route[Enemy.RouteIndex].position;
		if(!Enemy.Agent.pathPending && Enemy.Agent.remainingDistance < 0.5f) {
			if (!Enemy.IsResting) {
				Enemy.StartCoroutine(RestAtPoint());
			}
		}
	}

	IEnumerator RestAtPoint() {
		Enemy.IsResting = true;
		yield return new WaitForSeconds(Enemy.RestTime);
		// Move to next point or reset to start of the route
		if (Enemy.RouteIndex == Enemy.Route.Length - 1) {
			//Enemy.RouteIndex--;
			Enemy.EndReached = true;
			Logger.Log(Name, "EndReached");
		}
		else if (Enemy.RouteIndex == 0) {
			Enemy.EndReached = false;
		}

		if (!Enemy.EndReached) {
			Enemy.RouteIndex++;
		}
		else if (Enemy.EndReached) {
			Enemy.RouteIndex--;
		}
		// And make sure to set isResting back to false to allow the coroutine to start again at the next point.
		Enemy.IsResting = false;
	}
}
