using BehaviorTree;
using UnityEngine;


public class CheckTargetInRetreatRange : Node {
	private Transform _transform;

	public CheckTargetInRetreatRange(Transform transform) {
		_transform = transform;
	}

	public override NodeState Evaluate(BaseBehaviourTree tree) {

		GameObject target = tree.Target;
		if (target != null) {


			if (Vector3.Distance(_transform.position, target.transform.position) > tree.Stats.RetreatRange) {
				tree.Target = null;
				State = NodeState.FAILURE;
				return State;
			}
			State = NodeState.SUCCESS;
			return State;
		}



		State = NodeState.FAILURE;
		return State;
	}
}