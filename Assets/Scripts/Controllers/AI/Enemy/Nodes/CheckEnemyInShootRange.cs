using BehaviorTree;
using UnityEngine;

public class CheckEnemyDistance : Node {
	private Transform _transform;
	public CheckEnemyDistance(Transform transform) {
		_transform = transform;
	}

	public override NodeState Evaluate(BaseBehaviourTree tree) {

		if (Vector2.Distance(_transform.position, tree.Target.transform.position) > 15f) {
			State = NodeState.FAILURE;
			return State;
		}
		State = NodeState.FAILURE;
		return State;
	}
}