using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class TaskAttack : Node {


	private EnemyHealthController _enemy;
	private Transform _lastTarget;
	private EnemyBiteAttack _enemyBiteAttack;

	private float _attackTime = 1f;
	private float _attackCounter = 0f;


	public TaskAttack(Transform transform) {
		_enemy = GameObject.Find("Enemy").GetComponent<EnemyHealthController>();
	}

	public override NodeState Evaluate(BehaviorTree.Tree tree) {
		Transform target = (Transform)GetData("target");
		_enemyBiteAttack = tree.gameObject.GetComponent<EnemyBiteAttack>();

		if (target != _lastTarget) {
			_lastTarget = target;
		}

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
