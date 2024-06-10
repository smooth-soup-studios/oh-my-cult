using BehaviorTree;
using UnityEngine;


public class TaskRetreatFromEnemy : Node {
	private Transform _transform;
	private float _radius = 1.25f;
	public TaskRetreatFromEnemy(Transform transform) {
		_transform = transform;
	}

	public override NodeState Evaluate(BaseBehaviourTree tree) {


		Vector3 target = tree.Target.transform.position;

		Vector3 dirToPlayer = _transform.position - target;
		Vector3 newPosition = _transform.position + dirToPlayer;

		if (Vector3.Distance(_transform.position, target) < 3f) {
			tree.Agent.SetDestination(newPosition);
			Logger.Log("Beweeg", "RenWeg");
		}
		if (Vector3.Distance(_transform.position, target) > 15f) {
			tree.Target = null;
			Logger.Log("Beweeg", "RenWeg");
			State = NodeState.FAILURE;
			return State;
		}
		State = NodeState.RUNNING;
		return State;
	}
}