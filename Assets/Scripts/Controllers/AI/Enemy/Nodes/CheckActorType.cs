using UnityEngine;
using BehaviorTree;
using System;
using System.Linq;

public class CheckActorType : Node {
	ActorType[] _typesToCheckFor;
	public CheckActorType(ActorType typeToCheckFor) {
		_typesToCheckFor = new ActorType[] { typeToCheckFor };
	}
	public CheckActorType(ActorType[] typesToCheckFor) {
		_typesToCheckFor = typesToCheckFor;
	}

	public override NodeState Evaluate(BaseBehaviourTree tree) {
		if (_typesToCheckFor.Contains(tree.ActorType)) {
			State = NodeState.SUCCESS;
			return State;
		}

		State = NodeState.FAILURE;
		return State;
	}
}
