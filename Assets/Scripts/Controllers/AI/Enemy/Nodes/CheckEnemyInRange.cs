using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;
using UnityEngine.AI;

public class CheckEnemyInRange : Node {

	private static int _enemyLayerMask = 1 << 6;
	private Transform _transform;

	public CheckEnemyInRange(Transform transform) {
		_transform = transform;
	}

	public override NodeState Evaluate(EnemyBehaviourTree tree) {
		object t = GetData("target");
		if (t == null) {
			Collider2D[] colliders = Physics2D.OverlapCircleAll(_transform.position, EnemyBT.FovRange, _enemyLayerMask);
			if (colliders.Length > 0) {
				Parent.Parent.SetData("target", colliders[0].transform);
				State = NodeState.SUCCESS;
				return State;
			}
			State = NodeState.FAILURE;
			return State;
		}
		State = NodeState.SUCCESS;
		return State;
	}

}
