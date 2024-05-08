using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskSearchLastKnownLocation : Node
{
    private Transform _transform;
    private float _radius = 20f;
    public TaskSearchLastKnownLocation(Transform transform) {
		_transform = transform;
	}
    public override NodeState Evaluate(EnemyBehaviourTree tree) {
		Vector3 target = EnemyBT.SearchLocation;

		if (Vector2.Distance(_transform.position, target) > 1f) {
			EnemyBT.Agent.SetDestination(target);
			EnemyBT.Agent.speed = 20;
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
