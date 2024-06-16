using UnityEngine;
using BehaviorTree;

public class TaskGoToTarget : Node {
	private float _radius = 1.25f;

	public TaskGoToTarget() {

	}

	public override NodeState Evaluate(BaseBehaviourTree tree) {
		Transform ActorPos = tree.gameObject.transform;
		Vector3 target = tree.Target.transform.position;
		tree.Agent.speed = tree.Stats.ChargeSpeed;

		tree.Movement = (tree.Target.transform.position - tree.Agent.transform.position).normalized;
		tree.ActorAnimator.SetFloat("X", tree.Movement.x);
		tree.ActorAnimator.SetFloat("Y", tree.Movement.y);

		if (Vector2.Distance(ActorPos.position, target) > 1.4f) {
			Vector3 movePos = target;
			movePos = Vector3.MoveTowards(movePos, ActorPos.position, _radius);
			tree.Agent.SetDestination(movePos);
		}
		if (Vector2.Distance(ActorPos.position, target) > tree.Stats.DetectionRange) {
			tree.Target = null;
			State = NodeState.FAILURE;
			return State;
		}
		State = NodeState.RUNNING;
		return State;
	}

}

