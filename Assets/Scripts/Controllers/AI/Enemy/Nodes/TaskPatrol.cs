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
	private string name = "Patrol";
	public TaskPatrol(Transform[] waypoints) {

		_waypoints = waypoints;
	}

	public override NodeState Evaluate() {
		if (_waiting) {
			_waitCounter += Time.deltaTime;
			if (_waitCounter >= _waitTime) {
				_waiting = false;
			}
		}
		else {
			if (Vector2.Distance(EnemyBT.Agent.transform.position, _waypoints[_currentWaypointIndex].transform.position) < 0.01f) {

				_waitCounter = 0;
				_waiting = true;
				Logger.Log(name, $"{_currentWaypointIndex}");
				Logger.Log(name, $"{_endReached}");

				// if (_currentWaypointIndex == 0) {
				// 	_endReached = true;
				// }
				if (Vector2.Distance(EnemyBT.Agent.transform.position, _waypoints[_waypoints.Length - 1].transform.position) < 0.0001f) {

					_waypoints.Reverse();
					_currentWaypointIndex = 0;
				EnemyBT.Agent.destination = _waypoints[_currentWaypointIndex].position;
				}
				// if (!_endReached) {
				// 	_currentWaypointIndex--;
				// }
				// else if (_endReached) {
				// 	_currentWaypointIndex++;
				// }


				// if (_currentWaypointIndex >= 0) {
				// 	_currentWaypointIndex++;
				// }
				_currentWaypointIndex++;
				EnemyBT.Agent.destination = _waypoints[_currentWaypointIndex].position;
				string log = "";
				_waypoints.ToList().ForEach(w => log += w + " - ");
				Logger.Log(name, $" {log}");
			}
			else {
				EnemyBT.Agent.destination = _waypoints[_currentWaypointIndex].position;
			}
		}

		State = NodeState.RUNNING;
		return State;

	}
}

