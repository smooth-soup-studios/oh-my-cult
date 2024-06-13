using UnityEngine;
using BehaviorTree;

public class TaskChangeToEnemy : Node {
	public float AnimationWaitTime = .2f;
	bool _didAnimationStart = false;
	float _tAnimationStart;
	GameObject _animationContainerInstance;

	public override NodeState Evaluate(BaseBehaviourTree tree) {
		if (!_didAnimationStart) {
			_didAnimationStart = true;
			_tAnimationStart = Time.time;
		}
		else {
			if (Time.time - _tAnimationStart > AnimationWaitTime && _animationContainerInstance == null) {
				_animationContainerInstance = Object.Instantiate(tree.NPCTransformPrefab, tree.transform);
			}
			if (Time.time - _tAnimationStart > _animationContainerInstance.GetComponent<EnemyTransformAnimationContainerController>().Duration / 2 + AnimationWaitTime) {
				tree.HalfwayTransitionAnimation = true;
			}
			if (Time.time - _tAnimationStart > _animationContainerInstance.GetComponent<EnemyTransformAnimationContainerController>().Duration + AnimationWaitTime) {
				tree.ActorType = ActorType.MeleeEnemy;
			}
		}

		State = NodeState.RUNNING;
		return State;
	}
}
