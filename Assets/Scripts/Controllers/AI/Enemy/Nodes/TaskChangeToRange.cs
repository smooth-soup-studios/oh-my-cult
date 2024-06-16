using BehaviorTree;

public class TaskChangeToRange : Node {

    public override NodeState Evaluate(BaseBehaviourTree tree) {

		if(tree.ActorAnimator.GetBool("IsRanged")) {
			return NodeState.FAILURE;
		}
        tree.ActorAnimator.SetBool("IsRanged", true);
        tree.ActorType = ActorType.RangedEnemy;
        State = NodeState.SUCCESS;
        return State;
    }
}