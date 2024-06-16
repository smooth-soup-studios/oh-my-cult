using UnityEngine;
using BehaviorTree;
using System.Linq;

public class TaskPatrol : Node {
	private Transform[] _waypoints;
	private int _currentWaypointIndex = 0;
	private float _waitTime = 1f; // in seconds
	private float _waitCounter = 0f;
	private bool _waiting = false;
	private bool _endReached = true;



	public TaskPatrol(Transform[] waypoints) {
		_waypoints = waypoints;
	}

	public override NodeState Evaluate(BaseBehaviourTree tree) {
		// tree.EnemyAnimator.Play("EnemyWalk", MovementDirection);
		tree.Agent.speed = tree.Stats.Speed;
		tree.ActorAnimator.SetBool("IsNPC", false);
		tree.Movement = (_waypoints[_currentWaypointIndex].transform.position - tree.Agent.transform.position).normalized;
		tree.ActorAnimator.SetFloat("X", tree.Movement.x);
		tree.ActorAnimator.SetFloat("Y", tree.Movement.y);

		if (_waiting) {
			_waitCounter += Time.deltaTime;
			if (_waitCounter >= _waitTime) {
				_waiting = false;
			}
		}
		else {
			if (Vector2.Distance(tree.Agent.transform.position, _waypoints[_waypoints.Length - 1].transform.position) < 0.01f) {
				_waypoints = _waypoints.Reverse().ToArray();
				_currentWaypointIndex = 1;
				tree.Agent.destination = _waypoints[_currentWaypointIndex].position;
			}
			else if (Vector2.Distance(tree.Agent.transform.position, _waypoints[_currentWaypointIndex].transform.position) < 0.01f) {
				if (tree.RandomizeWaypoints) {
					_currentWaypointIndex = Random.Range(0, _waypoints.Length);
				}
				else {
					_waitCounter = 0;
					_waiting = true;
					_currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
				}
				tree.Agent.destination = _waypoints[_currentWaypointIndex].position;
			}
			tree.Agent.destination = _waypoints[_currentWaypointIndex].position;

		}

		State = NodeState.RUNNING;
		return State;
	}

}

