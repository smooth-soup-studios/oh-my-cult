using BehaviorTree;
using UnityEngine;


public class TaskShoot : Node {
	private float _cooldown = 5f;
	private float _cooldownTimer;
	private Vector2 _offset = new Vector2(0f, 0f);
	public TaskShoot() {

	}

	public override NodeState Evaluate(BaseBehaviourTree tree) {
		tree.Movement = (tree.Target.transform.position - tree.Agent.transform.position).normalized;
		tree.ActorAnimator.SetFloat("X", tree.Movement.x);
		tree.ActorAnimator.SetFloat("Y", tree.Movement.y);

		if (Time.time > _cooldownTimer) {
			tree.ActorAnimator.SetBool("IsAttacking", true);
			GameObject.Instantiate(tree.Stats.EnemyProjectile, tree.transform.position, Quaternion.identity);
			Logger.Log($"{tree.Target}", "Target");
			_cooldownTimer = Time.time + _cooldown;
			tree.Agent.SetDestination(tree.transform.position);
		}
		State = NodeState.RUNNING;
		return State;
	}
}