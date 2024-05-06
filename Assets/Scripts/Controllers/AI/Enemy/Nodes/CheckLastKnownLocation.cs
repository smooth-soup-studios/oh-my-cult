using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class CheckLastKnownLocation : Node
{
    private Transform _transform;
    public CheckLastKnownLocation(Transform transform) {
		_transform = transform;
	}

	public override NodeState Evaluate(EnemyBehaviourTree tree) {
        Vector3 search = EnemyBT.SearchLocation;
        if (search == Vector3.zero){
            State = NodeState.FAILURE;
            return State;
        }

        State = NodeState.SUCCESS;
        return State;
    }
}
