using BehaviorTree;
using UnityEngine;

public class CheckLastKnownLocation : Node {

	public override NodeState Evaluate(BaseBehaviourTree tree) {
		Vector3 search = tree.SearchLocation;
		if (search == Vector3.zero) {
			State = NodeState.FAILURE;
			return State;
		}
		State = NodeState.SUCCESS;
		return State;
	}
}
