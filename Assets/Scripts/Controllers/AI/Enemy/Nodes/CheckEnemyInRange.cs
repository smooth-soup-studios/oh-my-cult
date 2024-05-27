using BehaviorTree;
using UnityEngine;

public class CheckEnemyInRange : Node {

	private static int _enemyLayerMask;
	private Transform _transform;

	public CheckEnemyInRange(Transform transform) {
		_transform = transform;
		_enemyLayerMask = 1 << LayerMask.NameToLayer("Player");
	}

	public override NodeState Evaluate(EnemyBehaviourTree tree) {
		GameObject target = tree.Target;
		if (target == null) {
			Collider2D[] colliders = Physics2D.OverlapCircleAll(_transform.position, tree.FOVRange, _enemyLayerMask);
			if (colliders.Length > 0) {
				//tree.target is unnecessary because at the beginning you say Target = target
				tree.Target = colliders[0].gameObject;
				State = NodeState.SUCCESS;
				return State;
			}
			State = NodeState.FAILURE;
			return State;
		}
		tree.SearchLocation = target.transform.position;
		State = NodeState.SUCCESS;
		return State;
	}

}
