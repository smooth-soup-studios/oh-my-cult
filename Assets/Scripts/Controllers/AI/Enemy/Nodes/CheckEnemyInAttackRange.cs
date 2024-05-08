using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class CheckEnemyInAttackRange : Node
{
	private string name = "in range";


    private Transform _transform;

        public CheckEnemyInAttackRange(Transform transform)
    {
        _transform = transform;
    }

	public override NodeState Evaluate(EnemyBehaviourTree tree)
    {
        GameObject target = EnemyBT.Target;
        if (target == null){
            State = NodeState.FAILURE;
            return State;
        }

        if(Vector2.Distance(_transform.position, target.transform.position) <= EnemyBT.AttackRange){
			Logger.Log(name, "Attack");
            State = NodeState.SUCCESS;
            return State;
        }
        State = NodeState.FAILURE;
        return State;
    }
}
