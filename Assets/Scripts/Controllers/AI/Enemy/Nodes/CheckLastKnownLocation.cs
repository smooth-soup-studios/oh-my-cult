using BehaviorTree;
using UnityEngine;

public class CheckLastKnownLocation : Node {
	private Transform _transform;
	public CheckLastKnownLocation(Transform transform) {
		_transform = transform;
	}

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
