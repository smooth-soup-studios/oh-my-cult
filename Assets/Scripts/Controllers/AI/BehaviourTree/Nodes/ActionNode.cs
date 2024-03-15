using System;

namespace Controllers.AI.BehaviourTrees.Nodes {
public class ActionNode : BehaviourTreeNode {
	Action _action;

	public ActionNode(Action action) {
		_action = action;
	}

	public override bool Execute() => throw new NotImplementedException();
}
}