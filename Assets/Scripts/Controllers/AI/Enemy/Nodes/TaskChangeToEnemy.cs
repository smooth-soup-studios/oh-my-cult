using UnityEngine;
using BehaviorTree;
using Unity.VisualScripting;

public class TaskChangeToEnemy : Node {
	private Transform _transform;
	private float _radius = 20f;

	public SpriteRenderer SpriteRenderer;
	public TaskChangeToEnemy(Transform transform) {

		_transform = transform;

	}
	public override NodeState Evaluate(BaseBehaviourTree tree) {
		tree.EnemyAnimator.SetBool("IsNPC", false);
		tree.EnemType = EnemyState.MeleeEnemy;
		State = NodeState.RUNNING;
		return State;
	}
}
