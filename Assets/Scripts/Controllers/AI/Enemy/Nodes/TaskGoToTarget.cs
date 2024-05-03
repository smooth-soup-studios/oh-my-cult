using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskGoToTarget : Node {
	private Transform _transform;
	private string name = "target";
	private float _radius = 20f;
	public TaskGoToTarget(Transform transform) {
		_transform = transform;
	}

	public override NodeState Evaluate(BehaviorTree.EnemyBehaviourTree tree) {
		Transform target = (Transform)GetData("target");

		if (Vector2.Distance(_transform.position, target.position) > 1f) {
			// _transform.position = Vector3.MoveTowards(_transform.position, target.position, EnemyBT.Speed * Time.deltaTime);
			// _transform.LookAt(target.position);
			// EnemyBT.Agent.destination = target.position;
			Vector3 movePos = target.transform.position;
			movePos = Vector3.MoveTowards(movePos, _transform.position, _radius);
			EnemyBT.Agent.SetDestination(movePos);
			EnemyBT.Agent.speed = 20;
			Logger.Log(name, "Charge");
		}
		if (Vector2.Distance(_transform.position, target.position) > 40f) {
			ClearData("target");
			State = NodeState.FAILURE;
			return State;
		}
		State = NodeState.RUNNING;
		return State;
	}

}

