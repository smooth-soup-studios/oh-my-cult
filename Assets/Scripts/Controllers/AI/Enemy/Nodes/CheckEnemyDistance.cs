using BehaviorTree;
using UnityEngine;


public class CheckEnemyDistance : Node {
	private Transform _transform;
	private static int _enemyLayerMask;
	public CheckEnemyDistance(Transform transform) {
		_enemyLayerMask = 1 << LayerMask.NameToLayer("Player");
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