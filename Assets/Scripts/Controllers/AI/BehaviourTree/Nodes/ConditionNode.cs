using System;

namespace Controllers.AI.BehaviourTrees.Nodes {
public class ConditionNode : BehaviourTreeNode {
	Func<bool> _condition;

	public ConditionNode(Func<bool> condition) {
		_condition = condition;
		if (_condition == null) {
			throw new ArgumentNullException(nameof(condition), "A condition must be provided.");
		}
	}

	public override bool Execute() => _condition();
}
}
