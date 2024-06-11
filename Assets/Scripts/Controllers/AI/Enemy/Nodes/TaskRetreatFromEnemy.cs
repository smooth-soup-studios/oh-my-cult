using BehaviorTree;
using UnityEngine;


public class TaskRetreatFromEnemy : Node {
	private Transform _transform;
	private float _radius = 1.25f;
	public TaskRetreatFromEnemy(Transform transform) {
		_transform = transform;
	}

	public override NodeState Evaluate(BaseBehaviourTree tree) {
		Transform ActorPos = tree.gameObject.transform;
		Vector3 target = tree.Target.transform.position;

		tree.Movement = (tree.Target.transform.position - tree.Agent.transform.position).normalized;
		tree.ActorAnimator.SetFloat("X", -tree.Movement.x);
		tree.ActorAnimator.SetFloat("Y", -tree.Movement.y);


        Vector3 dirToPlayer = _transform.position - target;
        Vector3 newPosition = _transform.position + dirToPlayer;

		if (Vector3.Distance(_transform.position, target) < tree.Stats.RetreatRange) {
			// Vector3 movePos = target;
			// movePos = Vector3.MoveTowards(movePos, -ActorPos.position, _radius);
			tree.Agent.SetDestination(newPosition);
		}
		if (Vector3.Distance(_transform.position, target) > tree.Stats.RetreatRange) {
			tree.Target = null;
			State = NodeState.FAILURE;
			return State;
		}
		State = NodeState.RUNNING;
		return State;

	}
}