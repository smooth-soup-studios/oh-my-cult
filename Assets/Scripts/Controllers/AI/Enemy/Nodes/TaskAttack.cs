using BehaviorTree;
using UnityEngine;

public class TaskAttack : Node {
	private EnemyBiteAttack _enemyBiteAttack;

	private float _attackTime = 1.04f;


	public TaskAttack(Transform transform) { }

	public override NodeState Evaluate(EnemyBehaviourTree tree) {
		_enemyBiteAttack = tree.gameObject.GetComponent<EnemyBiteAttack>();
		tree.AttackCounter += Time.deltaTime;
		if (tree.AttackCounter >= _attackTime) {
			_enemyBiteAttack.Attack();
			tree.AttackCounter = -0.04f;
		}
		State = NodeState.RUNNING;
		return State;
	}
}
