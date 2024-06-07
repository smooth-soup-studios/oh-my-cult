using BehaviorTree;
using UnityEngine;

public class CheckAgressionDisabled : Node {

	public CheckAgressionDisabled() { }

	public override NodeState Evaluate(BaseBehaviourTree tree) {
		return tree.DisableAgression ? NodeState.FAILURE : NodeState.SUCCESS;
	}
}