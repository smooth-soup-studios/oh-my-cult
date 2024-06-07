using System.Linq;
using BehaviorTree;
using UnityEngine;

public class CheckAgentHasWaypoints : Node {
	private Transform[] _waypoints;

	public CheckAgentHasWaypoints(Transform[] waypoints) {
		_waypoints = waypoints;
	}

	public override NodeState Evaluate(BaseBehaviourTree tree) {
		if (_waypoints == null || _waypoints.Length < 2 || _waypoints.Any(e => e == null))
			return NodeState.FAILURE;
		return NodeState.SUCCESS;

	}
}