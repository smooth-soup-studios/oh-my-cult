using UnityEngine;
using BehaviorTree;

public class TaskChangeToEnemy : Node {

	public override NodeState Evaluate(BaseBehaviourTree tree) {
		tree.ActorType = ActorType.MeleeEnemy;
		State = NodeState.RUNNING;
		return State;
	}
}
