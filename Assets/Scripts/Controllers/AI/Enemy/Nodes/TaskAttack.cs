using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class TaskAttack : Node
{


    private Transform _lastTarget;
    private EnemyHealthController _enemyHealthController;

    private float _attackTime = 1f;
    private float _attackCounter = 0f;

    public TaskAttack(Transform transform)
    {

    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");
        if (target != _lastTarget)
        {
            _enemyHealthController = target.GetComponent<EnemyHealthController>();
            _lastTarget = target;
        }

        _attackCounter += Time.deltaTime;
        if (_attackCounter >= _attackTime)
        {
            bool enemyIsDead = _enemyHealthController;
            if (enemyIsDead)
            {
                ClearData("target");

            }
            else
            {
                _attackCounter = 0f;
            }
        }

        State = NodeState.RUNNING;
        return State;
    }

}
