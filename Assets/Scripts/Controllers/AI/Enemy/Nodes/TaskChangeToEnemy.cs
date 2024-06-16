using UnityEngine;
using BehaviorTree;

public class TaskChangeToEnemy : Node {
	private Rigidbody2D _rb;
	private float _animationWaitTime = .2f;
	private float _copyAnimationDuration;
	private bool _didAnimationStart = false;
	private float _tAnimationStart;
	private bool _didInstantiateAnimationPrefab = false;

	public override NodeState Evaluate(BaseBehaviourTree tree) {
		if (!_didAnimationStart) {
			_didAnimationStart = true;
			_tAnimationStart = Time.time;
			if (tree.TryGetComponent<Rigidbody2D>(out _rb)) {
				_rb.constraints = RigidbodyConstraints2D.FreezeAll;
			}
		}
		else {
			if (Time.time - _tAnimationStart > _animationWaitTime && !_didInstantiateAnimationPrefab) {
				_didInstantiateAnimationPrefab = true;
				GameObject _animationContainerInstance = Object.Instantiate(tree.NPCTransformPrefab, tree.transform);
				_copyAnimationDuration = _animationContainerInstance.GetComponent<EnemyTransformAnimationContainerController>().Duration;
			}
			if (Time.time - _tAnimationStart > _copyAnimationDuration / 2 + _animationWaitTime) {
				// Why. I hate this. (I know why but still hate it.)
				tree.HalfwayTransitionAnimation = true;
			}
			if (Time.time - _tAnimationStart > _copyAnimationDuration + _animationWaitTime) {
				tree.ActorType = ActorType.MeleeEnemy;
				if (_rb) {
					_rb.constraints = RigidbodyConstraints2D.FreezeRotation;
				}
			}
		}

		State = NodeState.RUNNING;
		return State;
	}
}
