using BehaviorTree;
using UnityEngine;


public class TaskShoot : Node {
	private float _cooldown = 0;
	private float _cooldownTimer = 1.04f;
	private Vector2 _offset = new Vector2(0f, 0f);
	public TaskShoot() {

	}

	public override NodeState Evaluate(BaseBehaviourTree tree) {
		_cooldown += Time.deltaTime;
		tree.Movement = (tree.Target.transform.position - tree.Agent.transform.position).normalized;
		tree.ActorAnimator.SetFloat("X", tree.Movement.x);
		tree.ActorAnimator.SetFloat("Y", tree.Movement.y);
		tree.Agent.SetDestination(tree.transform.position);

		if (_cooldown >= _cooldownTimer) {
			tree.ActorAnimator.SetBool("IsAttacking", true);
			GameObject.Instantiate(tree.Stats.EnemyProjectile, tree.transform.position, Quaternion.identity);
			Logger.Log($"{tree.Target}", "Target");
			_cooldown = 0f;
		}
		State = NodeState.RUNNING;
		return State;
	}
}