using BehaviorTree;
using UnityEngine;


public class TaskShoot : Node {
	private float _cooldown = 1.04f;
	private float _cooldownTimer = 0;
	private bool _setWeapon = false;
	public TaskShoot() {

	}

	public override NodeState Evaluate(BaseBehaviourTree tree) {
		if (!_setWeapon) {
			SetWeaponOriginONCE(tree);
		}

		_cooldownTimer += Time.deltaTime;
		tree.Movement = (tree.Target.transform.position - tree.Agent.transform.position).normalized;
		tree.ActorAnimator.SetFloat("X", tree.Movement.x);
		tree.ActorAnimator.SetFloat("Y", tree.Movement.y);
		tree.Agent.SetDestination(tree.transform.position);

		if (_cooldownTimer >= _cooldown) {
			tree.ActorAnimator.SetBool("IsAttacking", true);
			_cooldownTimer = 0f;
		}
		State = NodeState.RUNNING;
		return State;
	}


	private void SetWeaponOriginONCE(BaseBehaviourTree tree) {
		BirdShootAttack attack = tree.Stats.EnemyWeapon as BirdShootAttack;
		attack.Origin = tree.gameObject;
		_setWeapon = true;
	}
}