using System.Linq;
using BehaviorTree;
using UnityEngine;

public class CheckAgentHasWaypoints : Node {

	public CheckAgentHasWaypoints(Transform transform) { }

	public override NodeState Evaluate(EnemyBehaviourTree tree) {
		// just make this a single tree already -_-
		EnemyBT enemyBT = tree as EnemyBT;
		if (enemyBT == null || enemyBT.Waypoints == null || enemyBT.Waypoints.Length < 2 || enemyBT.Waypoints.Any(e => e == null))
			return NodeState.FAILURE;
		return NodeState.SUCCESS;

	}
}