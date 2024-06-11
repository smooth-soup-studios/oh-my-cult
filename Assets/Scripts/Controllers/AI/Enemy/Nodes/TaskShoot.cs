using BehaviorTree;
using UnityEngine;


public class TaskShoot : Node {
	private float _cooldown = 2f;
	private float _cooldownTimer;
	public TaskShoot() {

	}

	public override NodeState Evaluate(BaseBehaviourTree tree) {
		if (Time.time > _cooldownTimer) {
			GameObject.Instantiate(tree.Stats.EnemyProjectile, tree.transform.position, Quaternion.identity);
			Logger.Log($"{tree.Target}", "Target");
			_cooldownTimer = Time.time + _cooldown;
		}
		State = NodeState.RUNNING;
		return State;
	}
}