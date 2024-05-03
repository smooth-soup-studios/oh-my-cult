using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BehaviorTree {
	public class Sequence : Node {
		public Sequence() : base() { }
		public Sequence(List<Node> children) : base(children) { }
		public override NodeState Evaluate(EnemyBehaviourTree tree) {
			bool anyChildIsRunning = false;
			foreach (Node node in Children) {
				switch (node.Evaluate(tree)) {
					case NodeState.FAILURE:
						State = NodeState.FAILURE;
						return State;
					case NodeState.SUCCESS:
						continue;
					case NodeState.RUNNING:
						anyChildIsRunning = true;
						continue;
					default:
						State = NodeState.SUCCESS;
						return State;
				}
			}
			State = anyChildIsRunning ? NodeState.RUNNING : NodeState.SUCCESS;
			return State;
		}
	}
}
