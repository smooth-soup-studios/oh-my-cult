using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskGoToTarget : Node {
	private Transform _transform;
	public TaskGoToTarget(Transform transform) {
		_transform = transform;
	}

	public override NodeState Evaluate() {
		Transform target = (Transform)GetData("target");

		if (Vector2.Distance(_transform.position, target.position) > 0.01f) {
			// _transform.position = Vector3.MoveTowards(_transform.position, target.position, EnemyBT.Speed * Time.deltaTime);
			// _transform.LookAt(target.position);
			EnemyBT.Agent.destination = target.position;
		}
		if (Vector3.Distance(_transform.position, target.position) > 6f) {
			ClearData("target");
			State = NodeState.FAILURE;
			return State;
		}
		State = NodeState.RUNNING;
		return State;
	}

}

