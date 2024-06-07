using BehaviorTree;
using UnityEngine;

public class TaskAttack : Node {

	private float _attackTime = 0.48f;

	public override NodeState Evaluate(BaseBehaviourTree tree) {
		tree.AttackCounter += Time.deltaTime;
		if (tree.AttackCounter >= _attackTime) {
			tree.Stats.EnemyWeapon.PrimaryAction(tree.gameObject);
			tree.AttackCounter = -0.18f;
		}
		State = NodeState.RUNNING;
		return State;
	}
}
