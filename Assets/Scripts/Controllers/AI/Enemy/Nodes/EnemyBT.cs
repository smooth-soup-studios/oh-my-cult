using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;


public class EnemyBT : EnemyBehaviourTree {
	public Transform[] Waypoints;
	public static float Speed = 2f;
	public static float FovRange = 30f;
	public static float AttackRange = 20f;
	public static GameObject Target = null;
	public static Vector3 SearchLocation = Vector3.zero;




	private new void Awake() {
		base.Awake();
		Agent.updateRotation = false;
		Agent.updateUpAxis = false;
	}

	protected override Node SetupTree() {
		Node root = new Selector(new List<Node>
		{
			 new Sequence(new List<Node>
			{
				new CheckEnemyInAttackRange(transform),
				new TaskAttack(transform),
			}),
			new Sequence(new List<Node>
			{
				new CheckEnemyInRange(transform),
				new TaskGoToTarget(transform),
			}),
			new Sequence(new List<Node>
			{
				new CheckLastKnownLocation(transform),
				new TaskSearchLastKnownLocation(transform),
			}),
			new TaskPatrol( Waypoints),
		});

		return root;
	}
	private void OnDrawGizmos() {
		if (transform == null)
			return;
		Gizmos.DrawWireSphere(transform.position, FovRange);
	}
}