using BehaviorTree;
using UnityEngine;

public class TaskRandomWalk : Node {
	// Config
	private float _minDistance = 30.0f;
	private float _maxDistance = 70.0f;
	private float _maxTravelTime = 7.0f;

	// State
	private Vector3 _randomTarget;
	private float _travelTime = 0.0f;

	public TaskRandomWalk(Transform transform) { }

	public override NodeState Evaluate(EnemyBehaviourTree tree) {
		if (_randomTarget == Vector3.zero) {
			_randomTarget = GetRandomPosition(tree.Agent.transform);
		}

		tree.Agent.destination = _randomTarget;
		tree.Movement = (_randomTarget - tree.Agent.transform.position).normalized;
		tree.EnemyAnimator.SetFloat("X", tree.Movement.x);
		tree.EnemyAnimator.SetFloat("Y", tree.Movement.y);

		tree.Agent.speed = 20f;
		tree.Agent.acceleration = 80;

		// Choose a new target if:
		// - The agent has reached the target
		if (Vector2.Distance(tree.Agent.transform.position, _randomTarget) < 0.01f) {
			ResetNode();
		}
		// - The agent has been traveling for too long (may indicate that the target is unreachable)
		else if (_travelTime >= _maxTravelTime) {
			ResetNode();
		}
		else {
			_travelTime += Time.deltaTime;
		}

		return NodeState.RUNNING;
	}

	private void ResetNode() {
		_randomTarget = Vector3.zero;
		_travelTime = 0f;
	}

	private Vector3 GetRandomPosition(Transform characterTransform) {
		// Generate a random angle and distance
		float randomAngle = Random.Range(0f, Mathf.PI * 2);
		float randomDistance = Random.Range(_minDistance, _maxDistance);

		// Calculate the position within the circle
		float offsetX = Mathf.Cos(randomAngle) * randomDistance;
		float offsetY = Mathf.Sin(randomAngle) * randomDistance;

		Vector2 randomPosition = new(characterTransform.position.x + offsetX,
									 characterTransform.position.y + offsetY);
		return randomPosition;
	}
}