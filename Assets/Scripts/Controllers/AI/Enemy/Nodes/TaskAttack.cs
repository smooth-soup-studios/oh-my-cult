using BehaviorTree;
using UnityEngine;

public class TaskAttack : Node {
	private EnemyBiteAttack _enemyBiteAttack;

	private float _attackTime = 0.48f;


	public TaskAttack(Transform transform) { }

	public override NodeState Evaluate(BaseBehaviourTree tree) {
		_enemyBiteAttack = tree.gameObject.GetComponent<EnemyBiteAttack>();
		tree.AttackCounter += Time.deltaTime;
		if (tree.AttackCounter >= _attackTime) {
			tree.EnemyWeapon.PrimaryAction(tree.gameObject);
			tree.AttackCounter = -0.18f;
		}
		State = NodeState.RUNNING;
		return State;
	}
}
