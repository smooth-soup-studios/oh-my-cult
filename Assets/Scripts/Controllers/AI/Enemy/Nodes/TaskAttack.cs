using BehaviorTree;
using UnityEngine;

public class TaskAttack : Node {
	private EnemyHealthController _enemy;
	private EnemyBiteAttack _enemyBiteAttack;

	private float _attackTime = 1f;
	private float _attackCounter = 0f;

	public TaskAttack(Transform transform) { }

	public override NodeState Evaluate(EnemyBehaviourTree tree) {
		_enemy = tree.GetComponent<EnemyHealthController>();
		_enemyBiteAttack = tree.gameObject.GetComponent<EnemyBiteAttack>();
		_attackCounter += Time.deltaTime;
		if (_attackCounter >= _attackTime) {
			_enemyBiteAttack.Attack();
			_attackCounter = 0f;
		}
		State = NodeState.RUNNING;
		return State;
	}
}
