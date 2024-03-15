using System.Collections.Generic;
using System.Linq;

namespace Controllers.AI.BehaviourTrees.Nodes {
public class SequenceNode : BehaviourTreeNode {
	List<BehaviourTreeNode> _children;

	public SequenceNode(List<BehaviourTreeNode> children) {
		_children = children;
	}

	public override bool Execute() => _children.All(child => child.Execute());
}
}