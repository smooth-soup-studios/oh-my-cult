using UnityEngine;
using BehaviorTree;

public class TaskSearchLastKnownLocation : Node {
	private Transform _transform;

	public TaskSearchLastKnownLocation(Transform transform) {
		_transform = transform;
	}

	public override NodeState Evaluate(BaseBehaviourTree tree) {
		Vector3 target = tree.SearchLocation;

		if (Vector2.Distance(_transform.position, target) > 1f) {
			tree.Agent.SetDestination(target);
		}
		if (Vector2.Distance(_transform.position, target) < 1f) {
			tree.SearchLocation = Vector3.zero;
			State = NodeState.SUCCESS;
			return State;
		}
		State = NodeState.RUNNING;
		return State;
	}
}
