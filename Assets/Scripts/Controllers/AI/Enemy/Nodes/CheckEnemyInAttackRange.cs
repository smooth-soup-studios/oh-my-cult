using UnityEngine;
using BehaviorTree;

public class CheckEnemyInAttackRange : Node {
	private Transform _transform;

	public CheckEnemyInAttackRange(Transform transform) {
		_transform = transform;
	}

	public override NodeState Evaluate(EnemyBehaviourTree tree) {
		GameObject target = EnemyBT.Target;
		if (target == null) {
			State = NodeState.FAILURE;
			return State;
		}
		if (Vector2.Distance(_transform.position, target.transform.position) <= EnemyBT.AttackRange) {
			tree.EnemyAnimator.SetBool("IsAttacking", true);
			State = NodeState.SUCCESS;
			return State;


		}
		tree.EnemyAnimator.SetBool("IsAttacking", false);
		State = NodeState.FAILURE;
		return State;
	}
}