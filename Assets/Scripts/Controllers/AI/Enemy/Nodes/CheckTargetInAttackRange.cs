using UnityEngine;
using BehaviorTree;

public class CheckTargetInAttackRange : Node {

	public override NodeState Evaluate(BaseBehaviourTree tree) {
		GameObject target = tree.Target;
		if (target == null) {
			State = NodeState.FAILURE;
			return State;
		}
		if (Vector2.Distance(tree.transform.position, target.transform.position) <= tree.Stats.AttackRange) {
			State = NodeState.SUCCESS;
			return State;
		}
		State = NodeState.FAILURE;
		return State;
	}
}
