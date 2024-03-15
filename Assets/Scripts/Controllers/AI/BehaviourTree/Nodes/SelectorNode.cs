using System.Collections.Generic;
using System.Linq;

namespace Controllers.AI.BehaviourTrees.Nodes {
public class SelectorNode : BehaviourTreeNode {
	List<BehaviourTreeNode> _children;

	public SelectorNode(List<BehaviourTreeNode> children) {
		_children = children;
	}

	public override bool Execute() => _children.Any(child => child.Execute());

}
}