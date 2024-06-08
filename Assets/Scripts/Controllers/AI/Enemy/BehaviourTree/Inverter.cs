using System.Collections.Generic;

namespace BehaviorTree {
	public class Inverter : Node {
		private Node _kiddo;
		public Inverter(Node child) {
			_kiddo = child;
		}

		public override NodeState Evaluate(BaseBehaviourTree tree) {

			return _kiddo.Evaluate(tree) switch {
				NodeState.SUCCESS => NodeState.FAILURE,
				NodeState.FAILURE => NodeState.SUCCESS,
				NodeState.RUNNING => NodeState.RUNNING,
				_ => State,
			};
		}
	}
}