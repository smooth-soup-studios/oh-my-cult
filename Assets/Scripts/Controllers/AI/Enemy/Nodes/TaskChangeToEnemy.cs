using UnityEngine;
using BehaviorTree;

public class TaskChangeToEnemy : Node {

	public override NodeState Evaluate(BaseBehaviourTree tree) {
		tree.ActorAnimator.SetBool("IsNPC", false);
		tree.ActorType = ActorType.MeleeEnemy;
		State = NodeState.RUNNING;
		return State;
	}
}
