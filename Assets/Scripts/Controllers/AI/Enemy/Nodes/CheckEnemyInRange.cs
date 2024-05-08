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
		GameObject target = EnemyBT.Target;
		if (target == null) {
			Collider2D[] colliders = Physics2D.OverlapCircleAll(_transform.position, EnemyBT.FovRange, _enemyLayerMask);
			if (colliders.Length > 0) {
				EnemyBT.Target = colliders[0].gameObject;
				State = NodeState.SUCCESS;
				return State;
			}
			State = NodeState.FAILURE;
			return State;
		}
		EnemyBT.SearchLocation = target.transform.position;
		State = NodeState.SUCCESS;
		return State;
	}

}
