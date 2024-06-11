using System.Collections.Generic;

namespace BehaviorTree {
	public class Selector : Node {
		public Selector() : base() { }
		public Selector(List<Node> children) : base(children) { }
		public override NodeState Evaluate(BaseBehaviourTree tree) {
			foreach (Node node in Children) {
				switch (node.Evaluate(tree)) {
					case NodeState.FAILURE:
						continue;
					case NodeState.SUCCESS:
						State = NodeState.SUCCESS;
						return State;
					case NodeState.RUNNING:
						State = NodeState.RUNNING;
						return State;
					default:
						continue;
				}
			}
			State = NodeState.FAILURE;
			return State;
		}
	}
}