using System.Linq;
using BehaviorTree;
using UnityEngine;

public class CheckAgressionDisabled : Node {

	public CheckAgressionDisabled(Transform transform) { }

	public override NodeState Evaluate(EnemyBehaviourTree tree) {
		EnemyBT enemyBT = tree as EnemyBT;
		if (enemyBT) {
			return enemyBT.DisableAgression ? NodeState.FAILURE : NodeState.SUCCESS;
		}
		return NodeState.SUCCESS;
	}
}