using BehaviorTree;
using UnityEngine;

public class TaskAttack : Node {


	public override NodeState Evaluate(BaseBehaviourTree tree) {
		tree.AttackCounter += Time.deltaTime;
		if (tree.AttackCounter >= tree.Stats.AttackSpeed) {
			tree.ActorAnimator.SetBool("IsAttacking", true);
			tree.AttackCounter = 0;
			return NodeState.RUNNING;
		}
		return NodeState.FAILURE;
	}
}
