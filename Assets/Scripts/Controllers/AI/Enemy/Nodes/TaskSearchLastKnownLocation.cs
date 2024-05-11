using UnityEngine;
using BehaviorTree;

public class TaskSearchLastKnownLocation : Node {
	private Transform _transform;
	private float _radius = 20f;
	public TaskSearchLastKnownLocation(Transform transform) {
		_transform = transform;
	}
	public override NodeState Evaluate(EnemyBehaviourTree tree) {
		Vector3 target = EnemyBT.SearchLocation;
		tree.AttackCounter = -0.04f;

		if (Vector2.Distance(_transform.position, target) > 1f) {
			tree.Agent.SetDestination(target);
			tree.Agent.speed = 20;
		}
		if (Vector2.Distance(_transform.position, target) < 1f) {
			EnemyBT.SearchLocation = Vector3.zero;
			State = NodeState.SUCCESS;
			return State;
		}
		State = NodeState.RUNNING;
		return State;
	}
}