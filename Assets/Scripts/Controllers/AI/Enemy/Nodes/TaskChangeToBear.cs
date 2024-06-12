using BehaviorTree;

public class TaskChangeToBear : Node {

	public override NodeState Evaluate(BaseBehaviourTree tree) {

		if (tree.ActorAnimator.GetBool("IsBear")) {
			return NodeState.FAILURE;
		}
		tree.ActorType = ActorType.BearEnemy;
		State = NodeState.SUCCESS;
		return State;
	}
}
