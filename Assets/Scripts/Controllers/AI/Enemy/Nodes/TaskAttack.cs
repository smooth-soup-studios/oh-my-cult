using BehaviorTree;
using UnityEngine;

public class TaskAttack : Node {
	private EnemyHealthController _enemy;
	private EnemyBiteAttack _enemyBiteAttack;

	private float _attackTime = 1.04f;


	public TaskAttack(Transform transform) { }

	public override NodeState Evaluate(EnemyBehaviourTree tree) {
		_enemy = tree.GetComponent<EnemyHealthController>();
		_enemyBiteAttack = tree.gameObject.GetComponent<EnemyBiteAttack>();
		EnemyBT.AttackCounter += Time.deltaTime;
		if (EnemyBT.AttackCounter >= _attackTime) {
			_enemyBiteAttack.Attack();
			EnemyBT.AttackCounter = -0.04f;
		}
		State = NodeState.RUNNING;
		return State;
	}
}
