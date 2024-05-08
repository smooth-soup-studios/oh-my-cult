using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskGoToTarget : Node {
	private Transform _transform;
	private string _name = "target";
	private float _radius = 20f;
	public TaskGoToTarget(Transform transform) {
		_transform = transform;
	}

	public override NodeState Evaluate(EnemyBehaviourTree tree) {
		Vector3 target = EnemyBT.Target.transform.position;

		if (Vector2.Distance(_transform.position, target) > 1f) {
			Vector3 movePos = target;
			movePos = Vector3.MoveTowards(movePos, _transform.position, _radius);
			EnemyBT.Agent.SetDestination(movePos);
			EnemyBT.Agent.speed = 20;
			Logger.Log(_name, "Charge");
		}
		if (Vector2.Distance(_transform.position, target) > 40f) {
			EnemyBT.Target = null;
			State = NodeState.FAILURE;
			return State;
		}
		State = NodeState.RUNNING;
		return State;
	}

}

