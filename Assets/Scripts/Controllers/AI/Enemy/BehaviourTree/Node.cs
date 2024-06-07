using System.Collections.Generic;

namespace BehaviorTree {
	public enum NodeState {
		RUNNING,
		SUCCESS,
		FAILURE
	}



	public class Node {
		protected NodeState State;
		public Node Parent;
		protected List<Node> Children = new();



		public Node() {
			Parent = null;
		}

		public Node(List<Node> children) {
			foreach (Node child in children) {
				_Attach(child);
			}
		}

		private void _Attach(Node node) {
			node.Parent = this;
			Children.Add(node);
		}
		public virtual NodeState Evaluate(BaseBehaviourTree tree) => NodeState.FAILURE;

	}
}
