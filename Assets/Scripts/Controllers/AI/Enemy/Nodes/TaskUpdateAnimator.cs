using BehaviorTree;

public class TaskUpdateAnimator : Node {
	private bool _returnSuccess;
	public TaskUpdateAnimator(bool returnSuccess = false) {
		_returnSuccess = returnSuccess;
	}

	// Basically a blank node returning whatever the user wants
	public override NodeState Evaluate(BaseBehaviourTree tree) {
		ActorType type = tree.ActorType;
		switch (type) {
			case ActorType.NPC:
				tree.ActorAnimator.SetBool("IsNPC", true);
				tree.ActorAnimator.SetBool("IsBear", false);
				break;
			case ActorType.BearEnemy:
				tree.ActorAnimator.SetBool("IsNPC", false);
				tree.ActorAnimator.SetBool("IsBear", true);
				break;
			case ActorType.MeleeEnemy:
				tree.ActorAnimator.SetBool("IsNPC", false);
				tree.ActorAnimator.SetBool("IsBear", false);
				break;
		}
		return _returnSuccess ? NodeState.SUCCESS : NodeState.FAILURE;
	}
}
