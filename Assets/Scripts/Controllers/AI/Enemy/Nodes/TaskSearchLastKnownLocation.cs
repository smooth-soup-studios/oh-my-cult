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
			//target = Vector3.MoveTowards(_transform.position, target, _radius);
			EnemyBT.Agent.SetDestination(target);
			EnemyBT.Agent.speed = 20;
            Logger.Log("Search", "Searching");
		}
		if (Vector2.Distance(_transform.position, target) < 5f) {
            Logger.Log("Search", "Found");
			EnemyBT.SearchLocation = Vector3.zero;
			State = NodeState.SUCCESS;
			return State;
		}
		State = NodeState.RUNNING;
		return State;
	}
}
