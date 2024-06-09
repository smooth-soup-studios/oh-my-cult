using BehaviorTree;

public class TaskChangeToBear : Node {

    public override NodeState Evaluate(BaseBehaviourTree tree) {

		if(tree.ActorAnimator.GetBool("IsBear")) {
			return NodeState.FAILURE;
		}
        tree.ActorAnimator.SetBool("IsBear", true);
        tree.ActorType = ActorType.BearEnemy;
		Logger.Log("Bear", "IsBear");
        State = NodeState.SUCCESS;
        return State;
    }
}
