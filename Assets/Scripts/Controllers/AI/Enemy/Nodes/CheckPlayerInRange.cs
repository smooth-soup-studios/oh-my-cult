using BehaviorTree;
using UnityEngine;

public class CheckPlayerInRange : Node {

	private static int _enemyLayerMask;

	public CheckPlayerInRange() {
		_enemyLayerMask = 1 << LayerMask.NameToLayer("Player");
	}

	public override NodeState Evaluate(BaseBehaviourTree tree) {
		GameObject target = tree.Target;
		if (target == null) {
			Collider2D[] colliders = Physics2D.OverlapCircleAll(tree.transform.position, tree.Stats.DetectionRange, _enemyLayerMask);
			if (colliders.Length > 0) {
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
