using UnityEngine;
using BehaviorTree;

public class TaskChangeToEnemy : Node {
	public float AnimationWaitTime = .2f;
	float _copyAnimationDuration;
	bool _didAnimationStart = false;
	float _tAnimationStart;
	bool _didInstantiateAnimationPrefab = false;

	public override NodeState Evaluate(BaseBehaviourTree tree) {
		if (!_didAnimationStart) {
			_didAnimationStart = true;
			_tAnimationStart = Time.time;
		}
		else {
			if (Time.time - _tAnimationStart > AnimationWaitTime && !_didInstantiateAnimationPrefab) {
				_didInstantiateAnimationPrefab = true;
				GameObject _animationContainerInstance = Object.Instantiate(tree.NPCTransformPrefab, tree.transform);
				_copyAnimationDuration = _animationContainerInstance.GetComponent<EnemyTransformAnimationContainerController>().Duration;
			}
			if (Time.time - _tAnimationStart > _copyAnimationDuration / 2 + AnimationWaitTime) {
				tree.HalfwayTransitionAnimation = true;
			}
			if (Time.time - _tAnimationStart > _copyAnimationDuration + AnimationWaitTime) {
				tree.ActorType = ActorType.MeleeEnemy;
			}
		}

		State = NodeState.RUNNING;
		return State;
	}
}
