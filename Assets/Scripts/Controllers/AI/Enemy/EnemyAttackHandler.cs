using BehaviorTree;
using UnityEngine;

public class EnemyAttackHandler : MonoBehaviour {
	private BaseBehaviourTree _behaviourTree;
	private void Awake() {
		_behaviourTree = GetComponent<BaseBehaviourTree>();
	}

	public void DoAttack() {
		_behaviourTree.ActorAnimator.SetBool("IsAttacking", false);
		_behaviourTree.Stats.EnemyWeapon.PrimaryAction(gameObject);
	}

	public void RangedAttack() {
		_behaviourTree.ActorAnimator.SetBool("IsAttacking", false);
		_behaviourTree.Stats.EnemyWeapon.SecondaryAction(gameObject);
	}
}