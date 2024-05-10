using UnityEngine;
using BehaviorTree;

public class TaskGoToTarget : Node {
	private Transform _transform;
	private string _name = "target";
	private float _radius = 20f;

	public TaskGoToTarget(Transform transform) {
		_transform = transform;
	}

	public override NodeState Evaluate(EnemyBehaviourTree tree) {
		Vector3 target = EnemyBT.Target.transform.position;
		tree.Movement = (EnemyBT.Target.transform.position - EnemyBT.Agent.transform.position).normalized;
		EnemyBT.EnemyAnimator.SetFloat("X", tree.Movement.x);
		EnemyBT.EnemyAnimator.SetFloat("Y", tree.Movement.y);

		if (Vector2.Distance(_transform.position, target) > 1f) {
			Vector3 movePos = target;
			movePos = Vector3.MoveTowards(movePos, _transform.position, _radius);
			EnemyBT.Agent.SetDestination(movePos);
			EnemyBT.Agent.speed = 20;
		}
		if (Vector2.Distance(_transform.position, target) > 40f) {
			EnemyBT.Target = null;
			State = NodeState.FAILURE;
			return State;
		}
		State = NodeState.RUNNING;
		return State;
	}

}

