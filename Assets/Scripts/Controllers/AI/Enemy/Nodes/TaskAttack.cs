using BehaviorTree;
using UnityEngine;

public class TaskAttack : Node {


	public override NodeState Evaluate(BaseBehaviourTree tree) {
		tree.AttackCounter += Time.deltaTime;
		if (tree.AttackCounter >= tree.Stats.AttackSpeed) {
			tree.Stats.EnemyWeapon.PrimaryAction(tree.gameObject);
			tree.AttackCounter = 0;
		}
		State = NodeState.RUNNING;
		return State;
	}
}
