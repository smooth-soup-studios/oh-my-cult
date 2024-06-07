using UnityEngine;
using BehaviorTree;

public class CheckEnemyType : Node {

	public CheckEnemyType() {

	}

	public override NodeState Evaluate(BaseBehaviourTree tree) {

			EnemyState _enemyState = tree.EnemType;
			if(_enemyState == EnemyState.NPC){
				State = NodeState.SUCCESS;
				return State;
			}


		State = NodeState.FAILURE;
		return State;
}
}
