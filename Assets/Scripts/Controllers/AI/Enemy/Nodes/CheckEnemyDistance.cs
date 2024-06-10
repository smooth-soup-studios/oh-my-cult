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


		if (Vector2.Distance(_transform.position, tree.Target.transform.position) < 10f) {
			Logger.Log("Check", "Distance");
			State = NodeState.SUCCESS;
			return State;
		}
		State = NodeState.SUCCESS;
		return State;
	}
}