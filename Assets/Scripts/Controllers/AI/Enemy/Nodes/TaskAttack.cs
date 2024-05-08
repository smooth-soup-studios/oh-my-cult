using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class TaskAttack : Node {


	private EnemyHealthController _enemy;
	private EnemyBiteAttack _enemyBiteAttack;

	private float _attackTime = 1f;
	private float _attackCounter = 0f;


	public TaskAttack(Transform transform) {
		_enemy = GameObject.Find("Enemy").GetComponent<EnemyHealthController>();
	}

	public override NodeState Evaluate(EnemyBehaviourTree tree) {
		_enemyBiteAttack = tree.gameObject.GetComponent<EnemyBiteAttack>();
		
		_attackCounter += Time.deltaTime;
		if (_attackCounter >= _attackTime) {
			_enemyBiteAttack.Attack();
			if (_enemy.GetCurrentHealth() == 0) {
				ClearData("target");

			}
			else {
				_attackCounter = 0f;
			}
		}

		State = NodeState.RUNNING;
		return State;
	}

}
