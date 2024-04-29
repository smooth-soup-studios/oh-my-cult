using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class CheckEnemyInAttackRange : Node
{


    private Transform _transform;

        public CheckEnemyInAttackRange(Transform transform)
    {
        _transform = transform;
    }

    public override NodeState Evaluate()
    {
        object t = GetData("target");
        if (t == null){
            State = NodeState.FAILURE;
            return State;
        }

        Transform target = (Transform)t;
        if(Vector3.Distance(_transform.position, target.position) <= EnemyBT.AttackRange){

            State = NodeState.SUCCESS;
            return State;
        }
        State = NodeState.FAILURE;
        return State;
    }
}
