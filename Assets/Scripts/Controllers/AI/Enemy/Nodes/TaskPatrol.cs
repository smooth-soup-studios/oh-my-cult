using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using UnityEngine.AI;
using System.Linq;

public class TaskPatrol : Node {
	private Transform[] _waypoints;
	private int _currentWaypointIndex = 0;
	private float _waitTime = 1f; // in seconds
	private float _waitCounter = 0f;
	private bool _waiting = false;
	private bool _endReached = true;
	private string _name = "Patrol";
	public TaskPatrol(Transform[] waypoints) {
		_waypoints = waypoints;
	}

	public override NodeState Evaluate(EnemyBehaviourTree tree) {
		EnemyBT.Agent.speed = 10f;
		if (_waiting) {
			_waitCounter += Time.deltaTime;
			if (_waitCounter >= _waitTime) {
				_waiting = false;
			}
		}
		else {
			if (Vector2.Distance(EnemyBT.Agent.transform.position, _waypoints[_waypoints.Length - 1].transform.position) < 0.01f) {
				_waypoints = _waypoints.Reverse().ToArray();
				_currentWaypointIndex = 1;
				EnemyBT.Agent.destination = _waypoints[_currentWaypointIndex].position;
			}
			else if (Vector2.Distance(EnemyBT.Agent.transform.position, _waypoints[_currentWaypointIndex].transform.position) < 0.01f) {
				_waitCounter = 0;
				_waiting = true;
				_currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
				EnemyBT.Agent.destination = _waypoints[_currentWaypointIndex].position;
			}
			EnemyBT.Agent.destination = _waypoints[_currentWaypointIndex].position;
		}
		State = NodeState.RUNNING;
		return State;
	}
}

