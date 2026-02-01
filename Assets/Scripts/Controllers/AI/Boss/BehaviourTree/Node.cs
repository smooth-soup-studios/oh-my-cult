using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BossBehaviourTree{
	public enum BossNodeState {
		RUNNING,
		SUCCESS,
		FAILURE
	}

public class BossNode {
    protected BossNodeState State; 
	public BossNode Parent; 

	protected List<BossNode> Children = new();
	public BossNode() {
			Parent = null;
		}

		public BossNode(List<BossNode> children) {
			foreach (BossNode child in children) {
				_Attach(child);
			}
		}

		private void _Attach(BossNode bossNode) {
			bossNode.Parent = this;
			Children.Add(bossNode);
		}
    		public virtual BossNodeState Evaluate(BossBaseBehaviourTree tree) => BossNodeState.FAILURE;

}
}