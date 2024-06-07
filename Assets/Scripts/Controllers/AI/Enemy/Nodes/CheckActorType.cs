using UnityEngine;
using BehaviorTree;
using System;

public class CheckActorType : Node {
	ActorType _typeToCheckFor;
	public CheckActorType(ActorType typeToCheckFor) {
		_typeToCheckFor = typeToCheckFor;
	}

	public override NodeState Evaluate(BaseBehaviourTree tree) {
		if (tree.ActorType == _typeToCheckFor) {
			State = NodeState.SUCCESS;
			return State;
		}

		State = NodeState.FAILURE;
		return State;
	}
}
