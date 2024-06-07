using UnityEngine;
using BehaviorTree;

public class CheckEnemyInAttackRange : Node {

	public override NodeState Evaluate(BaseBehaviourTree tree) {
		GameObject target = tree.Target;
		if (target == null) {
			State = NodeState.FAILURE;
			return State;
		}
		if (Vector2.Distance(tree.transform.position, target.transform.position) <= tree.Stats.AttackRange) {
			tree.ActorAnimator.SetBool("IsAttacking", true);
			State = NodeState.SUCCESS;
			return State;


		}
		tree.ActorAnimator.SetBool("IsAttacking", false);
		State = NodeState.FAILURE;
		return State;
	}
}
